//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkerJob
    {
        public int ID { get; set; }
        public int WorkerID { get; set; }
        public int JobID { get; set; }
    
        public virtual Job Job { get; set; }
        public virtual Worker Worker { get; set; }
    }
}
