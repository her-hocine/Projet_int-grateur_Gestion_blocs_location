using System;
using System.Collections.Generic;

#nullable disable

namespace projetIntegrateur.Models
{
    public partial class Locataire
    {
        public Locataire()
        {
            Appartements = new HashSet<Appartement>();
            PlaceParkings = new HashSet<PlaceParking>();
        }

        public int IdLocataire { get; set; }
        public string NomLocataire { get; set; }
        public string PrenomLocataire { get; set; }
        public string EmailLocataire { get; set; }
        public DateTime? DateDebutBail { get; set; }
        public string EtatCompteLocataire { get; set; }

        public virtual ICollection<Appartement> Appartements { get; set; }
        public virtual ICollection<PlaceParking> PlaceParkings { get; set; }
    }
}
