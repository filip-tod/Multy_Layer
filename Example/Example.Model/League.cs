using Example.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Model
{
    public class League : ILeague
    {
        public int Id { get; set; }
        public string Division { get; set; }
        public string Commissioner { get; set; }
    }
}
