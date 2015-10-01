using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TrailerType
    {
        public const double STOR = 51.0;
        public double Capacity { get; private set; }
        
        public TrailerType(double capacity)
        {
            this.Capacity = capacity;
        }
    }
}


