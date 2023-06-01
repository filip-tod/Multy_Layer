using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Example.Service;
using Example.Model;

namespace Example.WebApi
{
    [RoutePrefix("api/league")]
    public class LeagueController : ApiController
    {
        [HttpGet]
        [Route("")]
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
        [HttpGet]
        [Route("/1")]
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
        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post(League League)
        {
            LeagueService service = new LeagueService();
            List<League> result = service.Post(League);
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not a new league");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
