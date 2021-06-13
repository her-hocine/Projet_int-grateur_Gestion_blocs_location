using System;
using System.Collections.Generic;

#nullable disable

namespace projetIntegrateur.Models
{
    public partial class Membre
    {
        public int IdMembre { get; set; }
        public string NomMembre { get; set; }
        public string PrenomMembre { get; set; }
        public string EmailMembre { get; set; }
        public string MotPasseMembre { get; set; }
        public int TelMembre { get; set; }
    }
}
