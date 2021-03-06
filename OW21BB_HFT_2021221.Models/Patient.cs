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
    [Table("Patient")]
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientID { get; set; }

        //Maybe use Social Security Number *Undecided*
        //[MaxLength(9)]
        //[Required]
        //public int SocialSecurityNumber { get; set; }

        [ForeignKey(nameof(Doctor))]
        public int? DoctorID { get; set; }


        [Required]
        [MaxLength(30)]
        public string Name { get; set; }


        [Required]
        public int Age { get; set; }

        public string Address { get; set; }

        public string Disease { get; set; }


        [NotMapped]
        public string AllData => $"[{PatientID}] -> {Name} - {Age} - {Address}";

        [NotMapped]
        [JsonIgnore]
        public virtual Doctor Doctor { get; set; }

        public Patient GetCopy(Patient value)
        {
            return new Patient()
            {
                Name = value.Name,
                Age = value.Age,
                Address = value.Address,
                Disease = value.Disease,
                Doctor = value.Doctor,
                PatientID = value.PatientID,
                DoctorID = value.DoctorID   
            };
        }
    }
}
