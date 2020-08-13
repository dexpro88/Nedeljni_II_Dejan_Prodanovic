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
    class ClinicDoctorsViewModel:ViewModelBase
    {
        ClinicDoctors view;

        IClinicManagerService managerService;
        IDoctorService doctorService;

        public ClinicDoctorsViewModel(ClinicDoctors clinicManagers,
            tblClinicAdmin adminLogedIn)
        {
            view = clinicManagers;


            managerService = new ClinicManagerService();
            doctorService = new DoctorService();

            ClinicDoctor = new vwClinicDoctor();
            SelectedDoctor = new Doctor();
            managerList = managerService.GetvwManagers();

            
            ClinicDoctorList = doctorService.GetvwDoctors();
            
            DoctorList = CreateDoctorList();
        }

        List<vwClinicManager> managerList;
    

        Dictionary<string, int> managerDict = new Dictionary<string,int>();

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


        private vwClinicDoctor clinicDoctor;
        public vwClinicDoctor ClinicDoctor
        {
            get
            {
                return clinicDoctor;
            }
            set
            {
                clinicDoctor = value;
                OnPropertyChanged("ClinicDoctor");
            }
        }

        private List<vwClinicDoctor> clinicDoctorList;
        public List<vwClinicDoctor> ClinicDoctorList
        {
            get
            {
                return clinicDoctorList;
            }
            set
            {
                clinicDoctorList = value;
                OnPropertyChanged("ClinicDoctorList");
            }
        }
        private Doctor selectedDoctor;
        public Doctor SelectedDoctor
        {
            get
            {
                return selectedDoctor;
            }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged("SelectedDoctor");
            }
        }

        private List<Doctor> doctorList;
        public List<Doctor> DoctorList
        {
            get
            {
                return doctorList;
            }
            set
            {
                doctorList = value;
                OnPropertyChanged("DoctorList");
            }
        }

        private Doctor doctor;
        public Doctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                doctor = value;
                OnPropertyChanged("Doctor");
            }
        }
        private ICommand addClinicDoctor;
        public ICommand AddClinicDoctor
        {
            get
            {
                if (addClinicDoctor == null)
                {
                    addClinicDoctor = new RelayCommand(param => AddClinicDoctorExecute(),
                        param => CanAddClinicDoctorExecute());
                }
                return addClinicDoctor;
            }
        }

        private void AddClinicDoctorExecute()
        {
            try
            {

                AddClinicDoctor addClinicDoctor = new AddClinicDoctor();
                addClinicDoctor.ShowDialog();

                ClinicDoctorList = doctorService.GetvwDoctors();
                DoctorList = CreateDoctorList();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddClinicDoctorExecute()
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
                if (SelectedDoctor != null)
                {

                    MessageBoxResult result = MessageBox.Show("Are you sure that you want to " +
                        "delete this ClinicDoctor?" +
                        "", "My App",
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    int doctorID = SelectedDoctor.DoctorID;

                    //MessageBox.Show(doctorID.ToString());

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            doctorService.DeleteDoctor(doctorID);
                            ClinicDoctorList = doctorService.GetvwDoctors();
                            DoctorList = CreateDoctorList();
                           

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
            if (SelectedDoctor == null)
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

        private List<Doctor> CreateDoctorList()
        {
            List<Doctor> newList = new List<Doctor>();
            foreach (var item in ClinicDoctorList)
            {

                Doctor doctor = new Doctor();
                doctor.UniqueNumber = item.UniqueNumber;
                doctor.AccountNumber = item.AccountNumber;
                doctor.DoctorShift = item.DoctorShift;
                doctor.RecievesPatients = item.RecievesPatients;
                doctor.Sector = item.Sector;
                doctor.ManagerID = item.ManagerID;
                doctor.UserID = item.UserID;
                doctor.DoctorID = item.DoctorID;
                doctor.IDCardNumber = item.IDCardNumber;
                doctor.FullName = item.FullName;
                doctor.Gender = item.Gender;
                doctor.DateOfBirth = item.DateOfBirth;
                doctor.Nationality = item.Nationality;
                doctor.Username = item.Username;

                vwClinicManager manager =
                    managerService.GetManagerByManagerId((int)doctor.ManagerID);

                doctor.ManagerFullName = manager.FullName;
                doctor.ManagerIDCardNumber = manager.IDCardNumber;

                newList.Add(doctor);
            }


            return newList;



    }
    }
}
