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

namespace OW21BB_HFT_2021221.WPFClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Hospital> Hospitals { get; set; }
        public RestCollection<Patient> Patients { get; set; }

        private SolidColorBrush backGroundColor;

        public SolidColorBrush BackGroundColor
        {
            get { return new SolidColorBrush(Color.FromRgb(153, 217, 234)); }
        }


        //private Patient selectedPatient;

        //public Patient SelectedPatient
        //{
        //    get { return selectedPatient; }
        //    set
        //    {

        //        if (value != null)
        //        {
        //            selectedPatient = selectedPatient.GetCopy(value);

        //            OnPropertyChanged();
        //            (DeletePatientCommand as RelayCommand).NotifyCanExecuteChanged();
        //        }
        //    }
        //}


        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public ICommand ManageHospitalsCommand { get; set; }
        public ICommand ManageDoctorsCommand { get; set; }
        public ICommand ManagePatientsCommand { get; set; }
        public ICommand ExitCommand { get; set; }


        private bool showMenu;

        public bool ShowMenu
        {
            get { return showMenu; }
            set
            {
                showMenu = value;
                OnPropertyChanged("ShowMenu");
                (ManageHospitalsCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private bool showHospitals;

        public bool ShowHospitals
        {
            get { return showHospitals; }
            set
            {
                showHospitals = value;
                OnPropertyChanged("ShowHospitals");
                (ManageHospitalsCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }



        //public ICommand CreatePatientCommand { get; set; }
        //public ICommand DeletePatientCommand { get; set; }
        //public ICommand UpdatePatientCommand { get; set; }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Hospitals = new RestCollection<Hospital>("http://localhost:41147/", "hospital", "hub");
                Patients = new RestCollection<Patient>("http://localhost:41147/", "patient", "hub");

                this.showMenu = true;
                this.showHospitals = false;

                ManageHospitalsCommand = new RelayCommand(() =>
                {
                    this.ShowHospitals = true;
                    this.ShowMenu = false;

                });

                ManageDoctorsCommand = new RelayCommand(() =>
                {

                });

                ManagePatientsCommand = new RelayCommand(() =>
                {

                });

                ExitCommand = new RelayCommand(() => Environment.Exit(0));

                #region patientCommands
                //CreatePatientCommand = new RelayCommand(() =>
                //{
                //    Patients.Add(new Patient()
                //    {
                //        Name = SelectedPatient.Name
                //    });
                //});

                //DeletePatientCommand = new RelayCommand(() =>
                //{
                //    Patients.Delete(SelectedPatient.PatientID);
                //},
                //() =>
                //{
                //    return SelectedPatient.Name != null;
                //}
                //);

                //UpdatePatientCommand = new RelayCommand(() =>
                //{
                //    Patients.Update(SelectedPatient);
                //},
                //() =>
                //{
                //    return SelectedPatient != null;
                //});

                //selectedPatient = new Patient(); 
                #endregion
            }
        }
    }
}
