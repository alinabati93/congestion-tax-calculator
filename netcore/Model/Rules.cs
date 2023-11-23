using congestion.calculator.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator.Model
{
    public class Rules
    {
        public List<FeeBasedOnTime> Fees = new List<FeeBasedOnTime>();
        public List<ExemptDay> tollFreeDaysOfYear = new List<ExemptDay>();
        public List<DayOfWeek> tollFreeDaysOfWeek = new List<DayOfWeek>();
        public List<int> tollFreeMonths = new List<int>();
        public List<VehicleType> tollFreeVehicles = new List<VehicleType>();
        public int maxFeeInDay = 60;
    }
}
