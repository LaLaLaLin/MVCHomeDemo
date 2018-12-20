//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Exam.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StuQueRecord
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StuQueRecord()
        {
            this.Results = new HashSet<Result>();
            this.StuQueRecordDetails = new HashSet<StuQueRecordDetail>();
        }
    
        public int RecordID { get; set; }
        public int StudetnID { get; set; }
        public int PaperID { get; set; }
        public System.DateTime RecordTime { get; set; }
    
        public virtual Paper Paper { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }
        public virtual Student Student { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StuQueRecordDetail> StuQueRecordDetails { get; set; }
    }
}