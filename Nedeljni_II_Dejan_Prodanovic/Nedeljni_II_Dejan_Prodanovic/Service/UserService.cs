using Nedeljni_II_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_II_Dejan_Prodanovic.Service
{
    class UserService:IUserService
    {
     
        public tblUser AddUser(tblUser user)
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {

                    tblUser newUser = new tblUser();
                    newUser.FullName = user.FullName;
                    newUser.IDCardNumber = user.IDCardNumber;
                    newUser.Gender = user.Gender;
                    newUser.Gender = user.Gender;
                    newUser.DateOfBirth = user.DateOfBirth;
                    newUser.Nationality = user.Nationality;
                    newUser.Username = user.Username;
                    newUser.Password = user.Password;

                    context.tblUsers.Add(newUser);
                    context.SaveChanges(); 
	 

                    return newUser;


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
