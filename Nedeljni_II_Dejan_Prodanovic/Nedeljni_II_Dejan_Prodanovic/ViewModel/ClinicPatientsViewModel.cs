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

            ClinicPatient = new vwClinicPatient();
            ClinicPatientList = patientService.GetvwPatients();
            
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


        private vwClinicPatient clinicPatient;
        public vwClinicPatient ClinicPatient
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

        

        private List<vwClinicPatient> clinicPatientList;
        public List<vwClinicPatient> ClinicPatientList
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

                AddClinicPatient addClinicPatient = new AddClinicPatient();
                addClinicPatient.ShowDialog();

                ClinicPatientList = patientService.GetvwPatients();


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

        private ICommand editPatient;
        public ICommand EditPatient
        {
            get
            {
                if (editPatient == null)
                {
                    editPatient = new RelayCommand(param => EditPatientExecute(),
                        param => CanEditPatientExecute());
                }
                return editPatient;
            }
        }

        private void EditPatientExecute()
        {
            try
            {

                EditPatient editPatient = new EditPatient(ClinicPatient);
                editPatient.ShowDialog();

                ClinicPatientList = patientService.GetvwPatients();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanEditPatientExecute()
        {

            return true;
        }

        private ICommand deletePatient;
        public ICommand DeletePatient
        {
            get
            {
                if (deletePatient == null)
                {
                    deletePatient = new RelayCommand(param => DeletePatientExecute(),
                        param => CanDeletePatientExecute());
                }
                return deletePatient;
            }
        }

        private void DeletePatientExecute()
        {
            try
            {
                if (ClinicPatient != null)
                {

                    MessageBoxResult result = MessageBox.Show("Are you sure that you want to " +
                        "delete this ClinicPatient?" +
                        "", "My App",
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    int patinetId = ClinicPatient.PatientID;


                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            patientService.DeletePatient(patinetId);
                            ClinicPatientList = patientService.GetvwPatients();

                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanDeletePatientExecute()
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
