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
    class EditPatientViewModel:ViewModelBase
    {
        EditPatient view;

        IUserService userService;
        IClinicAminService adminService;
        IClinicMaintenaceService maintenaceService;
        IClinicManagerService managerService;
        IPatientService patientService;
        IDoctorService doctorService;
        string oldUsername;
        string oldIdCardNumber;
        string oldHealthCardNumber;

        public EditPatientViewModel(EditPatient editPatient, vwClinicPatient patientToEdit)
        {
            view = editPatient;
            oldUsername = patientToEdit.Username;
            oldIdCardNumber = patientToEdit.IDCardNumber;
            oldHealthCardNumber = patientToEdit.HealthCardNumber;

            adminService = new ClinicAminService();
            userService = new UserService();
            maintenaceService = new ClinicMaintenaceService();
            managerService = new ClinicManagerService();
            patientService = new PatientService();
            doctorService = new DoctorService();

            User = new tblUser();
            Patient = patientToEdit;

            Doctors = doctorService.GetDoctors();


            if (Doctors.Count == 0)
            {
                NoDoctors = Visibility.Visible;
            }
            else
            {
                NoDoctors = Visibility.Hidden;
            }



        }



        List<tblClinicDoctor> Doctors;



        private Visibility noDoctors;
        public Visibility NoDoctors
        {
            get
            {
                return noDoctors;
            }
            set
            {
                noDoctors = value;
                OnPropertyChanged("NoDoctors");
            }
        }

        private tblUser user;
        public tblUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
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

        private DateTime dateOfBirth = DateTime.Now;
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; OnPropertyChanged("DateOfBirth"); }
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
                //MessageBox.Show(Patient.PatientID.ToString());
                //return;


                if (!ValidationClass.IsHealthCardNumber(Patient.HealthCardNumber))
                {
                    MessageBox.Show("IDCardNumber is not valid");
                    return;
                }



                tblUser userInDb = userService.GetUserByUserName(User.Username);

                if (userInDb != null && !userInDb.Username.Equals(oldUsername))
                {
                    string str1 = string.Format("User with this username exists\n" +
                        "Enter another username");
                    MessageBox.Show(str1);
                    return;
                }

                userInDb = userService.GetUserByIdCardNumber(User.IDCardNumber);

                if (userInDb != null && !userInDb.IDCardNumber.Equals(oldIdCardNumber))
                {
                    string str1 = string.Format("User with this IDCardNumber exists\n" +
                        "Enter another IDCardNumber");
                    MessageBox.Show(str1);
                    return;
                }

                tblClinicPatient patientInDb =
                    patientService.GetPatientByHealthCardNumber(Patient.HealthCardNumber);

                if (patientInDb != null && !patientInDb.HealthCardNumber.Equals(oldHealthCardNumber))
                {
                    string str1 = string.Format("Patient with this HealthCardNumber exists\n" +
                        "Enter another HealthCardNumber");
                    MessageBox.Show(str1);
                    return;
                }


                var passwordBox = parameter as PasswordBox;
                var password = passwordBox.Password;

                string encryptedString = EncryptionHelper.Encrypt(password);
                Patient.Gender = Gender;
                Patient.DateOfBirth = DateOfBirth;


                


               
                Patient.HealthAssuranceExpiryDate = DateTime.Today.AddYears(5);
              
                patientService.EditPatient(Patient);

                string str = string.Format("You edited Patient");
                MessageBox.Show(str);


                view.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute(object parameter)
        {

            if (String.IsNullOrEmpty(Patient.FullName) || String.IsNullOrEmpty(Patient.IDCardNumber)
                || String.IsNullOrEmpty(Patient.Nationality)
                || String.IsNullOrEmpty(Patient.Username) || parameter as PasswordBox == null
                || String.IsNullOrEmpty((parameter as PasswordBox).Password)
                || String.IsNullOrEmpty(Patient.HealthCardNumber))
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
    }
}
