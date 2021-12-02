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
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IHospitalLogic hospLogic;
        IDoctorLogic docLogic;
        IPatientLogic patLogic;

        public StatController(IHospitalLogic hospLogic, IDoctorLogic docLogic, IPatientLogic patLogic)
        {
            this.hospLogic = hospLogic;
            this.docLogic = docLogic;
            this.patLogic = patLogic;
        }

        [HttpGet] //stat/avgageofdoctorspatients
        public IEnumerable<KeyValuePair<string, double>> AVGAgeOfDoctorsPatients()
        {
            return docLogic.AVGAgeOfDoctorsPatients();
        }

        [HttpGet] //stat/doctorspecializatoncount
        public IEnumerable<KeyValuePair<string, int>> DoctorSpecializatonCount()
        {
            return hospLogic.DoctorSpecializatonCount();
        }

        [HttpGet("{disease}")] //stat/diseaseperdoctor/{disease}
        public IEnumerable<KeyValuePair<string, int>> DiseasePerDoctor(string disease)
        {
            return docLogic.DiseasePerDoctor(disease);
        }        

        [HttpGet] //stat/patientsperhospital
        public IEnumerable<KeyValuePair<string, int>> PatientsPerHospital()
        {
            return hospLogic.PatientsPerHospital().ToList();
        }

        [HttpGet("{hospitalID}")] //stat/doctorspecializatoncountinspecifichospital/{hospitalID}
        public IEnumerable<KeyValuePair<string, int>> DoctorSpecializatonCountInSpecificHospital(int hospitalID)
        {
            return hospLogic.DoctorSpecializatonCountInSpecificHospital(hospitalID);
        }

        [HttpGet("{disease}")] //stat/IsDiseasePresent/{disease}
        public bool IsDiseasePresent(string disease)
        {
            return patLogic.IsDiseasePresent(disease);
        }

        [HttpGet] //stat/alldiseasesperdoctor
        public IEnumerable<IEnumerable<string>> AllDiseasesPerDoctor()
        {
            var alldisease = docLogic.AllDiseasesPerDoctor();
            //var doctorNames = docLogic.GetAllDoctors().Select(x => x.Name).ToList();

            //KeyValuePair<List<string>, IEnumerable<IEnumerable<string>>> diseasesPerDoctor =
            //    new KeyValuePair<List<string>, IEnumerable<IEnumerable<string>>>(doctorNames, asd0);


            return alldisease;
        }
    }
}
