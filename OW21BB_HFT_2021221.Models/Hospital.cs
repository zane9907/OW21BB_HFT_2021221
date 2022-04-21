using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.Models
{
    [Table("Hospital")]
    public class Hospital
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HospitalID { get; set; }

        [Required]
        public string Name { get; set; }

        //[ForeignKey(nameof(Doctor))]        
        //public int DoctorID { get; set; }

        public string Location { get; set; }

           
        public int DoctorCount
        {
            get
            {
                return this.Doctors.Count();
            }
        }

        [NotMapped]
        public string AllData => $"{Name} - {Location}";

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Doctor> Doctors { get; set; }

        public Hospital()
        {
            this.Doctors = new HashSet<Doctor>();
        }

        public Hospital GetCopy(Hospital value)
        {
            return new Hospital()
            {
                HospitalID = value.HospitalID,
                Name = value.Name,
                Location = value.Location,
                Doctors = value.Doctors                
            };
        }
    }
}
