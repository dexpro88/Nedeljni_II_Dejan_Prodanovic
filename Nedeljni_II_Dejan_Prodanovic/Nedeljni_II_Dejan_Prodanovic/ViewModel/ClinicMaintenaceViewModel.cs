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
    class ClinicMaintenaceViewModel:ViewModelBase
    {
        ClinicMaintenaces view;

        IClinicMaintenace maintenaceService;

        public ClinicMaintenaceViewModel(ClinicMaintenaces clinicMaintenacesView,
            tblClinicAdmin adminLogedIn)
        {
            view = clinicMaintenacesView;


            maintenaceService = new ClinicMaintenaceService();

            ClinicMaintenace = new vwClinicMaintenace();
            ClinicMaintenaceList = maintenaceService.GetvwMaintenaces();
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


        private vwClinicMaintenace clinicMaintenace;
        public vwClinicMaintenace ClinicMaintenace
        {
            get
            {
                return clinicMaintenace;
            }
            set
            {
                clinicMaintenace = value;
                OnPropertyChanged("ClinicMaintenace");
            }
        }

        private List<vwClinicMaintenace> clinicMaintenaceList;
        public List<vwClinicMaintenace> ClinicMaintenaceList
        {
            get
            {
                return clinicMaintenaceList;
            }
            set
            {
                clinicMaintenaceList = value;
                OnPropertyChanged("ClinicMaintenaceList");
            }
        }
        private ICommand addClinicMaintenance;
        public ICommand AddClinicMaintenance
        {
            get
            {
                if (addClinicMaintenance == null)
                {
                    addClinicMaintenance = new RelayCommand(param => AddClinicMaintenanceExecute(),
                        param => CanAddClinicMaintenanceExecute());
                }
                return addClinicMaintenance;
            }
        }

        private void AddClinicMaintenanceExecute()
        {
            try
            {

                AddClinicMaintenance addClinicMaintenance = new AddClinicMaintenance();
                addClinicMaintenance.ShowDialog();

                ClinicMaintenaceList = maintenaceService.GetvwMaintenaces();
                //PredifinedAccount newView = new PredifinedAccount();
                //newView.Show();
                //view.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddClinicMaintenanceExecute()
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
                if (ClinicMaintenace != null)
                {

                    MessageBoxResult result = MessageBox.Show("Are you sure that you want to " +
                        "delete this ClinicMaintenace?" +
                        "", "My App",
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    int maintenaceId = ClinicMaintenace.ClinicMaintenaceID;
                   

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            maintenaceService.DeleteMaintenace(maintenaceId);
                            ClinicMaintenaceList = maintenaceService.GetvwMaintenaces();

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
            if (ClinicMaintenace == null)
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
