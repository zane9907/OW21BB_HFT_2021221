using OW21BB_HFT_2021221.Models;
using OW21BB_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.Logic
{
    public class HospitalLogic : IHospitalLogic
    {
        IRepository<Hospital> hospitalRepo;
        IRepository<Doctor> doctorRepository;
        IRepository<Patient> patientRepository;

        public HospitalLogic(IRepository<Hospital> hospitalRepo, IRepository<Doctor> doctorRepository, IRepository<Patient> patientRepository)
        {
            this.hospitalRepo = hospitalRepo;
            this.doctorRepository = doctorRepository;
            this.patientRepository = patientRepository;
        }

        public void AddNewHospital(Hospital hospital)
        {
            hospitalRepo.Create(hospital);
        }

        public void DeleteHospital(int id)
        {
            hospitalRepo.Delete(id);
        }

        public IEnumerable<KeyValuePair<string, int>> DoctorSpecializatonCount()
        {
            return (from x in doctorRepository.GetAll()
                    group x by x.Specialization into g
                    select new KeyValuePair<string, int>
                    (
                        g.Key, g.Count()
                    )).ToList();
        }

        public IEnumerable<KeyValuePair<string, int>> DoctorSpecializatonCountInSpecificHospital(int hospitalID)
        {
            var specCount = (from x in doctorRepository.GetAll()
                       where x.HospitalID.Equals(hospitalID)
                       group x by x.Specialization into g
                       select new KeyValuePair<string, int>
                       (
                           g.Key, g.Count()
                       )).ToList();

            return specCount;
        }

        public IEnumerable<Hospital> GetAllHospitals()
        {
            var hospitalList = hospitalRepo.GetAll().ToList();
            if (hospitalList.Count != 0)
            {
                return hospitalList;
            }
            else
            {
                throw new ListIsEmptyException("GetAllDoctors");
            }
        }

        public Hospital GetHospitalById(int id)
        {        


            int hospitalCount = hospitalRepo.GetAll().Count();
            if (id <= hospitalCount)
            {
                return hospitalRepo.Get(id);
            }
            else
            {
                throw new IndexOutOfRangeException("{ERROR} ID was too big!");
            }


        }

        public IEnumerable<KeyValuePair<string, int>> PatientsPerHospital()
        {
            var sub = from x in patientRepository.GetAll()
                      group x by x.DoctorID into g
                      select new
                      {
                          DOCTOR_ID = g.Key,
                          PAT_NO = g.Count()
                      };

            var query = (from x in doctorRepository.GetAll()
                         join z in sub on x.DoctorID equals z.DOCTOR_ID
                         let joinedItem = new { x.DoctorID, x.Name, z.PAT_NO }
                         join y in hospitalRepo.GetAll() on x.HospitalID equals y.HospitalID
                         let yes = new { y.Name, z.PAT_NO }
                         group yes by yes.Name into g
                         select new KeyValuePair<string, int>
                         (
                            g.Key, g.Sum(a => a.PAT_NO)
                         )).ToList();


            return query;
        }

        public void UpdateHospital(Hospital hospital)
        {
            hospitalRepo.Update(hospital);
        }
    }
}
