using OW21BB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.Logic
{
    public interface IDoctorLogic
    {
        Doctor GetHospitalById(int id);
        IEnumerable<Hospital> GetAllBlogs();
        void AddNewHospital(Hospital hospital);
        void UpdateHospital(Hospital hospital);
        void DeleteHospital(Hospital id);

        //TODO NON-CRUD
    }
}
