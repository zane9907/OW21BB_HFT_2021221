using OW21BB_HFT_2021221.Data;
using OW21BB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.Repository
{
    public class PatientRepository : Repository<Patient>, IRepository<Patient>
    {
        public PatientRepository(HospitalDbContext hpctx) : base(hpctx)
        {
        }

        public override void Create(Patient t)
        {
            hpctx.Patients.Add(t);
            hpctx.SaveChanges();
        }

        public override void Delete(int id)
        {
            var patientDelete = Get(id);
            hpctx.Patients.Remove(patientDelete);
            hpctx.SaveChanges();
        }

        public override Patient Get(int id)
        {
            return GetAll().SingleOrDefault(x => x.PatientID.Equals(id));
        }

        public override void Update(Patient t)
        {
            var patientUpdate = Get(t.PatientID);
            patientUpdate.Name = t.Name;
            patientUpdate.Age = t.Age;
            patientUpdate.Address = t.Address;
            hpctx.SaveChanges();
        }
    }
}
