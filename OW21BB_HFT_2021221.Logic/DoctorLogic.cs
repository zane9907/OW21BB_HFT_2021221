using OW21BB_HFT_2021221.Models;
using OW21BB_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.Logic
{
    public class DoctorLogic : IDoctorLogic
    {
        DoctorRepository doctorRepository;

        public DoctorLogic(DoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }

        public void AddNewHospital(Doctor doctor)
        {
            doctorRepository.Create(doctor);
        }

        public void DeleteHospital(Doctor doctor)
        {
            doctorRepository.Delete(doctor);
        }

        public IEnumerable<Doctor> GetAllBlogs()
        {
            return doctorRepository.GetAll().ToList();
        }

        public Doctor GetHospitalById(int id)
        {
            return doctorRepository.Get(id);

            //TODO exception for id
        }

        public void UpdateHospital(Doctor doctor)
        {
            doctorRepository.Update(doctor);
        }
    }
}
