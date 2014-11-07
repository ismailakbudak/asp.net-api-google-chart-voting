using API.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API.controllers
{
    public class CandidatesController : ApiController
    {
        public static List<Candidate> candidates = new List<Candidate>() 
        {  
        };

        // GET api/candidates
        public IEnumerable<Candidate> Get()
        {
            return candidates;
        }

        // GET api/candidates/5
        public Candidate Get(int id)
        {
            return candidates.Where(c => c.Id == id).FirstOrDefault();
            // another function
            // return (from inc in candidates where inc.Id == id select inc).FirstOrDefault();
        }

        // POST api/candidates 
        public IEnumerable<Candidate> Post([FromBody] Candidate value)
        {
            if (value.Name != null && value.Name.ToString() != "")
            {
                Candidate val = new Candidate();
                if (CandidatesController.candidates.Count > 0)
                {
                    val = new Candidate { Id = candidates.Max(c => c.Id) + 1, Name = value.Name };
                }
                else
                {
                    val = new Candidate { Id = 1, Name = value.Name };
                }
                candidates.Add(val);
            }
            return candidates;
        }

        // PUT api/candidates/5
        public IEnumerable<Candidate> Put(int id, [FromBody]Candidate value)
        {
            Candidate val = candidates.Where(c => c.Id == id).FirstOrDefault();
            val.Name = value.Name; 
            return candidates;
        }

        // DELETE api/candidates/5
        public IEnumerable<Candidate> Delete(int id)
        {
            Candidate val = candidates.Where(c => c.Id == id).FirstOrDefault();
            if (val != null)
            {
                candidates.Remove(val);
                VotesController.votes.RemoveAll(c => c.Candidate_id == val.Id);
            }
            return candidates;
        }

        // DELETE api/candidates/GetDeleteLast
        public string GetDeleteLast()
        {
            if (CandidatesController.candidates.Count > 0)
            {
                Candidate val = CandidatesController.candidates.Last();
                if (val != null)
                {
                    CandidatesController.candidates.Remove(val);
                    VotesController.votes.RemoveAll(c => c.Candidate_id == val.Id   );
                }
            }
            return "ok";
        }

        // DELETE api/candidates/GetDeleteAll
        public string GetDeleteAll()
        {
            if (CandidatesController.candidates.Count > 0)
            {
                CandidatesController.candidates.RemoveAll(c => c.Id > -1);
                VotesController.votes.RemoveAll(c => c.Id > -1);
            }
            return "ok";
        }
    }
}