//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Programming_Practice
{
    using System;
    using System.Collections.Generic;
    
    public partial class Log
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Log()
        {
            this.Progresses = new HashSet<Progress>();
        }
    
        public int ID { get; set; }
        public int Chapter_ID { get; set; }
        public int Type_ID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Level { get; set; }
        public string Hint_1 { get; set; }
        public string Hint_2 { get; set; }
        public string Hint_3 { get; set; }
        public string Description { get; set; }
    
        public virtual Chapter Chapter { get; set; }
        public virtual Type Type { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Progress> Progresses { get; set; }
    }
}
