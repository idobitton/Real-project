//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp50
{
    using System;
    using System.Collections.Generic;
    
    public partial class order_details
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public order_details()
        {
            this.order = new HashSet<order>();
        }
    
        public int Id { get; set; }
        public Nullable<int> order_date_id { get; set; }
        public int products_id { get; set; }
        public Nullable<int> final_price_id { get; set; }
        public string notes { get; set; }
    
        public virtual final_price final_price { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> order { get; set; }
        public virtual order_date order_date { get; set; }
        public virtual products products { get; set; }
    }
}
