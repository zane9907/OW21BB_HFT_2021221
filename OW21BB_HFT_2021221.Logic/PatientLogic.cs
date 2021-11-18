using OW21BB_HFT_2021221.Models;
using OW21BB_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.Logic
{
    public class PatientLogic : IPatientLogic
    {
        PatientRepository patientRepository;

        public PatientLogic(PatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }


        public void AddNewPatient(Patient patient)
        {
            patientRepository.Create(patient);
        }

        public void DeletePatient(Patient patient)
        {
            patientRepository.Delete(patient);
        }

        public IEnumerable<Patient> GetAllBlogs()
        {
            return patientRepository.GetAll().ToList();
        }

        public Patient GetPatientById(int id)
        {
            return patientRepository.Get(id);

            //TODO exception for id
        }

        public void UpdatePatient(Patient patient)
        {
            patientRepository.Update(patient);
        }
    }
}
