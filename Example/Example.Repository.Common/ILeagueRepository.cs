using Example.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Repository.Common
{
    public interface ILeagueRepository
    {
        List<League> Get();

        List<League> GetById(int id);
    }
}
