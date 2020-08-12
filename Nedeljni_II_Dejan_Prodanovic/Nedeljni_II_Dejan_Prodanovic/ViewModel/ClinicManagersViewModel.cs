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
    class ClinicManagersViewModel:ViewModelBase
    {
        ClinicManagers view;

        IClinicManagerService managerService;

        public ClinicManagersViewModel(ClinicManagers clinicMaintenacesView,
            tblClinicAdmin adminLogedIn)
        {
            view = clinicMaintenacesView;


            managerService = new ClinicManagerService();

            ClinicManager = new vwClinicManager();
            ClinicManagerList = managerService.GetvwManagers();
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


        private vwClinicManager clinicManager;
        public vwClinicManager ClinicManager
        {
            get
            {
                return clinicManager;
            }
            set
            {
                clinicManager = value;
                OnPropertyChanged("ClinicManager");
            }
        }

        private List<vwClinicManager> clinicManagerList;
        public List<vwClinicManager> ClinicManagerList
        {
            get
            {
                return clinicManagerList;
            }
            set
            {
                clinicManagerList = value;
                OnPropertyChanged("ClinicManagerList");
            }
        }
        private ICommand addClinicManager;
        public ICommand AddClinicManager
        {
            get
            {
                if (addClinicManager == null)
                {
                    addClinicManager = new RelayCommand(param => AddClinicManagerExecute(),
                        param => CanAddClinicManagerExecute());
                }
                return addClinicManager;
            }
        }

        private void AddClinicManagerExecute()
        {
            try
            {

                AddClinicManager addClinicManager = new AddClinicManager();
                addClinicManager.ShowDialog();

                ClinicManagerList = managerService.GetvwManagers();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddClinicManagerExecute()
        {

            return true;
        }

        private ICommand deletManager;
        public ICommand DeletManager
        {
            get
            {
                if (deletManager == null)
                {
                    deletManager = new RelayCommand(param => DeletManagerExecute(),
                        param => CanDeletManagerExecute());
                }
                return deletManager;
            }
        }

        private void DeletManagerExecute()
        {
            try
            {
                if (ClinicManager != null)
                {

                    MessageBoxResult result = MessageBox.Show("Are you sure that you want to " +
                        "delete this ClinicMaintenace?" +
                        "", "My App",
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    int maintenaceId = ClinicManager.ManagerID;


                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            //maintenaceService.DeleteMaintenace(maintenaceId);
                            ClinicManagerList = managerService.GetvwManagers();

                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanDeletManagerExecute()
        {
            if (ClinicManager == null)
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
