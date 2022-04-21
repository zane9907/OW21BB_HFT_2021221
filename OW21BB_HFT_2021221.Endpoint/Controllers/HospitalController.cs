using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OW21BB_HFT_2021221.Endpoint.Services;
using OW21BB_HFT_2021221.Logic;
using OW21BB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        IHospitalLogic hospLogic;
        IHubContext<SignalRHub> hub;

        public HospitalController(IHospitalLogic hospLogic, IHubContext<SignalRHub> hub)
        {
            this.hospLogic = hospLogic;
            this.hub = hub;
        }

        //GET: /hospital
        [HttpGet]
        public IEnumerable<Hospital> Get()
        {
            return hospLogic.GetAllHospitals();
        }


        // GET: /hospital/{id}
        [HttpGet("{id}")]
        public Hospital Get(int id)
        {
            return hospLogic.GetHospitalById(id);
        }

        // POST: /hospital
        [HttpPost]
        public void Post([FromBody] Hospital value)
        {
            hospLogic.AddNewHospital(value);
            hub.Clients.All.SendAsync("HospitalCreated", value);

        }

        // PUT: /hospital
        [HttpPut]
        public void Put([FromBody] Hospital value)
        {
            hospLogic.UpdateHospital(value);
            hub.Clients.All.SendAsync("HospitalUpdated", value);

        }

        // Delete /hospital/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var hosp = hospLogic.GetHospitalById(id);
            hospLogic.DeleteHospital(id);
            hub.Clients.All.SendAsync("HospitalDeleted", hosp);

        }

    }
}
