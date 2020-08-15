using Nedeljni_II_Dejan_Prodanovic.Command;
using Nedeljni_II_Dejan_Prodanovic.Model;
using Nedeljni_II_Dejan_Prodanovic.Service;
using Nedeljni_II_Dejan_Prodanovic.Utility;
using Nedeljni_II_Dejan_Prodanovic.Validation;
using Nedeljni_II_Dejan_Prodanovic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nedeljni_II_Dejan_Prodanovic.ViewModel
{
    class EditDoctorViewModel:ViewModelBase
    {
        EditDoctor view;
        IUserService userService;
        IClinicAminService adminService;
        IClinicMaintenaceService maintenaceService;
        IClinicManagerService managerService;
        IDoctorService doctorService;
        string oldUsername;
        string oldIdCardNumber;
        string oldUniqueNumber;
        string oldAccountNumber;
        public EditDoctorViewModel(EditDoctor addClinicDoctor, vwClinicDoctor doctorToEdit)
        {
            view = addClinicDoctor;

            oldUniqueNumber = doctorToEdit.UniqueNumber;
            oldUsername = doctorToEdit.Username;
            oldIdCardNumber = doctorToEdit.IDCardNumber;
            oldAccountNumber = doctorToEdit.AccountNumber;

            adminService = new ClinicAminService();
            userService = new UserService();
            maintenaceService = new ClinicMaintenaceService();
            managerService = new ClinicManagerService();
            doctorService = new DoctorService();

            Doctor = doctorToEdit;

            ManagerList = managerService.GetvwManagers();
            ManagerListToPresent = new List<string>();
            CreateManagerDictionary();

            if (ManagerList.Count == 0)
            {
                NoManagers = Visibility.Visible;
            }
            else
            {
                NoManagers = Visibility.Hidden;
            }

            ShiftList = new List<string>() { "First", "Second", "Third" };

        }

        List<vwClinicManager> ManagerList;
        Dictionary<string, int> managerDict = new Dictionary<string, int>();
        //public List<string> ManagerListToPresent = new List<string>();

        public bool adminCreated = false;

        private List<string> managerListToPresent;
        public List<string> ManagerListToPresent
        {
            get { return managerListToPresent; }
            set
            {
                managerListToPresent = value;
                OnPropertyChanged("ManagerListToPresent");
            }
        }

        private string manager;
        public string Manager
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
                OnPropertyChanged("Manager");
            }
        }


        private Visibility noManagers;
        public Visibility NoManagers
        {
            get
            {
                return noManagers;
            }
            set
            {
                noManagers = value;
                OnPropertyChanged("NoManagers");
            }
        }

        private vwClinicDoctor doctor;
        public vwClinicDoctor Doctor
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

        //private tblClinicDoctor doctor;
        //public tblClinicDoctor Doctor
        //{
        //    get
        //    {
        //        return doctor;
        //    }
        //    set
        //    {
        //        doctor = value;
        //        OnPropertyChanged("Doctor");
        //    }
        //}

        private List<string> shiftList;
        public List<string> ShiftList
        {
            get
            {
                return shiftList;
            }
            set
            {
                shiftList = value;
                OnPropertyChanged("ShiftList");
            }
        }

        private string shift;
        public string Shift
        {
            get
            {
                return shift;
            }
            set
            {
                shift = value;
                OnPropertyChanged("Shift");
            }
        }
        private string gender = "male";
        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        private string recievePatients = "yes";
        public string RecievePatients
        {
            get { return recievePatients; }
            set
            {
                recievePatients = value;
                OnPropertyChanged("RecievePatients");
            }
        }

        private DateTime dateOfBirth = DateTime.Now;
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; OnPropertyChanged("DateOfBirth"); }
        }

        private tblClinicMaintenace maintenace;
        public tblClinicMaintenace Maintenace
        {
            get
            {
                return maintenace;
            }
            set
            {
                maintenace = value;
                OnPropertyChanged("Maintenace");
            }
        }

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(SaveExecute, CanSaveExecute);
                }
                return save;
            }
        }

        private void SaveExecute(object parameter)
        {
            try
            {


                if (!ValidationClass.IsIDCardNumberValid(Doctor.IDCardNumber))
                {
                    MessageBox.Show("IDCardNumber is not valid");
                    return;
                }

                if (!ValidationClass.IsUniqueNumberValdi(Doctor.UniqueNumber))
                {
                    MessageBox.Show("UniqueNumber is not valid");
                    return;
                }


                if (!ValidationClass.IsAccountNumberValid(Doctor.AccountNumber))
                {
                    MessageBox.Show("AccountNumber is not valid");
                    return;
                }

                tblUser userInDb = userService.GetUserByUserName(Doctor.Username);

                if (userInDb != null && !userInDb.Username.Equals(oldUsername))
                {
                    string str1 = string.Format("User with this username exists\n" +
                        "Enter another username");
                    MessageBox.Show(str1);
                    return;
                }

                userInDb = userService.GetUserByIdCardNumber(Doctor.IDCardNumber);

                if (userInDb != null && !userInDb.IDCardNumber.Equals(oldIdCardNumber))
                {
                    string str1 = string.Format("User with this IDCardNumber exists\n" +
                        "Enter another IDCardNumber");
                    MessageBox.Show(str1);
                    return;
                }

                tblClinicDoctor doctorInDb =
                    doctorService.GetDoctorByAccountNumber(Doctor.AccountNumber);

                if (doctorInDb != null && !doctorInDb.AccountNumber.Equals(oldAccountNumber))
                {
                    string str1 = string.Format("Doctor with this AccountNumber exists\n" +
                        "Enter another AccountNumber");
                    MessageBox.Show(str1);
                    return;
                }

                doctorInDb = doctorService.GetDoctorByUniqueNumber(Doctor.UniqueNumber);

                if (doctorInDb != null && !doctorInDb.UniqueNumber.Equals(oldUniqueNumber))
                {
                    string str1 = string.Format("Doctor with this UniqueNumber exists\n" +
                        "Enter another UniqueNumber");
                    MessageBox.Show(str1);
                    return;
                }


                var passwordBox = parameter as PasswordBox;
                var password = passwordBox.Password;

                string encryptedString = EncryptionHelper.Encrypt(password);
                Doctor.Gender = Gender;
                Doctor.DateOfBirth = DateOfBirth;


                
                

                vwClinicManager manPom = managerService.GetManagerByManagerId(managerDict[Manager]);
                if (managerService.GetDoctorsOfManager(managerDict[Manager]).Count ==
                    manPom.MaxNumberOfDoctors)
                {
                    string str1 = string.Format("You can't choose this manager\n" +
                        "He can't have more doctors under his lead");
                    MessageBox.Show("");
                    return;
                }



                Doctor.DoctorShift = Shift;
                Doctor.ManagerID = managerDict[Manager];

                if (RecievePatients.Equals("yes"))
                {
                    Doctor.RecievesPatients = true;
                }
                else
                {
                    Doctor.RecievesPatients = false;
                }



                 doctorService.EditDoctor(Doctor);

               
                MessageBox.Show("You edited Doctor");

                adminCreated = true;
                view.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute(object parameter)
        {

            if (String.IsNullOrEmpty(Doctor.FullName) || String.IsNullOrEmpty(Doctor.IDCardNumber)
                //|| String.IsNullOrEmpty(User.Nationality)
                || String.IsNullOrEmpty(Doctor.Username) || parameter as PasswordBox == null
                || String.IsNullOrEmpty((parameter as PasswordBox).Password)
                || String.IsNullOrEmpty(Doctor.Sector) || String.IsNullOrEmpty(Doctor.AccountNumber)
                || String.IsNullOrEmpty(Doctor.UniqueNumber) || String.IsNullOrEmpty(Manager)
                || String.IsNullOrEmpty(Shift))
            {
                return false;
            }
            else
            {
                return true;
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


        private ICommand chooseGender;
        public ICommand ChooseGender
        {
            get
            {
                if (chooseGender == null)
                {
                    chooseGender = new RelayCommand(ChooseGenderExecute, CanChooseGenderExecute);
                }
                return chooseGender;
            }
        }

        private void ChooseGenderExecute(object parameter)
        {
            Gender = (string)parameter;
        }

        private bool CanChooseGenderExecute(object parameter)
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
        private ICommand chooseRecievePatients;
        public ICommand ChooseRecievePatients
        {
            get
            {
                if (chooseRecievePatients == null)
                {
                    chooseRecievePatients = new RelayCommand(ChooseRecievePatientsExecute,
                        CanChooseRecievePatientsExecute);
                }
                return chooseRecievePatients;
            }
        }

        private void ChooseRecievePatientsExecute(object parameter)
        {
            RecievePatients = (string)parameter;

        }

        private bool CanChooseRecievePatientsExecute(object parameter)
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

        private void CreateManagerDictionary()
        {
            foreach (var item in ManagerList)
            {
                string str = item.FullName + ", " + item.IDCardNumber;
                managerDict.Add(str, item.ManagerID);
                ManagerListToPresent.Add(str);
            }
        }
    }
}
