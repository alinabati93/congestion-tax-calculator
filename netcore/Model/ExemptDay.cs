using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator.Model
{
    public class ExemptDay
    {
        public DateOnly Date { get; set; }
        public int CountOfFreeDaysAfter { get; set; }
        public int CountOfFreeDaysBefore { get; set; }
        private DateOnly _startDate = DateOnly.MinValue;
        public DateOnly StartDate
        {
            get
            {
                if (_startDate.Equals(DateOnly.MinValue))
                {
                    _startDate = Date.AddDays(-CountOfFreeDaysBefore);
                }
                return _startDate;
            }
        }

        private DateOnly _endDate = DateOnly.MinValue;
        public DateOnly EndDate
        {
            get
            {
                if (_endDate.Equals(DateOnly.MinValue))
                    _endDate = Date.AddDays(CountOfFreeDaysAfter);
                return _endDate;
            }
        }

    }
}
