using Shooting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Context;

namespace Web.Controllers
{
    public class DefaultController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        [HttpGet]
        public IHttpActionResult Get()
        {
            //try
            //{
                var query = from result in db.Results
                            select result.Name;

                if (query != null) { return Ok(query); }
                else { return NotFound(); }
            //}
            //catch(Exception ex)
            //{
            //    System.Diagnostics.Debug.WriteLine(ex.InnerException.Message);
                
            //    return InternalServerError();
            //}
        }

        [HttpPost]
        public IHttpActionResult SaveResult(Result r)
        {
            return Ok(r);
        }
    }
}
