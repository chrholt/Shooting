using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooting
{
    public class Result
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string StevneID { get; set; }
        public string Type { get; set; }
        public string Results { get; set; }
    }

    
}
