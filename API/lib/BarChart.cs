using API.controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.lib
{
    /// <summary>
    /// Google chart api 
    /// For extra info look: https://developers.google.com/chart/interactive/docs/gallery/columnchart
    /// </summary>
    public class BarChart
    {
        // Fields
        private string data = "";
        private string cols = "";
        private string rows = "";
        private string roles = ""; 
        private string javascript;

        // Properties
        public string elementId { get; set; }

        public string hAxisTitle { get; set; }

        public string vAxisTitle { get; set; }
         
          
        public string title { get; set; }

        
        // ChartTypes
        public enum ChartType
        {
            BarChart,
            PieChart,
            LineChart,
            ColumnChart,
            AreaChart,
            BubbleChart,
            CandlestickChart,
            ComboChart,
            GeoChart,
            ScatterChart,
            SteppedAreaChart,
            TableChart
        }
        // Color types
        public Hashtable colors = new Hashtable();
         
        /*
        { 
            black,
            blue, 
            red, 
            green, 
            yellow, 
            gray,
            orange,
            azure
        };
        */
        // Methods
        public BarChart()
        {
            this.title = "Google Chart"; 
            this.elementId = "chart_div";
            colors.Add(0, "#5F04B4");
            colors.Add(1, "blue");
            colors.Add(2, "red");
            colors.Add(3, "green");
            colors.Add(4, "yellow");
            colors.Add(5, "orange");
            colors.Add(6, "#8000FF");
            colors.Add(7, "##58FA82");
            colors.Add(8, "#FE9A2E");
            colors.Add(9, "#D358F7");
            colors.Add(10, "#2EFEF7");
            colors.Add(11, "#80FF00");
            colors.Add(12, "#DF01D7");
            colors.Add(13, "#FE2E64");
            colors.Add(14, "#2EFE9A");
            colors.Add(15, "gold");
            colors.Add(16, "gray");

        }

        public void addColumn(string type, string columnName)
        {
            string data = this.data;
            this.data = data + "data.addColumn('" + type + "', '" + columnName + "');";
            this.cols = this.cols + " {'type': '" + type + "', 'val': '" + columnName + "'}, "; 
        }
        
        public void addColumnType(string type,  string columnName)
        {
            string data = this.data;
            this.data = data + "data.addColumn({ type: '" + type + "', role: '" + columnName + "'});"; 
            this.roles = this.roles + " {'type': '" + type + "', 'role': '" + columnName + "'}, ";
        }


        /*
         var json = { 
		       cols: [ {'type': 'string', val: 'aday' }, {'type': 'number', val: 'Oy Sayisi' }    ],
		    rows: [ { 'name': 'ismail', val: 200 }, { 'name': 'ismail3', val: 33 } ]
		 }
        */
        public void addRow(string value)
        {
            this.data = this.data + "data.addRow([" + value + "]);";  
        }

        public void addRowJson(string name, string value, string color)
        { 
            this.rows = this.rows + "{ 'name': '" + name + "', 'val' : " + value+", 'color' : '" +color+ "' },";
        }

        public string JSon() { 
            return   "{ 'cols': [ "+ this.cols +" ]," +
                      " 'roles': [ "+ this.roles +" ]," +
                     "  'rows': [ "+ this.rows +" ] " +
                     "}".Replace("'", "\"");
        }
         
        public string generateChart(ChartType chart)
        {
            this.javascript = "<script type=\"text/javascript\" src=\"https://www.google.com/jsapi\"></script>";
            this.javascript = this.javascript + "<script type=\"text/javascript\">";
            this.javascript = this.javascript + "google.load('visualization', '1.0', { 'packages': ['corechart']});";
            this.javascript = this.javascript + "google.setOnLoadCallback(drawChart);";
            this.javascript = this.javascript + "var data, options, chart,  view; ";
            this.javascript = this.javascript + "function drawChart() {";
            this.javascript = this.javascript + "data = new google.visualization.DataTable();";
            this.javascript = this.javascript + this.data;
            this.javascript = this.javascript + "options = {";
            this.javascript = this.javascript + "'title': '" + this.title + "',"; 
            this.javascript = this.javascript + "vAxis: {title: '" + this.vAxisTitle + "' },";
            this.javascript = this.javascript + "hAxis: {title: '" + this.hAxisTitle + "' },"; 
            
  
            object javascript = this.javascript;
            this.javascript = string.Concat(new object[] { javascript, "'width': ", VotesController.width, ", 'height': ", VotesController.height, "};" });
            string str = this.javascript;
            this.javascript = str + "chart = new google.visualization." + chart.ToString() + "(document.getElementById('" + this.elementId + "'));";
            this.javascript = this.javascript + "view = new google.visualization.DataView(data);                                              ";
            this.javascript = this.javascript + "view.setColumns([0, 1, { sourceColumn: 1, role: 'annotation' },{ sourceColumn: 2, role: 'style' }]);                            "; 
            this.javascript = this.javascript + "chart = new google.visualization.BarChart(document.getElementById('" + this.elementId + "'));";
            this.javascript = this.javascript + "chart.draw(view, options);	                                                                  ";
            this.javascript = this.javascript + "};"+
                "function setData(jsonData){     data =  new google.visualization.DataTable(jsonData);     " +
                "};                                                  "+
	            "function getData(){                                 "+
                "    return data;		                             "+
                "};                                                  "+ 
                "function updateChart() {                            "+
                "    data = getData();                               "+
                "    chart.draw(data, options);                      "+
                "};                                                  "+
                " function setChart(json){ console.dir(json);                          "+ 
	            "    data = new google.visualization.DataTable();    "+
                "    var json =  json;   for(var i=0; i<  json.cols.length; i++   ){     " +
			    "        var col =  json.cols[i];                    "+
			    "        data.addColumn( col.type, col.val );        "+
			    "    };                                              "+
                "    for(var i=0; i<  json.roles.length; i++   ){     " +
                "        var role =  json.roles[i];                    " +
                "        data.addColumn({ type: role.type, role: role.role} );        " +
			    "    };                                              "+
			    "    var arr = [];                                   "+
                "    for(var i=0; i<  json.rows.length; i++   ){     "+
			    "        var col =  json.rows[i];                    "+
                "        arr.push([ col.name,  col.val, col.color  ]);          " +
			    "    };                                              "+
			    "    data.addRows(arr);                             "+ 
			    "    view = new google.visualization.DataView(data);                                                 " +
                "    view.setColumns([0, 1, { sourceColumn: 1, role: 'annotation' }, { sourceColumn: 2, role: 'style' }]);" + 
			    "    chart = new google.visualization.BarChart(document.getElementById('" + this.elementId + "'));   " +
			    "    chart.draw(view, options);	                                                                     " +
                "};                                                   "+
                "</script>";
            return this.javascript;
        }


    }
}