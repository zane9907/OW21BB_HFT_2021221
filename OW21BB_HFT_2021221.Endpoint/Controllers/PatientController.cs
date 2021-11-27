using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public PatientController(IPatientLogic patLogic)
        {
            this.patLogic = patLogic;
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
        }

        // PUT /patient
        [HttpPut()]
        public void Put([FromBody] Patient value)
        {
            patLogic.UpdatePatient(value);
        }

        // DELETE /patient/id
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            patLogic.DeletePatient(id);
        }
    }
}
