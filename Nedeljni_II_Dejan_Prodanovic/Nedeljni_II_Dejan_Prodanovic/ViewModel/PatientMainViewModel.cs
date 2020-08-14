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
    class PatientMainViewModel:ViewModelBase
    {
        PatientMainView view;
        IUserService userService;
        IClinicAminService adminService;
        IClinicMaintenaceService maintenaceService;
        IClinicManagerService managerService;
        IDoctorService doctorService;

        public PatientMainViewModel(PatientMainView patientMainView,
            vwClinicPatient patientLogedIn)
        {
            view = patientMainView;


            adminService = new ClinicAminService();
            userService = new UserService();
            maintenaceService = new ClinicMaintenaceService();
            managerService = new ClinicManagerService();
            doctorService = new DoctorService();

            Patient = patientLogedIn;

        }

        private vwClinicPatient patient;
        public vwClinicPatient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
                OnPropertyChanged("Patient");
            }
        }

        private ICommand logout;
        public ICommand Logout
        {
            get
            {
                if (logout == null)
                {
                    logout = new RelayCommand(param => LogoutExecute(), param => CanLogoutExecute());
                }
                return logout;
            }
        }

        private void LogoutExecute()
        {
            try
            {
                LoginView loginView = new LoginView();
                loginView.Show();
                view.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanLogoutExecute()
        {
            return true;
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


        private ICommand createRequest;
        public ICommand CreateRequest
        {
            get
            {
                if (createRequest == null)
                {
                    createRequest = new RelayCommand(param => CreateRequestExecute(),
                        param => CanCreateRequestExecute());
                }
                return createRequest;
            }
        }

        private void CreateRequestExecute()
        {
            try
            {
                ExaminationReport examinationReport = new ExaminationReport(Patient);
                examinationReport.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanCreateRequestExecute()
        {

            return true;
        }
    }
}
