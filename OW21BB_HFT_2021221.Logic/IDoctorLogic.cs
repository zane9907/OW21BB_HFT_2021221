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
        IEnumerable<Doctor> GetAllBlogs();
        void AddNewHospital(Doctor doctor);
        void UpdateHospital(Doctor doctor);
        void DeleteHospital(Doctor doctor);

        //TODO NON-CRUD
    }
}
