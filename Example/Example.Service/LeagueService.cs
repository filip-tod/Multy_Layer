using Example.common;
using Example.Model;
using Example.Repository;
using Example.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service
{
    public class LeagueService : ILeagueService
    {
        public async Task<List<League>> Get()
        {
            try
            {
                LeagueRepository repository = new LeagueRepository();
                var result = await repository.Get();
                return result;
            }
            catch (Exception ex) 
            {
                return null;
            }
            
        }
        public async Task<List<League>> GetById(int id) 
        {
            try
            {
                LeagueRepository repository = new LeagueRepository();
                var result = await repository.GetById(id);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<bool> Post(League league) 
        {
            LeagueRepository repository = new LeagueRepository();

            bool isCreated = await repository.Post(league);

            return isCreated;
        }
        public async Task<bool> Put(int id,League league)
        {
            try
            {
                LeagueRepository repository = new LeagueRepository();
                var result = await repository.Put(id, league);
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> Delete(int id)
        {
            LeagueRepository repository = new LeagueRepository();
            bool isDeleted = await repository.Delete(id);
            return isDeleted;
        }
        public async Task<List<League>> GetLeagues(int pageNumber, int pageSize, string sortBy)
        {
            try
            {
                LeagueRepository repository = new LeagueRepository();
                var result = await repository.GetLeagues(pageNumber, pageSize, sortBy);
                return result;
            }
            catch (Exception ex)
            {
                return null;  
            }
        }

    }
}

