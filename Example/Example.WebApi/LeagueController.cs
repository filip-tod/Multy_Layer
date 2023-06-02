using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using Example.Service;
using Example.Model;

namespace Example.WebApi
{
    
    public class LeagueController : ApiController
    {
        
        public HttpResponseMessage Get()
        {
            LeagueService service = new LeagueService();
            List<League> result = service.Get();
            if(result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not find you'r league");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
       
        public HttpResponseMessage GetById(int id)
        {
            LeagueService service = new LeagueService();
            List<League> result = service.GetById(id);
            if(result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not find you'r league");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
      
        public HttpResponseMessage Post(League League)
        {
            LeagueService service = new LeagueService();
            bool result = service.Post(League);
            if (result == false)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not add new league");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
      
        public HttpResponseMessage Put(int id,League League)
        {
            LeagueService service = new LeagueService();
           bool result = service.Put(id, League);
            if (string.IsNullOrWhiteSpace(League.Division) || string.IsNullOrWhiteSpace(League.Commissioner))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not edit your coupon");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        public HttpResponseMessage Delete(int id)
        {
            LeagueService service = new LeagueService();
            bool result = service.Delete(id);
            if(result ==  false)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "League was not found and not Deleted");
                   
        }
    }
}

