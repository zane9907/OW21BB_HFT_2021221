using OW21BB_HFT_2021221.Data;
using OW21BB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.Repository
{
    public class DoctorRepository : Repository<Doctor>, IRepository<Doctor>
    {
        public DoctorRepository(HospitalDbContext hpctx) : base(hpctx)
        {
        }

        public override void Create(Doctor t)
        {
            hpctx.Doctors.Add(t);                
            hpctx.SaveChanges();
        }

        public override void Delete(Doctor t)
        {
            var doctorDelete = Get(t.DoctorID);
            hpctx.Doctors.Remove(doctorDelete);
            hpctx.SaveChanges();
        }

        public override Doctor Get(int id)
        {
            return GetAll().SingleOrDefault(x => x.DoctorID.Equals(id));
        }

        public override void Update(Doctor t)
        {
            var doctorUpdate = Get(t.DoctorID);
            doctorUpdate.Name = t.Name;
            doctorUpdate.Specialization = t.Specialization;
            hpctx.SaveChanges();
        }
    }
}
