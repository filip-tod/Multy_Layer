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
using System.Drawing.Printing;
using System.Globalization;
using System;
using Microsoft.TeamFoundation.Work.WebApi;
using Example.Service.Common;

namespace Example.WebApi
{
    
    public class LeagueController : ApiController
    {
        private ILeagueService LeagueService { get; set; }
        public LeagueController(ILeagueService service) 
        {
            LeagueService = service;
        }
        //radi 
        public async Task<HttpResponseMessage> GetAll()
        {
            
            List<League> result = await LeagueService.GetAll();
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not find you'r league");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
       
        public async Task<HttpResponseMessage> GetById(int id)
        {
            
            List<League> result = await LeagueService.GetById(id);
            if(result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not find you'r league");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
      
        public async Task<HttpResponseMessage> Post(League League)
        {
            
            bool result = await LeagueService.Post(League);
            if (result == false)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not add new league");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
      
        public async Task<HttpResponseMessage> Put(int id,League League)
        {
           
           bool result =await LeagueService.Put(id, League);
            if (result != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not edit your coupon");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        public async Task<HttpResponseMessage> Delete(int id)
        {
            
            bool result =await LeagueService.Delete(id);
            if(result ==  false)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            return Request.CreateResponse(HttpStatusCode.OK, "League was not found and not Deleted");
                   
        }
    }
}

