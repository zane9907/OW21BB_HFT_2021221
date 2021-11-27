using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public HospitalController(IHospitalLogic hospLogic)
        {
            this.hospLogic = hospLogic;
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

        // POST: /blog
        [HttpPost]
        public void Post([FromBody] Hospital value)
        {
            hospLogic.AddNewHospital(value);
        }

        // PUT: /blog
        [HttpPut]
        public void Put([FromBody] Hospital value)
        {
            hospLogic.UpdateHospital(value);
        }

        // Delete /blog/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            hospLogic.DeleteHospital(id);
        }

    }
}
