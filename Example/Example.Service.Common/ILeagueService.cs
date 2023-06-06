using Example.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Example.Service.Common
{
    public interface ILeagueService
    {
        
        Task<List<League>> GetAll();

        Task<List<League>> GetById(int id);

        Task<bool> Post(League league);

        Task<bool> Put(int id, League league);

        Task<bool> Delete (int id);
    }
}
