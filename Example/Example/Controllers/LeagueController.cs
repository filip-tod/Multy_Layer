using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using Example.Service;
using Example.Model;
using System.Threading.Tasks;
using Example.common;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Results;

namespace Example.WebApi
{
    
    public class LeagueController : ApiController
    {
        //radi 
        public async Task<HttpResponseMessage> Get()
        {
            LeagueService service = new LeagueService();
            List<League> result = await service.Get();
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not find you'r league");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
       
        public async Task<HttpResponseMessage> GetById(int id)
        {
            LeagueService service = new LeagueService();
            List<League> result = await service.GetById(id);
            if(result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not find you'r league");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
      
        public async Task<HttpResponseMessage> Post(League League)
        {
            LeagueService service = new LeagueService();
            bool result = await service.Post(League);
            if (result == false)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not add new league");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
      
        public async Task<HttpResponseMessage> Put(int id,League League)
        {
            LeagueService service = new LeagueService();
           bool result =await service.Put(id, League);
            if (result != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not edit your coupon");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        public async Task<HttpResponseMessage> Delete(int id)
        {
            LeagueService service = new LeagueService();
            bool result =await service.Delete(id);
            if(result ==  false)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            return Request.CreateResponse(HttpStatusCode.OK, "League was not found and not Deleted");
                   
        }

        public async Task<HttpResponseMessage> GetLeagues(int pageNumber, int pageSize, string sortBy)
        {
            LeagueService leagueService = new LeagueService();  
            var leagues = await leagueService.GetLeagues(pageNumber, pageSize, sortBy);  
           if(leagues == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "something went wrong");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "League was not found and not Deleted");
        }
    }
}

