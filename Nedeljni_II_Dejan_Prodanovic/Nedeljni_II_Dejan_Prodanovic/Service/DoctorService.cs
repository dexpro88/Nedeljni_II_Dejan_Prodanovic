using Nedeljni_II_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_II_Dejan_Prodanovic.Service
{
    class DoctorService : IDoctorService
    {
        public tblClinicDoctor AddDoctor(tblClinicDoctor doctor)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    tblClinicDoctor newDoctor = new tblClinicDoctor();
                    newDoctor.UserID = doctor.UserID;

                    newDoctor.AccountNumber = doctor.AccountNumber;
                    newDoctor.UniqueNumber = doctor.UniqueNumber;
                    newDoctor.Sector = doctor.Sector;
                    newDoctor.DoctorShift = doctor.DoctorShift;
                    newDoctor.RecievesPatients = doctor.RecievesPatients;
                    newDoctor.ManagerID = doctor.ManagerID;

                    context.tblClinicDoctors.Add(newDoctor);
                    context.SaveChanges();

                    return newDoctor;

      
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public void DeleteDoctor(int doctorId)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    tblClinicDoctor doctorToDelete = (from u in context.tblClinicDoctors
                                                        where u.DoctorID == doctorId
                                                      select u).First();

                    int userId = (int)doctorToDelete.UserID;

                    tblUser userToDelete = (from u in context.tblUsers
                                            where u.UserID == userId
                                            select u).First();

                    context.tblClinicDoctors.Remove(doctorToDelete);
                    context.tblUsers.Remove(userToDelete);

                    context.SaveChanges();


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        public tblClinicDoctor GetDoctorByAccountNumber(string AccountNumber)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {


                    tblClinicDoctor doctor = (from x in context.tblClinicDoctors
                                              where x.AccountNumber.Equals(AccountNumber)

                                              select x).First();

                    return doctor;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblClinicDoctor GetDoctorByUniqueNumber(string UniqueNumber)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {


                    tblClinicDoctor doctor = (from x in context.tblClinicDoctors
                                    where x.UniqueNumber.Equals(UniqueNumber)

                                    select x).First();

                    return doctor;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public vwClinicDoctor GetDoctorByUserId(int userId)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {


                    vwClinicDoctor doctor = (from x in context.vwClinicDoctors
                                               where x.UserID == userId
                                               select x).First();

                    return doctor;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<tblClinicDoctor> GetDoctors()
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    List<tblClinicDoctor> list = new List<tblClinicDoctor>();
                    list = (from x in context.tblClinicDoctors select x).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<vwClinicDoctor> GetvwDoctors()
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    List<vwClinicDoctor> list = new List<vwClinicDoctor>();
                    list = (from x in context.vwClinicDoctors select x).ToList();

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
