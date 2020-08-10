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
                using (ClinicDBEntities context = new ClinicDBEntities())
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
                using (ClinicDBEntities context = new ClinicDBEntities())
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
                using (ClinicDBEntities context = new ClinicDBEntities())
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
    }
}
