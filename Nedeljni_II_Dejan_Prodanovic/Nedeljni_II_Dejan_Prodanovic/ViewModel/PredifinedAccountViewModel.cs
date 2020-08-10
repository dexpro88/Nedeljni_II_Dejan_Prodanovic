using Nedeljni_II_Dejan_Prodanovic.Command;
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
    class PredifinedAccountViewModel:ViewModelBase
    {
        PredifinedAccount view;


        IClinicAminService adminService;

        public PredifinedAccountViewModel(PredifinedAccount predifinedAccOpen)
        {
            view = predifinedAccOpen;
            adminService = new ClinicAminService();

            if (adminService.GetAdmins().Count == 0)
            {
                NoClinicAdmin = Visibility.Visible;
            }
            else
            {
                NoClinicAdmin = Visibility.Hidden;
            }
        }

        private Visibility noClinicAdmin;
        public Visibility NoClinicAdmin
        {
            get
            {
                return noClinicAdmin;
            }
            set
            {
                noClinicAdmin = value;
                OnPropertyChanged("NoClinicAdmin");
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





        private ICommand addAdmin;
        public ICommand AddAdmin
        {
            get
            {
                if (addAdmin == null)
                {
                    addAdmin = new RelayCommand(param => AddAdminExecute(),
                        param => CanAddAdminExecute());
                }
                return addAdmin;
            }
        }

        private void AddAdminExecute()
        {
            try
            {
                AddClinicAdministrator addAdmin = new AddClinicAdministrator();
                addAdmin.ShowDialog();

                if ((addAdmin.DataContext as AddClinicAdministratorViewModel).adminCreated==true)
                {
                   
                    NoClinicAdmin = Visibility.Hidden;
                }
                PredifinedAccount newView = new PredifinedAccount();
                newView.Show();
                view.Close();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddAdminExecute()
        {

            return true;
        }
    }
}
