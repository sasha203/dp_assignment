//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class Weather
    {
        public System.Guid W_Id { get; set; }
        public string Main { get; set; }
        public string Desc { get; set; }
        public Nullable<decimal> Temperature { get; set; }
        public Nullable<int> Humidity { get; set; }
        public Nullable<decimal> WindSpeed { get; set; }
        public string WindDirection { get; set; }
        public Nullable<decimal> WindDirectionInDegrees { get; set; }
        public System.DateTime TimeStamp { get; set; }
    }
}
