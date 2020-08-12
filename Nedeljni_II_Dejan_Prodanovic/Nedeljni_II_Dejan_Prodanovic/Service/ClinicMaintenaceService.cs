using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Nedeljni_II_Dejan_Prodanovic.Model;

namespace Nedeljni_II_Dejan_Prodanovic.Service
{
    class ClinicMaintenaceService : IClinicMaintenaceService
    {
        public tblClinicMaintenace AddMaintenace(tblClinicMaintenace maintenace)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {

                    if (GetMaintenaces().Count==3)
                    {
                        
                        tblClinicMaintenace maintenaceToDelete = GetMaintenace();
                        DeleteMaintenace(maintenaceToDelete.ClinicMaintenaceID);
                    }
                    tblClinicMaintenace newMaintenace = new tblClinicMaintenace();
                    newMaintenace.UserID = maintenace.UserID;
                    newMaintenace.CanChooseClinicExpansionPermission = 
                        maintenace.CanChooseClinicExpansionPermission;
                    newMaintenace.CanChooseInvalidAccess =
                    maintenace.CanChooseInvalidAccess;

                    context.tblClinicMaintenaces.Add(newMaintenace);
                    context.SaveChanges();

                    return newMaintenace;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public void DeleteMaintenace(int maintenaceId)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    tblClinicMaintenace maintenaceToDelete = (from u in context.tblClinicMaintenaces
                                                  where u.ClinicMaintenaceID == maintenaceId
                                                              select u).First();

                    int userId = (int) maintenaceToDelete.UserID;

                    tblUser userToDelete = (from u in context.tblUsers
                                            where u.UserID == userId
                                            select u).First();
                    context.tblClinicMaintenaces.Remove(maintenaceToDelete);
                    context.tblUsers.Remove(userToDelete);

                    context.SaveChanges();


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        public vwClinicMaintenace GetMaintenaceByUserId(int userId)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {


                    vwClinicMaintenace admin = (from x in context.vwClinicMaintenaces
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

        public tblClinicMaintenace GetMaintenace()
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    tblClinicMaintenace maintenace = new tblClinicMaintenace();
                    maintenace = (from x in context.tblClinicMaintenaces
                                  select x).FirstOrDefault();


                    return maintenace;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<tblClinicMaintenace> GetMaintenaces()
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    List<tblClinicMaintenace> list = new List<tblClinicMaintenace>();
                    list = (from x in context.tblClinicMaintenaces select x).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<vwClinicMaintenace> GetvwMaintenaces()
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    List<vwClinicMaintenace> list = new List<vwClinicMaintenace>();
                    list = (from x in context.vwClinicMaintenaces select x).ToList();

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
