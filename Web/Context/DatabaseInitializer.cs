using Shooting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Web.Context
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {

        protected override void Seed(DatabaseContext context)
        {
            base.Seed(context);
            //DUMMY DATA - HARDCODED
            var result1 = new Result
            {
                Date = DateTime.Now,
                Name = "hardcoded1",
                Results = "",
                StevneID = "hc1",
                Type = "Jaktfelt"
            };
            var result2 = new Result
            {
                Date = DateTime.Now,
                Name = "hardcoded2",
                Results = "",
                StevneID = "hc2",
                Type = "Jaktfelt"
            };
            context.Results.Add(result1);
            context.Results.Add(result2);
            context.SaveChanges();
            //END OF DUMMY DATA
        }
    }
}