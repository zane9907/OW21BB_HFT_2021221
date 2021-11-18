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
        PatientRepository patientRepository;

        public DoctorLogic(DoctorRepository doctorRepository, PatientRepository patientRepository)
        {
            this.doctorRepository = doctorRepository;
            this.patientRepository = patientRepository;
        }


        public void AddNewDoctor(Doctor doctor)
        {
            doctorRepository.Create(doctor);
        }

        public IEnumerable<KeyValuePair<string, double?>> AVGAgeOfDoctorsPatients()
        {
            return (from x in patientRepository.GetAll()
                    group x by x.Doctor.Name into g
                    select new KeyValuePair<string, double?>
                    (
                        g.Key, g.Average(x => x.Age)
                    )).ToList();
        }

        public void DeleteDoctor(Doctor doctor)
        {
            doctorRepository.Delete(doctor);
        }

        public IEnumerable<Doctor> GetAllBlogs()
        {
            return doctorRepository.GetAll().ToList();
        }

        public Doctor GetDoctorById(int id)
        {
            return doctorRepository.Get(id);

            //TODO exception for id
        }

        public IEnumerable<KeyValuePair<string, string>> MostCommonDiseasesPerDoctor()
        {
            var sub = from x in patientRepository.GetAll()
                      group x by x.Doctor.Name into g
                      select new
                      {
                          KEY = g.Key,
                          DISEASE = g.Select(t => t.Disease)
                      };

            return from x in doctorRepository.GetAll()
                   join z in sub on x.Name equals z.KEY
                   let joinedItem = new { x.Name, z.DISEASE }
                   group joinedItem by joinedItem.Name into g
                   select new KeyValuePair<string, string>
                   (
                       g.Key, g.Max(t => t.DISEASE).ToString()
                   );
        }

        public void UpdateDoctor(Doctor doctor)
        {
            doctorRepository.Update(doctor);
        }

        IEnumerable<KeyValuePair<string, double>> IDoctorLogic.AVGAgeOfDoctorsPatients()
        {
            throw new NotImplementedException();
        }
    }
}
