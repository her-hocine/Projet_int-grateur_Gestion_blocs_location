using System;
using System.Collections.Generic;

#nullable disable

namespace projetIntegrateur.Models
{
    public partial class Bloc
    {
        public Bloc()
        {
            Appartements = new HashSet<Appartement>();
            PlaceParkings = new HashSet<PlaceParking>();
        }

        public int IdBloc { get; set; }
        public string AdresseBloc { get; set; }
        public int NbEtages { get; set; }
        public string Contracteur { get; set; }
        public DateTime AnneeConstruction { get; set; }
        public string ImageBloc { get; set; }

        public virtual ICollection<Appartement> Appartements { get; set; }
        public virtual ICollection<PlaceParking> PlaceParkings { get; set; }
    }
}
