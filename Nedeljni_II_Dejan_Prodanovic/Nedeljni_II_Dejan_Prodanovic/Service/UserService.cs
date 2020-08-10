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

        public tblUser GetUserByIdCardNumber(string IdCardNumber)
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {


                    tblUser user = (from x in context.tblUsers
                                    where x.IDCardNumber.Equals(IdCardNumber)

                                    select x).First();

                    return user;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblUser GetUserByUserName(string username)
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {


                    tblUser user = (from x in context.tblUsers
                                    where x.Username.Equals(username)

                                    select x).First();

                    return user;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblUser GetUserByUserNameAndPassword(string username, string password)
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {


                    tblUser user = (from x in context.tblUsers
                                    where x.Username.Equals(username)
                                    && x.Password.Equals(password)
                                    select x).First();

                    return user;
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
