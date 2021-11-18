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

        public void AddNewHospital(Hospital hospital)
        {
            hospitalRepo.Create(hospital);
        }

        public void DeleteHospital(Hospital t)
        {
            hospitalRepo.Delete(t);
        }

        public IEnumerable<Hospital> GetAllBlogs()
        {
            return hospitalRepo.ReadAll().ToList();
        }

        public Hospital GetHospitalById(int id)
        {
            return hospitalRepo.Read(id);

            //TODO exception
        }

        public void UpdateHospital(Hospital hospital)
        {
            hospitalRepo.Update(hospital);
        }
    }
}
