//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SOCIETYMASTER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SOCIETYMASTER()
        {
            this.PERSONINFOes = new HashSet<PERSONINFO>();
        }
    
        public int ID { get; set; }
        public string SOCIETYNAME { get; set; }
        public string LANDMARK { get; set; }
        public Nullable<int> WARD_ID { get; set; }
        public Nullable<int> MATDAN_ID { get; set; }
        public Nullable<int> BOOTH_ID { get; set; }
        public Nullable<bool> EF1 { get; set; }
        public Nullable<bool> EF2 { get; set; }
        public Nullable<bool> EF3 { get; set; }
        public Nullable<bool> EF4 { get; set; }
        public Nullable<bool> EF5 { get; set; }
    
        public virtual BOOTHMASTER BOOTHMASTER { get; set; }
        public virtual MATDANMATHAKMASTER MATDANMATHAKMASTER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PERSONINFO> PERSONINFOes { get; set; }
        public virtual WARDMASTER WARDMASTER { get; set; }
    }
}
