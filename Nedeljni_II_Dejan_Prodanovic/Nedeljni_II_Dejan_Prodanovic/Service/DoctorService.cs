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
