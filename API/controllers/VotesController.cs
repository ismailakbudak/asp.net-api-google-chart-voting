using API.lib;
using API.models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace API.controllers
{
    public class VotesController : ApiController
    {

        public static int width = 1200;

        public static int height = 600; 

        public static List<Vote> votes = new List<Vote>() 
        {  
        };

        // GET api/votes
        public IEnumerable<Vote> Get()
        {
            return votes;
        }

        // GET api/votes/5
        public Vote Get(int id)
        {
            return votes.Where(c => c.Id == id).FirstOrDefault(); 
        }

        // POST api/votes 
        public string Post([FromBody] Vote value)
        {
            Vote val = new Vote();
            if (VotesController.votes.Count > 0)
            {
                  val = new Vote { Id = votes.Max(c => c.Id) + 1, Candidate_id = value.Candidate_id, User_id = value.User_id };
            }
            else { 
                  val = new Vote { Id =  1, Candidate_id = value.Candidate_id, User_id = value.User_id };
            }
            votes.Add(val);
            return GetResult();
        }

        // PUT api/votes/5
        public IEnumerable<Vote> Put(int id, [FromBody]Vote value)
        {
            Vote val = votes.Where(c => c.Id == id).FirstOrDefault();
            if (val != null)
            {
                val.Candidate_id = value.Candidate_id;
                val.User_id = value.User_id;
            }
            return votes;
        }

        // DELETE api/votes/5
        public IEnumerable<Vote> Delete(int id)
        {
            Vote val = votes.Where(c => c.Id == id).FirstOrDefault();
            votes.Remove(val);
            return votes;
        } 

        public string GetResult()
        {
            BarChart chart = new BarChart();
            var result = VotesController.votes.GroupBy(v => v.Candidate_id, (key, group) => new { candidate_id = key, count = group.ToList().Count });

            chart.addColumn("string", "Aday");
            chart.addColumn("number", "Oy Sayısı");

            foreach (var item in result)
            {
                var candidate = CandidatesController.candidates.Where(c => c.Id == item.candidate_id).FirstOrDefault();
                if (candidate != null)
                {
                    chart.addRowJson(candidate.Name, item.count.ToString());
                }
            } 		    
             
            return chart.JSon();

             
        }

        // DELETE api/votes/GetDeleteLast
        public string GetDeleteLast()
        {
            if (VotesController.votes.Count > 0)
            {
                Vote val = VotesController.votes.Last();
                if (val != null)
                {
                    VotesController.votes.Remove(val);
                }
            }
            return GetResult();
        }

        // DELETE api/votes/GetDeleteAll
        public string GetDeleteAll()
        {
            if (VotesController.votes.Count > 0)
            { 
                VotesController.votes.RemoveAll(c => c.Id > -1 );
               
            }
            return GetResult();
        }
         
         
    }
}