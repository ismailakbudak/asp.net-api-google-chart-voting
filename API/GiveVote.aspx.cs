using API.controllers;
using API.lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace API
{
    public partial class GiveVote : System.Web.UI.Page
    {
        BarChart chart = new BarChart();
        protected void Page_Load(object sender, EventArgs e)
        {
            DrawChart();
        }

        private void DrawChart()
        {
            // get vote result 
            var result = VotesController.votes.GroupBy(v => v.Candidate_id, (key, group) => new { candidate_id = key, count = group.ToList().Count });
            chart.title = "Oylama Sonuçları"; 
            chart.addColumn("string", "Aday");
            chart.addColumn("number", "Oy Sayısı"); 
            chart.addColumnType("string", "style");
            int index = 0;
            foreach (var item in result)
            {
                var candidate = CandidatesController.candidates.Where(c => c.Id == item.candidate_id).FirstOrDefault();
                if (candidate != null)
                {
                    chart.addRow("'" + candidate.Name + "'," + item.count + ", '" + chart.colors[index].ToString() + "'");
                    index++;
                }
                if (index >= chart.colors.Count)
                {
                    index = 0;
                }
            } 
            ltScripts.Text = chart.generateChart(BarChart.ChartType.BarChart);
        }
    }
}