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
        Patient GetPatientById(int id);
        IEnumerable<Patient> GetAllPatients();
        void AddNewPatient(Patient patient);
        void UpdatePatient(Patient patient);
        void DeletePatient(Patient patient);

        bool IsDiseasePresent(string disease);


    }
}
