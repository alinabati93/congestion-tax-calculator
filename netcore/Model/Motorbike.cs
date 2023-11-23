using congestion.calculator.Enum;
using congestion.calculator.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator.Model
{
    public class Motorbike : Vehicle
    {
        public VehicleType VehicleType { get; set; } = VehicleType.Motorcycle;

    }
}