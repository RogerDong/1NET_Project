//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _MP22NET__201091___DONG_RongYu___HEBUT
{
    using System;
    using System.Collections.Generic;
    
    public partial class RestaurantMap
    {
        public RestaurantMap()
        {
            this.Bill = new HashSet<Bill>();
        }
    
        public int Position { get; set; }
        public string Existing { get; set; }
        public int Chairs { get; set; }
        public string Status { get; set; }
        public int IfSelected { get; set; }
        public Nullable<int> HandledBy { get; set; }
    
        public virtual ICollection<Bill> Bill { get; set; }
        public virtual Waiters Waiters { get; set; }
    }
}
