using Example.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.common
{
    public class Sortingt<T> : List<T>
    {
        public string divisionFilter { get; set; }
        public string commissionerFilter { get; set; }

    }
}
