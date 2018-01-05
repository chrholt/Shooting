using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooting
{
    public class Result
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        //Name of the result
        public string Name { get; set; }
        //Date for the result
        public DateTime Date { get; set; }
        //Property for ID - primary for NJFF's ID for their competitions
        public string StevneID { get; set; }
        //Type of result (Jaktfelt/Figurjakt/Jegertrap)
        public string Type { get; set; }
        //JSON of different results - different depending on result type
        public string Results { get; set; }
        //Property to mark changes or if it's a new Result (used for synchronization between local and remote database)
        public bool IsDirty { get; set; }
    }

    
}
