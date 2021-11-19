using OW21BB_HFT_2021221.Models;
using OW21BB_HFT_2021221.Data;
using OW21BB_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.Logic
{
    public interface IHospitalLogic
    {
        Hospital GetHospitalById(int id);
        IEnumerable<Hospital> GetAllHospitals();
        void AddNewHospital(Hospital hospital);
        void UpdateHospital(Hospital hospital);
        void DeleteHospital(Hospital id);

        

        IEnumerable<KeyValuePair<string, int>> DoctorSpecializatonCount();
        IEnumerable<KeyValuePair<string, int>> DoctorSpecializatonCountInSpecificHospital(int hospitalID);
        IEnumerable<KeyValuePair<string, int>> PatientsPerHospital();

    }
}
