using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.HR.Personnel.Employee
{
    public class EmployeeInfo
    {
        private string _firstName;
        private string _lastName;
        private Dictionary<String, String> _errors = new Dictionary<string, string>();

        public int EmpID { get; set; }
        public string EmpCode { get; set; }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    _errors.Add("FirstName", "FirstName cannot be blank!");

                _firstName = value;
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    _errors.Add("LastName", "LastName cannot be blank!");

                _lastName = value;
            }
        }


        public int Gender { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public string Mobile { get; set; }

        public int? FileId { get; set; }

        public string FileUrl { get; set; }


        public string FullName { get; set; }
        public string EmpName { get { return FirstName + ", " + LastName; } }
        public string CompanyName { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public string Position { get; set; }
        public string IDNo { get; set; }
        public Nullable<DateTime> IDIssuedDate { get; set; }
        public Nullable<int> IDIssuedPlace { get; set; }
        public string IDIssuedPlaceName { get; set; }
        public string TaxNo { get; set; }
        public string PAddress { get; set; }
        public Nullable<int> PLSCountryID { get; set; }
        public Nullable<int> PLSProvinceID { get; set; }
        public Nullable<int> PLSDistrictID { get; set; }
        public Nullable<int> TLSCountryID { get; set; }
        public Nullable<int> TLSProvinceID { get; set; }
        public Nullable<int> TLSDistrictID { get; set; }
        public string TAddress { get; set; }

        public int LSCompanyID { get; set; }

        public string LSCompanyName { get; set; }

        public Nullable<int> LSLevel1ID { get; set; }

        public string LSLevel1Name { get; set; }

        public Nullable<int> LSLevel2ID { get; set; }

        public string LSLevel2Name { get; set; }

        public Nullable<DateTime> DOB { get; set; }

        public Nullable<int> YOB { get; set; }


        public Nullable<DateTime> StartDate { get; set; }


        public Nullable<DateTime> JoinDate { get; set; }
        public Nullable<int> BornLSCountryID { get; set; }
        public Nullable<int> BornLSProvinceID { get; set; }
        public Nullable<int> BornLSDistrictID { get; set; }
        public Nullable<int> NativeCountryID { get; set; }
        public Nullable<int> NativeProvinceID { get; set; }
        public Nullable<int> NativeDistrictID { get; set; }

        public Nullable<int> LSMaritalID { get; set; }
        public string LSMaritalName { get; set; }


        public Nullable<int> LSNationalityID { get; set; }
        public string LSNationalityName { get; set; }

        public Nullable<int> LSEthnicID { get; set; }

        public string LSEthnicName { get; set; }

        public Nullable<int> LSReligionID { get; set; }
        public string LSReligionName { get; set; }

        public Nullable<int> LSEducationID { get; set; }

        public string LSEducationName { get; set; }

        public Nullable<int> LSQualificationID { get; set; }

        public string LSQualificationName { get; set; }

        public Nullable<int> LSMajorID { get; set; }
        public string LSMajorName { get; set; }

        public Nullable<int> LineManagerID { get; set; }
        public string LineManagerName { get; set; }

        public Nullable<int> LSLocationID { get; set; }

        public string LSLocationName { get; set; }

        public Nullable<int> LSPositionID { get; set; }

        public string LSPositionName { get; set; }

        public string AccountNumber { get; set; }

        public string AccountName { get; set; }

        public Nullable<int> LSBankID { get; set; }

        public string LSBankName { get; set; }

        public Nullable<int> LSBankBranchID { get; set; }
        public string LSBankBranchName { get; set; }


        public Nullable<bool> SelfDeduction { get; set; }


        public Nullable<bool> DependDeduction { get; set; }

        public Nullable<int> NoOfDependent { get; set; }

        public string SIBook { get; set; }

        public string HIBook { get; set; }
        public Nullable<bool> PayByBank { get; set; }

        public string EmergencyContact { get; set; }

        public string EmergencyAddess { get; set; }

        public string EmergencyPhone { get; set; }

        public string EmergencyMobile { get; set; }

        public bool? Active { get; set; }
    }
}
