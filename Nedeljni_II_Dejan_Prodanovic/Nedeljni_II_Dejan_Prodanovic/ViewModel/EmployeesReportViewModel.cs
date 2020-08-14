using Nedeljni_II_Dejan_Prodanovic.Command;
using Nedeljni_II_Dejan_Prodanovic.Model;
using Nedeljni_II_Dejan_Prodanovic.Service;
using Nedeljni_II_Dejan_Prodanovic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_II_Dejan_Prodanovic.ViewModel
{
    class EmployeesReportViewModel:ViewModelBase
    {
        EmployeesReport view;

        IUserService userService;
        IClinicService clinicService;
        IClinicAminService adminService;
        IClinicManagerService managerService;
        IPatientService patientService;
        IDoctorService doctorService;
        IClinicMaintenaceService maintenaceService;

        public EmployeesReportViewModel(EmployeesReport employeesReport)
        {
            view = employeesReport;


            clinicService = new ClinicService();
            userService = new UserService();
            adminService = new ClinicAminService();
            managerService = new ClinicManagerService();
            patientService = new PatientService();
            doctorService = new DoctorService();
            maintenaceService = new ClinicMaintenaceService();



            List<tblClinicPatient> patients = patientService.GetPatients();
            NumberOfPatients = patients.Count().ToString();

            List<tblClinicDoctor> doctors = doctorService.GetDoctors();
            NumberOfDoctors = doctors.Count().ToString();

            List<tblClinicManager> managers = managerService.GetManagers();
            NumberOfManagers = managers.Count().ToString();

            List<tblClinicMaintenace> maintenaces = maintenaceService.GetMaintenaces();
            NumberOfMaintenances = maintenaces.Count().ToString();


        }

        private tblClinicAdmin admin;
        public tblClinicAdmin Admin
        {
            get
            {
                return admin;
            }
            set
            {
                admin = value;
                OnPropertyChanged("Admin");
            }
        }

        private string numberOfPatients;
        public string NumberOfPatients
        {
            get
            {
                return numberOfPatients;
            }
            set
            {
                numberOfPatients = value;
                OnPropertyChanged("NumberOfPatients");
            }
        }


        private string numberOfDoctors;
        public string NumberOfDoctors
        {
            get
            {
                return numberOfDoctors;
            }
            set
            {
                numberOfDoctors = value;
                OnPropertyChanged("NumberOfDoctors");
            }
        }

        private string numberOfRiskPatients;
        public string NumberOfRiskPatients
        {
            get
            {
                return numberOfRiskPatients;
            }
            set
            {
                numberOfRiskPatients = value;
                OnPropertyChanged("NumberOfRiskPatients");
            }
        }

        private string numberOfManagers;
        public string NumberOfManagers
        {
            get
            {
                return numberOfManagers;
            }
            set
            {
                numberOfManagers = value;
                OnPropertyChanged("NumberOfManagers");
            }
        }

        private string numberOfMaintenances;
        public string NumberOfMaintenances
        {
            get
            {
                return numberOfMaintenances;
            }
            set
            {
                numberOfMaintenances = value;
                OnPropertyChanged("NumberOfMaintenances");
            }
        }
        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }

        private void CloseExecute()
        {
            try
            {
                view.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanCloseExecute()
        {
            return true;
        }

        private bool CanChooseHasTerraceExecute(object parameter)
        {
            if (parameter != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
