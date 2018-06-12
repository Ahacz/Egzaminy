using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Egzaminy.ViewModels
{
    public class DajPrzedmioty
    {
        public int PrzedmID { get; set; }
        public string Nazwa { get; set; }
        public bool Przypisano { get; set; }
    }
}