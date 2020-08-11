using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nedeljni_II_Dejan_Prodanovic.Model;

namespace Nedeljni_II_Dejan_Prodanovic.Service
{
    class ClinicService : IClinicService
    {
        public tblClinicInstitution AddClinic(tblClinicInstitution clinic)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {

                    tblClinicInstitution newClinic = new tblClinicInstitution();
                    newClinic.InstitutionName = clinic.InstitutionName;
                    newClinic.CreationDate = clinic.CreationDate;
                    newClinic.InstitutionOwner = clinic.InstitutionOwner;
                    newClinic.InstitutionAddress = clinic.InstitutionAddress;
                    newClinic.NumberOdFloors = clinic.NumberOdFloors;
                    newClinic.NumberOfRoomsPerFloor = clinic.NumberOfRoomsPerFloor;
                    newClinic.HasTerrace = clinic.HasTerrace;
                    newClinic.HasYard = clinic.HasYard;
                    newClinic.NumberOfAccessPointsForAmbulance =
                        clinic.NumberOfAccessPointsForAmbulance;
                    newClinic.NumberOfAccessPointsForInvalids =
                        clinic.NumberOfAccessPointsForInvalids;
                    
     
 
                    context.tblClinicInstitutions.Add(newClinic);
                    context.SaveChanges();


                    return newClinic;


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public void EditClinic(tblClinicInstitution clinic)
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {

                    tblClinicInstitution clinicToEdit = (from u in context.tblClinicInstitutions
                                                where u.InstitutionID == clinic.InstitutionID
                                                select u).First();

                    clinicToEdit.InstitutionOwner = clinic.InstitutionOwner;
                    clinicToEdit.NumberOfAccessPointsForAmbulance = 
                        clinic.NumberOfAccessPointsForAmbulance;
                    clinicToEdit.NumberOfAccessPointsForInvalids = clinic.NumberOfAccessPointsForInvalids;


                    context.SaveChanges();



                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());

            }
        }

        public tblClinicInstitution GetClinic()
        {
            try
            {
                using (MyClinicDBEntities context = new MyClinicDBEntities())
                {
                    tblClinicInstitution clinic = new tblClinicInstitution();
                    clinic = (from x in context.tblClinicInstitutions select x).FirstOrDefault();

                     
                    return clinic;
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
