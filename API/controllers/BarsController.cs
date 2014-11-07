using API.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API.controllers
{
    public class BarsController: ApiController
    {
        // GET api/bars/GetIncreaseWidth
        public string GetIncreaseWidth()
        {
            VotesController.width += 100;
            return VotesController.width.ToString();
        }

        // GET api/bars/GetDecreaseWidth
        public string GetDecreaseWidth()
        {
            VotesController.width -= 100;
            return VotesController.width.ToString();
        }

        // GET api/bars/GetIncreaseHeight
        public string GetIncreaseHeight()
        {
            VotesController.height += 100;
            return VotesController.height.ToString();
        }

        // GET api/bars/GetDecreaseHeight
        public string GetDecreaseHeight()
        {
            VotesController.height -= 100;
            return VotesController.height.ToString();
        }
         
    }
}