//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project2021
{
    using System;
    using System.Collections.Generic;
    
    public partial class AuditTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AuditTable()
        {
            this.QuesTransaction_table = new HashSet<QuesTransaction_table>();
            this.Form4Details = new HashSet<Form4Details>();
            this.HSE_details_tbl = new HashSet<HSE_details_tbl>();
            this.NCReport_details = new HashSet<NCReport_details>();
        }
    
        public System.DateTime dateOfAudit { get; set; }
        public string siteName { get; set; }
        public string siteDetails { get; set; }
        public string auditTeam { get; set; }
        public string observerTeam { get; set; }
        public string strenghts { get; set; }
        public string MajorNC { get; set; }
        public string MinorNC { get; set; }
        public string OFIs { get; set; }
        public int id { get; set; }
        public string Audit_ID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<bool> IsSubmittedToObserver { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuesTransaction_table> QuesTransaction_table { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Form4Details> Form4Details { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HSE_details_tbl> HSE_details_tbl { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NCReport_details> NCReport_details { get; set; }
    }
}
