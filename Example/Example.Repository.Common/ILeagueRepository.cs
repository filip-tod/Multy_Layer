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
        Task<List<League>> GetAll();

        Task<List<League>> GetById(int id);

        Task<bool> Post(League league);
        Task<bool> Put(int id, League league);
        Task<bool> Delete(int id);


    }
}
