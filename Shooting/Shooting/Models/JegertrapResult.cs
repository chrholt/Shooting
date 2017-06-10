using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Shooting.Models
{
    public class JegertrapResult
    {
        public int AchievablePoints { get; set; }
        public int AchievedPoints { get; set; }
        public ObservableCollection<JegertrapSeries> Series {get;set;}
    }
}
