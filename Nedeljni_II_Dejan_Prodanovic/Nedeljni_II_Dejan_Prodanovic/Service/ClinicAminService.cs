using Nedeljni_II_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_II_Dejan_Prodanovic.Service
{
    class ClinicAminService : IClinicAminService
    {
        public tblClinicAdmin AddAdmin(tblClinicAdmin admin)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {

                    tblClinicAdmin newAdmin = new tblClinicAdmin();
                    newAdmin.UserID = admin.UserID;
                    newAdmin.HasCreatedClinic = admin.HasCreatedClinic;
                    

                    context.tblClinicAdmins.Add(newAdmin);
                    context.SaveChanges();

                    return newAdmin;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblClinicAdmin GetAdminByUserId(int userId)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {


                    tblClinicAdmin admin = (from x in context.tblClinicAdmins
                                      where x.UserID == userId
                                      select x).First();

                    return admin;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<tblClinicAdmin> GetAdmins()
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    List<tblClinicAdmin> list = new List<tblClinicAdmin>();
                    list = (from x in context.tblClinicAdmins select x).ToList();

                     return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public void SetHasCreatedClinic(tblClinicAdmin admin)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {

                    tblClinicAdmin adminToEdit = (from u in context.tblClinicAdmins
                                                where u.AdminID == admin.AdminID
                                                select u).First();


                    adminToEdit.HasCreatedClinic = true;


                    context.SaveChanges();


                    

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
               
            }
        }
    }
}
