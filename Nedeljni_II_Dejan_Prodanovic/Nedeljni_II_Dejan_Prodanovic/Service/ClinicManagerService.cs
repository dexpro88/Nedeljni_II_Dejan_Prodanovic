using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nedeljni_II_Dejan_Prodanovic.Model;

namespace Nedeljni_II_Dejan_Prodanovic.Service
{
    class ClinicManagerService : IClinicManagerService
    {
        public tblClinicManager AddManager(tblClinicManager manager)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    tblClinicManager newManager = new tblClinicManager();
                    newManager.UserID = manager.UserID;
                   
                    newManager.ManagerID = manager.ManagerID;
                    newManager.MinNumberOdRooms = manager.MinNumberOdRooms;
                    newManager.MaxNumberOfDoctors = manager.MaxNumberOfDoctors;
                    newManager.ManagerFloor = manager.ManagerFloor;

                    context.tblClinicManagers.Add(newManager);
                    context.SaveChanges();

                    return newManager;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public void DeleteManager(int managerId)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    tblClinicManager managerToDelete = (from u in context.tblClinicManagers
                                                              where u.ManagerID == managerId
                                                        select u).First();

                    int userId = (int)managerToDelete.UserID;

                    tblUser userToDelete = (from u in context.tblUsers
                                            where u.UserID == userId
                                            select u).First();

                    context.tblClinicManagers.Remove(managerToDelete);
                    context.tblUsers.Remove(userToDelete);

                    context.SaveChanges();


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        public void EditManager(vwClinicManager manager)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    tblClinicManager managerToEdit = (from u in context.tblClinicManagers
                                                            where u.ManagerID == manager.ManagerID
                                                            select u).First();
                    tblUser userToEdit = (from u in context.tblUsers
                                          where u.UserID == manager.UserID
                                          select u).First();


                    managerToEdit.MinNumberOdRooms =
                    manager.MinNumberOdRooms;
                    managerToEdit.MaxNumberOfDoctors =
                    manager.MaxNumberOfDoctors;
                    managerToEdit.NumberOfOmissions =
                   manager.NumberOfOmissions;
                    managerToEdit.ManagerFloor =
                   manager.ManagerFloor;

                    userToEdit.Username = manager.Username;
                    userToEdit.FullName = manager.FullName;
                    userToEdit.Gender = manager.Gender;
                    userToEdit.IDCardNumber = manager.IDCardNumber;
                    userToEdit.Nationality = manager.Nationality;

                    context.SaveChanges();



                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());

            }
        }

        public List<tblClinicDoctor> GetDoctorsOfManager(int managerId)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    List<tblClinicDoctor> list = new List<tblClinicDoctor>();
                    list = (from x in context.tblClinicDoctors
                            where x.ManagerID == managerId
                            select x).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public vwClinicManager GetManagerByManagerId(int managerId)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {


                    vwClinicManager manager = (from x in context.vwClinicManagers
                                               where x.ManagerID == managerId
                                               select x).First();

                    return manager;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public vwClinicManager GetManagerByUserId(int userId)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {


                    vwClinicManager manager = (from x in context.vwClinicManagers
                                                where x.UserID == userId
                                                select x).First();

                    return manager;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<tblClinicManager> GetManagers()
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    List<tblClinicManager> list = new List<tblClinicManager>();
                    list = (from x in context.tblClinicManagers select x).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<vwClinicManager> GetvwManagers()
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    List<vwClinicManager> list = new List<vwClinicManager>();
                    list = (from x in context.vwClinicManagers select x).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public void PunishManager(int managerId)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    tblClinicManager managerToEdit = (from u in context.tblClinicManagers
                                                        where u.ManagerID == managerId
                                                        select u).First();



                    if (managerToEdit.NumberOfOmissions==null)
                    {
                        managerToEdit.NumberOfOmissions = 1;
                    }
                    else
                    {
                        managerToEdit.NumberOfOmissions++;
                    }
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
