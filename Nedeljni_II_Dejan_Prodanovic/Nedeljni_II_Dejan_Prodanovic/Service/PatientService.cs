using Nedeljni_II_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_II_Dejan_Prodanovic.Service
{
    class PatientService : IPatientService
    {
        public tblClinicPatient AddPatient(tblClinicPatient patient)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    tblClinicPatient newPatient = new tblClinicPatient();
                  
                    newPatient.HealthCardNumber = patient.HealthCardNumber;
                    newPatient.HealthAssuranceExpiryDate = patient.HealthAssuranceExpiryDate;
                    newPatient.DoctorUniqueNumber = patient.DoctorUniqueNumber;
                    newPatient.UserID = patient.UserID;
                    newPatient.DoctorId = patient.DoctorId;

                    context.tblClinicPatients.Add(newPatient);
                    context.SaveChanges();

                    return newPatient;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<tblClinicPatient> GetPatients()
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    List<tblClinicPatient> list = new List<tblClinicPatient>();
                    list = (from x in context.tblClinicPatients select x).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<vwClinicPatient> GetvwPatients()
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    List<vwClinicPatient> list = new List<vwClinicPatient>();
                    list = (from x in context.vwClinicPatients select x).ToList();

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
