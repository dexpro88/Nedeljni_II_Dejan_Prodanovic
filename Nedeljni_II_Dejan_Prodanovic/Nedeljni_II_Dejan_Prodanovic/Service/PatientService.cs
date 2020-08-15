using Nedeljni_II_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public void DeletePatient(int patientId)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    tblClinicPatient patientToDelete = (from u in context.tblClinicPatients
                                                      where u.PatientID == patientId
                                                        select u).First();

                    int userId = (int)patientToDelete.UserID;

                    tblUser userToDelete = (from u in context.tblUsers
                                            where u.UserID == userId
                                            select u).First();

                    context.tblClinicPatients.Remove(patientToDelete);
                    context.tblUsers.Remove(userToDelete);

                    context.SaveChanges();


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        public void EditPatient(vwClinicPatient patient)
        {
            
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    tblClinicPatient patientToEdit = (from u in context.tblClinicPatients
                                                      where u.PatientID == patient.PatientID
                                                      select u).First();
                    tblUser userToEdit = (from u in context.tblUsers
                                          where u.UserID == patient.UserID
                                          select u).First();


                    patientToEdit.HealthCardNumber = patient.HealthCardNumber;
                    patientToEdit.HealthAssuranceExpiryDate = patient.HealthAssuranceExpiryDate;
                    patientToEdit.DoctorUniqueNumber = patient.DoctorUniqueNumber;
                    

                    userToEdit.Username = patient.Username;
                    userToEdit.FullName = patient.FullName;
                    userToEdit.Gender = patient.Gender;
                    userToEdit.IDCardNumber = patient.IDCardNumber;
                    userToEdit.Nationality = patient.Nationality;

                    context.SaveChanges();

                    
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());

            }
        }

        public tblClinicPatient GetPatientByHealthCardNumber(string healthCardNumber)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {


                    tblClinicPatient patient = (from x in context.tblClinicPatients
                                              where x.HealthCardNumber.Equals(healthCardNumber)

                                              select x).First();

                    return patient;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public vwClinicPatient GetPatientByUserId(int userId)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {


                    vwClinicPatient patient = (from x in context.vwClinicPatients
                                             where x.UserID == userId
                                             select x).First();

                    return patient;
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
