using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using OW21BB_HFT_2021221.Models;
using OW21BB_HFT_2021221.WPFClient.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace OW21BB_HFT_2021221.WPFClient.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Hospital> Hospitals { get; set; }
        public RestCollection<Doctor> Doctors { get; set; }
        public RestCollection<Patient> Patients { get; set; }

        private SolidColorBrush backGroundColor;

        public SolidColorBrush BackGroundColor
        {
            get { return new SolidColorBrush(Color.FromRgb(153, 217, 234)); }
        }


        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        #region Commands



        public ICommand ManageHospitalsCommand { get; set; }
        public ICommand ManageDoctorsCommand { get; set; }
        public ICommand ManagePatientsCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        public ICommand BackCommand { get; set; }


        public ICommand CreateHospitalCommand { get; set; }
        public ICommand DeleteHospitalCommand { get; set; }
        public ICommand UpdateHospitalCommand { get; set; }

        public ICommand CreateDoctorCommand { get; set; }
        public ICommand DeleteDoctorCommand { get; set; }
        public ICommand UpdateDoctorCommand { get; set; }

        public ICommand CreatePatientCommand { get; set; }
        public ICommand DeletePatientCommand { get; set; }
        public ICommand UpdatePatientCommand { get; set; }



        #endregion


        #region Properties



        private bool showMenu;

        public bool ShowMenu
        {
            get { return showMenu; }
            set
            {
                SetProperty(ref showMenu, value);
            }
        }

        private bool showHospitals;

        public bool ShowHospitals
        {
            get { return showHospitals; }
            set
            {
                SetProperty(ref showHospitals, value);
                (ManageHospitalsCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private bool showDoctors;

        public bool ShowDoctors
        {
            get { return showDoctors; }
            set
            {
                SetProperty(ref showDoctors, value);
                (ManageDoctorsCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private bool showPatients;

        public bool ShowPatients
        {
            get { return showPatients; }
            set
            {
                SetProperty(ref showPatients, value);
                (ManagePatientsCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }



        private Hospital selectedHospital;

        public Hospital SelectedHospital
        {
            get { return selectedHospital; }
            set
            {
                if (value != null)
                {
                    selectedHospital = selectedHospital.GetCopy(value);
                    OnPropertyChanged();

                    (UpdateHospitalCommand as RelayCommand).NotifyCanExecuteChanged();
                    (DeleteHospitalCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        private Doctor selectedDoctor;

        public Doctor SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                if (value != null)
                {
                    selectedDoctor = selectedDoctor.GetCopy(value);
                    OnPropertyChanged();

                    (DeleteDoctorCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }



        private Patient selectedPatient;

        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {

                if (value != null)
                {
                    selectedPatient = selectedPatient.GetCopy(value);

                    OnPropertyChanged();
                    (DeletePatientCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        #endregion





        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Hospitals = new RestCollection<Hospital>("http://localhost:41147/", "hospital", "hub");
                Doctors = new RestCollection<Doctor>("http://localhost:41147/", "doctor", "hub");
                Patients = new RestCollection<Patient>("http://localhost:41147/", "patient", "hub");

                this.showMenu = true;
                this.showHospitals = false;
                this.showDoctors = false;
                this.showPatients = false;

                SetupMenuCommands();

                SetupHospitalCommands();
                SetupDoctorCommands();
                SetupPatientCommands();

            }
        }

        private void SetupHospitalCommands()
        {
            CreateHospitalCommand = new RelayCommand(() =>
            {
                Hospitals.Add(new Hospital()
                {
                    Name = SelectedHospital.Name,
                    Location = SelectedHospital.Location,
                });
            });

            DeleteHospitalCommand = new RelayCommand(() =>
            {
                Hospitals.Delete(SelectedHospital.HospitalID);
            });

            UpdateHospitalCommand = new RelayCommand(() =>
            {                
                Hospitals.Update(SelectedHospital);
            });

            selectedHospital = new Hospital();


        }

        private void SetupDoctorCommands()
        {
            CreateDoctorCommand = new RelayCommand(() =>
            {
                var assignedHospital = AssignHospital();
                Doctors.Add(new Doctor()
                {
                    Name = SelectedDoctor.Name,
                    Specialization = SelectedDoctor.Specialization,
                    HospitalID = assignedHospital?.HospitalID,
                    Hospital = assignedHospital

                });
            });

            DeleteDoctorCommand = new RelayCommand(() =>
            {
                Doctors.Delete(SelectedDoctor.DoctorID);
            });

            UpdateDoctorCommand = new RelayCommand(() =>
            {
                Doctors.Update(SelectedDoctor);
            });

            selectedDoctor = new Doctor();
        }

        private void SetupPatientCommands()
        {
            CreatePatientCommand = new RelayCommand(() =>
            {
                var assignedDoctor = AssignDoctor();
                Patients.Add(new Patient()
                {
                    Name = SelectedPatient.Name,
                    Address = SelectedPatient.Address,
                    Age = SelectedPatient.Age,
                    Disease = SelectedPatient.Disease,
                    DoctorID = assignedDoctor?.DoctorID,
                    Doctor = assignedDoctor
                });
            });

            DeletePatientCommand = new RelayCommand(() =>
            {
                Patients.Delete(SelectedPatient.PatientID);
            });

            UpdatePatientCommand = new RelayCommand(() =>
            {
                Patients.Update(SelectedPatient);
            });

            selectedPatient = new Patient();
        }

        private void SetupMenuCommands()
        {
            ManageHospitalsCommand = new RelayCommand(() =>
            {
                this.ShowHospitals = true;
                this.ShowDoctors = false;
                this.ShowPatients = false;
                this.ShowMenu = false;

            });

            ManageDoctorsCommand = new RelayCommand(() =>
            {
                this.ShowHospitals = false;
                this.ShowDoctors = true;
                this.ShowPatients = false;
                this.ShowMenu = false;
            });

            ManagePatientsCommand = new RelayCommand(() =>
            {
                this.ShowHospitals = false;
                this.ShowDoctors = false;
                this.ShowPatients = true;
                this.ShowMenu = false;
            });

            ExitCommand = new RelayCommand(() => Environment.Exit(0));

            BackCommand = new RelayCommand(() =>
            {
                this.ShowHospitals = false;
                this.ShowDoctors = false;
                this.ShowPatients = false;
                this.ShowMenu = true;
            });
        }



        private Hospital? AssignHospital()
        {
            return Hospitals.ElementAtOrDefault(Util.rnd.Next(0, Hospitals.Count()));
        }

        private Doctor? AssignDoctor()
        {
            return Doctors.ElementAt(Util.rnd.Next(0, Doctors.Count()));
        }
    }
}
