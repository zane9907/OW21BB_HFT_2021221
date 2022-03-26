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
    public class PatientController : ControllerBase
    {
        IPatientLogic patLogic;
        IHubContext<SignalRHub> hub;

        public PatientController(IPatientLogic patLogic, IHubContext<SignalRHub> hub)
        {
            this.patLogic = patLogic;
            this.hub = hub;
        }


        // GET: /patient
        [HttpGet]
        public IEnumerable<Patient> Get()
        {
            return patLogic.GetAllPatients();
        }

        // GET /patient/id
        [HttpGet("{id}")]
        public Patient Get(int id)
        {
            return patLogic.GetPatientById(id);
        }

        // POST /patient
        [HttpPost]
        public void Post([FromBody] Patient value)
        {
            patLogic.AddNewPatient(value);
            hub.Clients.All.SendAsync("PatientCreated", value);
        }

        // PUT /patient
        [HttpPut()]
        public void Put([FromBody] Patient value)
        {
            patLogic.UpdatePatient(value);
            hub.Clients.All.SendAsync("PatientUpdated", value);
        }

        // DELETE /patient/id
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var pat = patLogic.GetPatientById(id);
            patLogic.DeletePatient(id);
            hub.Clients.All.SendAsync("PatientDeleted", pat);
        }
    }
}
