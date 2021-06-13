using System;
using System.Collections.Generic;

#nullable disable

namespace projetIntegrateur.Models
{
    public partial class Partenaire
    {
        public int IdPartenaire { get; set; }
        public string NomPartenaire { get; set; }
        public string PrenomPartenaire { get; set; }
        public string FonctionPartenaire { get; set; }
        public string EmailPartenaire { get; set; }
        public int TelPartenaire { get; set; }
    }
}
