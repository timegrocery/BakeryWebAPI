//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MonAn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MonAn()
        {
            this.ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
            this.ChiTietMonAns = new HashSet<ChiTietMonAn>();
        }
    
        public int MonAn_ID { get; set; }
        public string MonAn_Ten { get; set; }
        public string MonAn_DonViTinh { get; set; }
        public Nullable<decimal> MonAn_Gia { get; set; }
        public string MonAn_HinhAnh { get; set; }
        public string MonAn_Loai { get; set; }
        public string MonAn_TrangThai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietMonAn> ChiTietMonAns { get; set; }
    }
}
