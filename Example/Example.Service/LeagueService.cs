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
        public List<League> Get()
        {
            try
            {
                LeagueRepository repository = new LeagueRepository();
                var result = repository.Get();
                return result;
            }
            catch (Exception ex) 
            {
                return null;
            }
            
        }
        public List<League> GetById(int id) 
        {
            try
            {
                LeagueRepository repository = new LeagueRepository();
                var result = repository.GetById(id);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
