//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Egzaminy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Egzamin
    {
        public Egzamin()
        {
            this.Wykladowca = new HashSet<ApplicationUser>();
            this.Rokk = new HashSet<Rok>();
        }
        public int Id { get; set; }
        public System.DateTime Data { get; set; }
        public int CzasRozpoczecia { get; set; }
        public int CzasTrwania { get; set; }
        public int Sala { get; set; }
    
       // public virtual AspNetUsers AspNetUsers { get; set; }
       [Display(Name ="Rok")]
        public virtual ICollection<Rok> Rokk { get; set; }
        [Display(Name = "Wykładowca")]
        public virtual ICollection<ApplicationUser> Wykladowca { get; set; }
    }
}
