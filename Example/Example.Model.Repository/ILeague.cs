using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Model.Repository
{
    public interface ILeague
    {
        int Id { get; set; }
        string Division { get; set; }
        string Commissioner { get; set; }
    }
}
