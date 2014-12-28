//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Eagle.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class REC_tblQualification
    {
        public int QualificationID { get; set; }
        public int CandidateID { get; set; }
        public string QualificationNo { get; set; }
        public int LSQualificationID { get; set; }
        public Nullable<System.DateTime> QualificationDate { get; set; }
        public Nullable<System.DateTime> FromMonth { get; set; }
        public Nullable<System.DateTime> ToMonth { get; set; }
        public Nullable<int> LSSchoolID { get; set; }
        public string OtherSchool { get; set; }
        public Nullable<int> LSFacultyID { get; set; }
        public string OtherFaculty { get; set; }
        public Nullable<int> LSMajorID { get; set; }
        public Nullable<int> LSTrainingTypeID { get; set; }
        public Nullable<bool> PayByCompany { get; set; }
        public Nullable<int> TrainingPlace { get; set; }
        public Nullable<int> LSCountryID { get; set; }
        public string AttachFile { get; set; }
        public int Priority { get; set; }
        public string Note { get; set; }
    
        public virtual LS_tblCountry LS_tblCountry { get; set; }
        public virtual LS_tblFaculty LS_tblFaculty { get; set; }
        public virtual LS_tblMajor LS_tblMajor { get; set; }
        public virtual LS_tblQualification LS_tblQualification { get; set; }
        public virtual LS_tblSchool LS_tblSchool { get; set; }
        public virtual LS_tblTrainingType LS_tblTrainingType { get; set; }
        public virtual REC_tblCandidate REC_tblCandidate { get; set; }
    }
}
