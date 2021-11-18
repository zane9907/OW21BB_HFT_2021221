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

        public IEnumerable<KeyValuePair<string, int>> AllDiseasesPerDoctor()
        {
            //var dlist = patientRepository.GetAll().Select(x => x.Disease).ToList();


            var sub = from x in patientRepository.GetAll()
                      group x by x.Disease into g
                      select new
                      {
                          DISEASE = g.Key,
                          COUNT = g.Count(),
                          DOCTOR_ID = g.Select(y=>y.DoctorID).SingleOrDefault()
                      };



            //return null;
            return (from x in doctorRepository.GetAll()
                    join z in sub on x.DoctorID equals z.DOCTOR_ID
                    let joinedItem = new { x.DoctorID, x.Name, z.DISEASE }
                    group joinedItem by joinedItem.Name into g
                    select new KeyValuePair<string, int>
                    (
                       g.Key, g.Select(y=>y.DISEASE).Count()

                    )).ToList();

            //return (from x in patientRepository.GetAll()
            //        group x by x.Doctor.Name into g
            //        select new KeyValuePair<string, IEnumerable<string>>
            //        (
            //            g.Key, dlist
            //        )).ToList();

        }

        public void UpdateDoctor(Doctor doctor)
        {
            doctorRepository.Update(doctor);
        }


        public IEnumerable<KeyValuePair<string, int>> DiseasePerDoctor(string disease)
        {
            return (from x in patientRepository.GetAll()
                      group x by x.Doctor.Name into g
                      select new KeyValuePair<string, int>
                      (
                          g.Key, g.Where(y=>y.Disease.Equals(disease)).Count()
                      )).ToList();
        }
    }
}
