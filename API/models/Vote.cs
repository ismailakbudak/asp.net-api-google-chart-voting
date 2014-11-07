using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.models
{
    public class Vote
    {
        public int Id { get; set; }
        public int Candidate_id { get; set; }
        public int User_id { get; set; }
         
    }
}