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
    public class DoctorController : ControllerBase
    {
        IDoctorLogic docLogic;

        public DoctorController(IDoctorLogic docLogic)
        {
            this.docLogic = docLogic;
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
        }

        // PUT /doctor
        [HttpPut()]
        public void Put([FromBody] Doctor value)
        {
            docLogic.UpdateDoctor(value);
        }

        // DELETE /doctor/id
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            docLogic.DeleteDoctor(id);
        }
    }
}
