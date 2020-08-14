using Nedeljni_II_Dejan_Prodanovic.Command;
using Nedeljni_II_Dejan_Prodanovic.Model;
using Nedeljni_II_Dejan_Prodanovic.Service;
using Nedeljni_II_Dejan_Prodanovic.Utility;
using Nedeljni_II_Dejan_Prodanovic.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nedeljni_II_Dejan_Prodanovic.ViewModel
{
    class LoginViewModel:ViewModelBase
    {
        LoginView view;
        Dictionary<string, string> credentialsFromFile = new Dictionary<string, string>();
        IUserService userService;
        IClinicAminService adminService;
        IClinicMaintenaceService maintenaceService;
        IClinicManagerService managerService;
        IDoctorService doctorService;
        IPatientService patientService;

        public LoginViewModel(LoginView loginView)
        {
            view = loginView;
            ReadCredentialsFromFile();
            userService = new UserService();
            adminService = new ClinicAminService();
            maintenaceService = new ClinicMaintenaceService();
            managerService = new ClinicManagerService();
            doctorService = new DoctorService();
            patientService = new PatientService();
        }

        private string userName;
        public string UserName
        {

            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        public string Error
        {
            get { return null; }
        }

        public string this[string someProperty]
        {
            get
            {

                return string.Empty;
            }
        }



        private ICommand submitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                if (submitCommand == null)
                {
                    submitCommand = new RelayCommand(Submit);
                    return submitCommand;
                }
                return submitCommand;
            }
        }

        void Submit(object obj)
        {

            string password = (obj as PasswordBox).Password;



            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Wrong user name or password");
                return;
            }
            if (UserName.Equals(credentialsFromFile["username"]) &&
                password.Equals(credentialsFromFile["password"]))
            {
                PredifinedAccount predifinedAccount = new PredifinedAccount();
                view.Close();
                predifinedAccount.Show();
                return;
            }

            string encryptedString = EncryptionHelper.Encrypt(password);

            tblUser user = userService.GetUserByUserNameAndPassword(UserName, encryptedString);
            if (user != null)
            {
                tblClinicAdmin admin = adminService.GetAdminByUserId(user.UserID);

                if (admin != null)
                {
                    if (admin.HasCreatedClinic==false)
                    {
                        ClinicAdminFirstLogin adminMainView = new ClinicAdminFirstLogin(admin);
                        view.Close();
                        adminMainView.Show();
                       
                        return;
                    }
                    else
                    {
                        ClinicAdminMain adminMain = new ClinicAdminMain(admin);
                        adminMain.Show();
                        view.Close();
                    }
                   
                }
                vwClinicMaintenace maintenace = maintenaceService.GetMaintenaceByUserId(user.UserID);

                if (maintenace != null)
                {

                    ClinicMaintenanceMain maintenanceMain = new ClinicMaintenanceMain(maintenace);
                    view.Close();
                    maintenanceMain.Show();

                        return;
                   
                }

                vwClinicManager manager = managerService.GetManagerByUserId(user.UserID);

                if (manager != null)
                {

                    ClinicManagerMain managerMain = new ClinicManagerMain(manager);
                    view.Close();
                    managerMain.Show();

                    return;

                }

                vwClinicDoctor doctor = doctorService.GetDoctorByUserId(user.UserID);

                if (doctor != null)
                {

                    DoctorMainView doctorMainView = new DoctorMainView();
                    view.Close();
                    doctorMainView.Show();

                    return;

                }

                vwClinicPatient patient = patientService.GetPatientByUserId(user.UserID);

                if (patient != null)
                {

                    PatientMainView patientMainView = new PatientMainView(patient);
                    view.Close();
                    patientMainView.Show();

                    return;

                }
            }
            

            else
            {
                MessageBox.Show("Wrong username or password");

            }


        }

        private ICommand register;
        public ICommand Register
        {
            get
            {
                if (register == null)
                {
                    register = new RelayCommand(param => RegisterExecute(), param => CanRegisterExecute());
                }
                return register;
            }
        }

        private void RegisterExecute()
        {
            try
            {
                RegisterView register = new RegisterView();
                register.Show();
                view.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanRegisterExecute()
        {

            return true;
        }

        private void ReadCredentialsFromFile()
        {
            try
            {
                
                using (StreamReader sr = new StreamReader(@"..\..\ClinicAccess.txt"))
                {
                    string line;
                     
                    while ((line = sr.ReadLine()) != null)
                    {
                        string []lineStr = line.Split(':');
                      
                        credentialsFromFile.Add(lineStr[0],lineStr[1]);
                    }
                }
            }
            catch (Exception e)
            {
                 
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

    
    }
}
