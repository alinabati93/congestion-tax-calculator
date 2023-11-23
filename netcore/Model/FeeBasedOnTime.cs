using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator.Model
{
    public class FeeBasedOnTime
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int Fee { get; set; }
    }
}
