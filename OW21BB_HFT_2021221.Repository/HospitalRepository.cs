using OW21BB_HFT_2021221.Data;
using OW21BB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.Repository
{
    public class HospitalRepository : Repository<Hospital>
    {
        public HospitalRepository(HospitalDbContext hpctx) : base(hpctx)
        {
        }

        public override void Create(Hospital t)
        {
            hpctx.Hospitals.Add(t);
            hpctx.SaveChanges();
        }

        public override void Delete(Hospital t)
        {
            var hospitalDelete = Read(t.HospitalID);
            hpctx.Hospitals.Remove(hospitalDelete);
            hpctx.SaveChanges();
        }

        public override Hospital Read(int id)
        {
            return ReadAll().SingleOrDefault(x => x.HospitalID.Equals(id));
        }

        public override void Update(Hospital t)
        {
            var hospitalUpdate = Read(t.HospitalID);
            hospitalUpdate.Name = t.Name;
            hospitalUpdate.Location = t.Location;
            hpctx.SaveChanges();
        }
    }
}
