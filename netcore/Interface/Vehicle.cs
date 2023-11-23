using congestion.calculator.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator.Interface
{
    public interface Vehicle
    {
        VehicleType VehicleType { get; set; }
    }
}