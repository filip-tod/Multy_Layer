using Example.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service.Common
{
    public interface ILeagueService
    {
        
        Task<List<League>> Get();

        Task<List<League>> GetById(int id);

        Task<bool> Post(League league);

        Task<bool> Put(int id, League league);

        Task<bool> Delete (int id);
    }
}
