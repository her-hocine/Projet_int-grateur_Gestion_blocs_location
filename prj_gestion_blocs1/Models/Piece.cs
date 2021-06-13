using System;
using System.Collections.Generic;

#nullable disable

namespace projetIntegrateur.Models
{
    public partial class Piece
    {
        public Piece()
        {
            Equipements = new HashSet<Equipement>();
        }

        public int IdPiece { get; set; }
        public int IdAppartement { get; set; }
        public double SuperficiePiece { get; set; }
        public string CouleurPiece { get; set; }
        public DateTime DateRenovation { get; set; }
        public string ImagePiece { get; set; }

        public virtual Appartement IdAppartementNavigation { get; set; }
        public virtual ICollection<Equipement> Equipements { get; set; }
    }
}
