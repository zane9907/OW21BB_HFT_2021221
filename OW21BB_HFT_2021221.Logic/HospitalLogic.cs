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
        HospitalRepository hospitalRepo;
        DoctorRepository doctorRepository;

        public HospitalLogic(HospitalRepository hospitalRepo, DoctorRepository doctorRepository)
        {
            this.hospitalRepo = hospitalRepo;
            this.doctorRepository = doctorRepository;
        }

        public void AddNewHospital(Hospital hospital)
        {
            hospitalRepo.Create(hospital);
        }

        public void DeleteHospital(Hospital t)
        {
            hospitalRepo.Delete(t);
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

        public IEnumerable<Hospital> GetAllBlogs()
        {
            return hospitalRepo.GetAll().ToList();
        }

        public Hospital GetHospitalById(int id)
        {
            return hospitalRepo.Get(id);

            //TODO exception for id
        }

        public void UpdateHospital(Hospital hospital)
        {
            hospitalRepo.Update(hospital);
        }
    }
}
