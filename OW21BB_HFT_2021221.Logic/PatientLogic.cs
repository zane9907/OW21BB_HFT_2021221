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
        IRepository<Patient> patientRepository;

        public PatientLogic(IRepository<Patient> patientRepository)
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

        public IEnumerable<Patient> GetAllPatients()
        {
            var patientList = patientRepository.GetAll().ToList();
            if (patientList.Count != 0)
            {
                return patientList;
            }
            else
            {
                throw new ListIsEmptyException("GetAllPatients");
            }

        }

        public Patient GetPatientById(int id)
        {
            if (id <= patientRepository.GetAll().Count())
            {
                return patientRepository.Get(id); 
            }
            else
            {
                throw new IndexOutOfRangeException("{ERROR} ID was too big!");
            }

            
        }

        public void UpdatePatient(Patient patient)
        {
            patientRepository.Update(patient);
        }

        public bool IsDiseasePresent(string disease)
        {
            var isPresent = patientRepository.GetAll().Any(x => x.Disease.Contains(disease));
            if (isPresent)
            {
                return isPresent;
            }
            else
            {
                throw new DiseaseIsNotPresentException(disease);
            }
        }
    }
}
