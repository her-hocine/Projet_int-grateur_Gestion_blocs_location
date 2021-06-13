using System;
using System.Collections.Generic;

#nullable disable

namespace projetIntegrateur.Models
{
    public partial class Appartement
    {
        public Appartement()
        {
            Pieces = new HashSet<Piece>();
        }

        public int IdAppartement { get; set; }
        public int IdBloc { get; set; }
        public int IdLocataire { get; set; }
        public double SuperficieAppartement { get; set; }
        public int NbBalcons { get; set; }
        public double PrixLocation { get; set; }

        public virtual Bloc IdBlocNavigation { get; set; }
        public virtual Locataire IdLocataireNavigation { get; set; }
        public virtual ICollection<Piece> Pieces { get; set; }
    }
}
