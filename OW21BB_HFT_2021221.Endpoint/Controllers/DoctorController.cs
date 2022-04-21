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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OW21BB_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        IDoctorLogic docLogic;
        IHubContext<SignalRHub> hub;

        public DoctorController(IDoctorLogic docLogic, IHubContext<SignalRHub> hub)
        {
            this.docLogic = docLogic;
            this.hub = hub;
        }


        // GET: /doctor
        [HttpGet]
        public IEnumerable<Doctor> Get()
        {
            return docLogic.GetAllDoctors();
        }

        // GET /doctor/id
        [HttpGet("{id}")]
        public Doctor Get(int id)
        {
            return docLogic.GetDoctorById(id);
        }

        // POST /doctor
        [HttpPost]
        public void Post([FromBody] Doctor value)
        {
            docLogic.AddNewDoctor(value);
            hub.Clients.All.SendAsync("DoctorCreated", value);

        }

        // PUT /doctor
        [HttpPut()]
        public void Put([FromBody] Doctor value)
        {
            docLogic.UpdateDoctor(value);
            hub.Clients.All.SendAsync("DoctorUpdated", value);

        }

        // DELETE /doctor/id
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var doc = docLogic.GetDoctorById(id);
            docLogic.DeleteDoctor(id);
            hub.Clients.All.SendAsync("DoctorDeleted", doc);

        }
    }
}
