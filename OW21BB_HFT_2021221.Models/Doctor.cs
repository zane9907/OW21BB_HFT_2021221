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
    [Table("Doctor")]
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorID { get; set; }

        [ForeignKey(nameof(Hospital))]
        public int? HospitalID { get; set; }

        //[ForeignKey(nameof(Patient))]        
        //public int PatientID { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string Specialization { get; set; }

        public int PatientCount
        {
            get
            {
                return this.Patients.Count();
            }
        }

        [NotMapped]
        public string AllData => $"[{DoctorID}] -> {Name} - {Specialization}";

        [NotMapped]
        [JsonIgnore]
        public virtual Hospital Hospital { get; set; }

        [NotMapped]
        [JsonIgnore]

        public virtual ICollection<Patient> Patients { get; set; }

        public Doctor()
        {
            this.Patients = new HashSet<Patient>();
        }

    }
}
