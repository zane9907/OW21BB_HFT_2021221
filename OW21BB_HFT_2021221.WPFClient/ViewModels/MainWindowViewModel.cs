using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using OW21BB_HFT_2021221.Models;
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
                Hospitals.Add(new Hospital().GetCopy(SelectedHospital));
            });

            DeleteHospitalCommand = new RelayCommand(() =>
            {
                Hospitals.Delete(SelectedHospital.HospitalID);
            },
            () =>
            {
                return SelectedHospital.Name != null;
            }
            );

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
                Doctors.Add(new Doctor()
                {
                    Name = SelectedDoctor.Name
                });
            });

            DeleteDoctorCommand = new RelayCommand(() =>
            {
                Doctors.Delete(SelectedDoctor.DoctorID);
            },
            () =>
            {
                return SelectedDoctor.Name != null;
            }
            );

            UpdateDoctorCommand = new RelayCommand(() =>
            {
                Doctors.Update(SelectedDoctor);
            },
            () =>
            {
                return SelectedDoctor != null;
            });

            selectedDoctor = new Doctor();
        }

        private void SetupPatientCommands()
        {
            CreatePatientCommand = new RelayCommand(() =>
            {
                Patients.Add(new Patient()
                {
                    Name = SelectedPatient.Name
                });
            });

            DeletePatientCommand = new RelayCommand(() =>
            {
                Patients.Delete(SelectedPatient.PatientID);
            },
            () =>
            {
                return SelectedPatient.Name != null;
            }
            );

            UpdatePatientCommand = new RelayCommand(() =>
            {
                Patients.Update(SelectedPatient);
            },
            () =>
            {
                return SelectedPatient != null;
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
    }
}
