﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooting.Models
{
    public class JaktfeltResult
    {
        public int Hits { get; set; }
        public int InnerHits { get; set; }
        public ObservableCollection<JaktfeltPost> Posts { get; set; }
    }
}
