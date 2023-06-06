using Example.common;
using Example.Model;
using Example.Repository;
using Example.Repository.Common;
using Example.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Example.Service
{
    public class LeagueService : ILeagueService
    {
        private ILeagueRepository LeagueRepository { get; set; }    
        private LeagueService (ILeagueRepository repo)
        {
            LeagueRepository = repo;

        }
        public async Task<List<League>> GetAll()
        {
            try
            {
               
                var result = await LeagueRepository.GetAll();
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
                var result = await LeagueRepository.GetById(id);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<bool> Post(League league) 
        {
            bool isCreated = await LeagueRepository.Post(league);

            return isCreated;
        }
        public async Task<bool> Put(int id,League league)
        {
            try
            {
                var result = await LeagueRepository.Put(id, league);
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> Delete(int id)
        {
            bool isDeleted = await LeagueRepository.Delete(id);
            return isDeleted;
        }
       

    }
}

