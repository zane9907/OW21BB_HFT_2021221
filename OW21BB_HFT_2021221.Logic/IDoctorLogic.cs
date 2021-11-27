using OW21BB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.Logic
{
    public interface IDoctorLogic
    {
        Doctor GetDoctorById(int id);
        IEnumerable<Doctor> GetAllDoctors();
        void AddNewDoctor(Doctor doctor);
        void UpdateDoctor(Doctor doctor);
        void DeleteDoctor(int id);

        

        IEnumerable<KeyValuePair<string, double>> AVGAgeOfDoctorsPatients();
        IEnumerable<IEnumerable<string>> AllDiseasesPerDoctor();
        IEnumerable<KeyValuePair<string, int>> DiseasePerDoctor(string disease);


    }
}
