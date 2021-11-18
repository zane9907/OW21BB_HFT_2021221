using OW21BB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.Logic
{
    public interface IPatientLogic
    {
        Patient GetHospitalById(int id);
        IEnumerable<Patient> GetAllBlogs();
        void AddNewHospital(Patient patient);
        void UpdateHospital(Patient patient);
        void DeleteHospital(Patient patient);

        //TODO NON-CRUD
    }
}
