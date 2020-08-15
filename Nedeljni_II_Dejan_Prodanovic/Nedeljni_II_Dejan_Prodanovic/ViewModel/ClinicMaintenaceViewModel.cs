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

        IClinicMaintenaceService maintenaceService;

        public ClinicMaintenaceViewModel(ClinicMaintenaces clinicMaintenacesView,
            tblClinicAdmin adminLogedIn)
        {
            view = clinicMaintenacesView;


            maintenaceService = new ClinicMaintenaceService();

            ClinicMaintenace = new ClinicMaintenance();
            clinicMaintenaceListDB = maintenaceService.GetvwMaintenaces();
            ClinicMaintenaceList = ConvertForPresentation(clinicMaintenaceListDB);
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


        private ClinicMaintenance clinicMaintenace;
        public ClinicMaintenance ClinicMaintenace
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

        private List<vwClinicMaintenace> clinicMaintenaceListDB;

        private List<ClinicMaintenance> clinicMaintenaceList;
        public List<ClinicMaintenance> ClinicMaintenaceList
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

                clinicMaintenaceListDB = maintenaceService.GetvwMaintenaces();
                ClinicMaintenaceList = ConvertForPresentation(clinicMaintenaceListDB);
                 

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

        private ICommand editMaintenance;
        public ICommand EditMaintenance
        {
            get
            {
                if (editMaintenance == null)
                {
                    editMaintenance = new RelayCommand(param => EditMaintenanceExecute(),
                        param => CanEditMaintenanceExecute());
                }
                return editMaintenance;
            }
        }

        private void EditMaintenanceExecute()
        {
            try
            {

                EditMaintenance editClinicMaintenance = 
                    new EditMaintenance(ConvertTovwMaintenance(ClinicMaintenace));
                editClinicMaintenance.ShowDialog();

                clinicMaintenaceListDB = maintenaceService.GetvwMaintenaces();
                ClinicMaintenaceList = ConvertForPresentation(clinicMaintenaceListDB);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanEditMaintenanceExecute()
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
                            clinicMaintenaceListDB = maintenaceService.GetvwMaintenaces();
                            ClinicMaintenaceList = ConvertForPresentation(clinicMaintenaceListDB);

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

        private List<ClinicMaintenance>ConvertForPresentation(List<vwClinicMaintenace>list)
        {
            List<ClinicMaintenance> returnList = new List<ClinicMaintenance>();

            foreach (var item in list)
            {
                ClinicMaintenance newItem = new ClinicMaintenance();
                newItem.CanChooseInvalidAccess = item.CanChooseInvalidAccess;
                newItem.ClinicMaintenaceID = item.ClinicMaintenaceID;
                newItem.CanChooseClinicExpansionPermission = item.CanChooseClinicExpansionPermission;
                newItem.UserID = item.UserID;
                newItem.IDCardNumber = item.IDCardNumber;
                newItem.FullName = item.FullName;
                newItem.Gender = item.Gender;
                newItem.DateOfBirth = item.DateOfBirth;
                newItem.Nationality = item.Nationality;
                newItem.Username = item.Username;

                if (item.CanChooseInvalidAccess == false)
                {
                    newItem.CanChooseAmbulanceAccess = true;
                }

                returnList.Add(newItem);

            }
            return returnList;
       
    }

        private vwClinicMaintenace ConvertTovwMaintenance(ClinicMaintenance maintenance)
        {
            vwClinicMaintenace vwClinicMaintenace = new vwClinicMaintenace();
            vwClinicMaintenace.CanChooseInvalidAccess = maintenance.CanChooseInvalidAccess;
            vwClinicMaintenace.ClinicMaintenaceID = maintenance.ClinicMaintenaceID;
            vwClinicMaintenace.CanChooseClinicExpansionPermission = maintenance.CanChooseClinicExpansionPermission;
            vwClinicMaintenace.UserID = maintenance.UserID;
            vwClinicMaintenace.IDCardNumber = maintenance.IDCardNumber;
            vwClinicMaintenace.FullName = maintenance.FullName;
            vwClinicMaintenace.Gender = maintenance.Gender;
            vwClinicMaintenace.DateOfBirth = maintenance.DateOfBirth;
            vwClinicMaintenace.Nationality = maintenance.Nationality;
            vwClinicMaintenace.Username = maintenance.Username;

            return vwClinicMaintenace;
        }
    }
}
