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
    class ClinicPatientsViewModel:ViewModelBase
    {
        ClinicPatients view;

        IPatientService patientService;

        public ClinicPatientsViewModel(ClinicPatients clinicPatientsView,
            tblClinicAdmin adminLogedIn)
        {
            view = clinicPatientsView;


            patientService = new PatientService();

            ClinicPatient = new tblClinicPatient();
            //clinicMaintenaceListDB = maintenaceService.GetvwMaintenaces();
            //ClinicMaintenaceList = ConvertForPresentation(clinicMaintenaceListDB);
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


        private tblClinicPatient clinicPatient;
        public tblClinicPatient ClinicPatient
        {
            get
            {
                return clinicPatient;
            }
            set
            {
                clinicPatient = value;
                OnPropertyChanged("ClinicPatient");
            }
        }

        

        private List<tblClinicPatient> clinicPatientList;
        public List<tblClinicPatient> ClinicPatientList
        {
            get
            {
                return clinicPatientList;
            }
            set
            {
                clinicPatientList = value;
                OnPropertyChanged("ClinicPatientList");
            }
        }
        private ICommand addClinicPatient;
        public ICommand AddClinicPatient
        {
            get
            {
                if (addClinicPatient == null)
                {
                    addClinicPatient = new RelayCommand(param => AddClinicPatientExecute(),
                        param => CanAddClinicPatientExecute());
                }
                return addClinicPatient;
            }
        }

        private void AddClinicPatientExecute()
        {
            try
            {

                //AddClinicMaintenance addClinicMaintenance = new AddClinicMaintenance();
                //addClinicMaintenance.ShowDialog();

                //clinicMaintenaceListDB = maintenaceService.GetvwMaintenaces();
                //ClinicMaintenaceList = ConvertForPresentation(clinicMaintenaceListDB);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddClinicPatientExecute()
        {

            return true;
        }

        private ICommand deletMaintenance;
        public ICommand DeleteMaintenance
        {
            get
            {
                if (deletMaintenance == null)
                {
                    deletMaintenance = new RelayCommand(param => DeleteMaintenanceExecute(),
                        param => CanDeleteMaintenanceExecute());
                }
                return deletMaintenance;
            }
        }

        private void DeleteMaintenanceExecute()
        {
            try
            {
                if (ClinicPatient != null)
                {

                    MessageBoxResult result = MessageBox.Show("Are you sure that you want to " +
                        "delete this ClinicMaintenace?" +
                        "", "My App",
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    int patinetId = ClinicPatient.PatientID;


                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            //maintenaceService.DeleteMaintenace(maintenaceId);
                            //clinicMaintenaceListDB = maintenaceService.GetvwMaintenaces();
                            //ClinicMaintenaceList = ConvertForPresentation(clinicMaintenaceListDB);

                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanDeleteMaintenanceExecute()
        {
            if (ClinicPatient == null)
            {
                return false;
            }
            else
            {
                return true;
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

        private ICommand back;
        public ICommand Back
        {
            get
            {
                if (back == null)
                {
                    back = new RelayCommand(param => BackExecute(), param => CanBackExecute());
                }
                return back;
            }
        }

        private void BackExecute()
        {
            try
            {
                ClinicAdminMain adminView =
                    new ClinicAdminMain(Admin);
                adminView.Show();
                view.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanBackExecute()
        {
            return true;
        }
    }
}
