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
        IRepository<Doctor> doctorRepository;
        IRepository<Doctor> patientRepository;

        public DoctorLogic(IRepository<Doctor> doctorRepository, IRepository<Patient> patientRepository)
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

            if (id <= doctorRepository.GetAll().Count())
            {
                return doctorRepository.Get(id);
            }
            else
            {
                throw new IndexOutOfRangeException("{ERROR} ID was too  big!");
            }

            //TODO exception for id
        }

        public IEnumerable<IEnumerable<string>> AllDiseasesPerDoctor()
        {


            return doctorRepository.GetAll().Select(x => x.Patients.Select(y => y.Disease).ToList()).ToList();


            //TESTS

            //var dlist = patientRepository.GetAll().Select(x => x.Disease).ToList();
            //var doctorName = doctorRepository.GetAll().Select(x => x.Name).ToList();

            //;       



            //;

            //var asd = (from x in doctorRepository.GetAll()
            //           group x by x.Name into g
            //           select new
            //           {
            //               KEY = g.Key,
            //               DISEASE = g.Select(x => x.Patients.Select(y => y.Disease).ToList()).ToList()
            //           }).ToList();


            //var asd = doctors.Select(x => x.Patients.Max(y => y.Disease));

            //;
            //var sub = from x in patientRepository.GetAll()
            //          group x by x.Disease into g
            //          select new
            //          {
            //              DISEASE = g.Key,
            //              COUNT = g.Count(),
            //              DOCTOR_ID = g.Select(y => y.DoctorID).SingleOrDefault()
            //          };



            //return null;
            //return (from x in doctorrepository.getall()
            //        join z in sub on x.doctorid equals z.doctor_id
            //        let joineditem = new { x.doctorid, x.name, z.disease }
            //        group joineditem by joineditem.name into g
            //        select new keyvaluepair<string, int>()
            //        {

            //        }


            //return (from x in patientrepository.getall()
            //        group x by x.doctor.name into g
            //        select new keyvaluepair<string, ienumerable<string>>
            //        (
            //            g.key, dlist
            //        )).tolist();

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
                        g.Key, g.Where(y => y.Disease.Equals(disease)).Count()
                    )).ToList();
        }
    }
}
