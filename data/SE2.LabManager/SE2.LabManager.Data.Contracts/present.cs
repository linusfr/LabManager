//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SE2.LabManager.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    
    public partial class present
    {
        public int presentID { get; set; }
        public sbyte wasPresent { get; set; }
        public string note { get; set; }
        public int labdate_labdateID { get; set; }
        public int student_studentID { get; set; }
    
        public virtual labdate labdate { get; set; }
        public virtual student student { get; set; }
    }
}