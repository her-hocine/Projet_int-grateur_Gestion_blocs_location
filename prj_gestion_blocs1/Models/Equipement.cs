using System;
using System.Collections.Generic;

#nullable disable

namespace projetIntegrateur.Models
{
    public partial class Equipement
    {
        public int IdEquipement { get; set; }
        public int IdPiece { get; set; }
        public string NomEquipement { get; set; }
        public string CategorieEquipement { get; set; }
        public string FournisseurEquipement { get; set; }
        public string MarqueEquipement { get; set; }
        public DateTime DateAchatEquip { get; set; }
        public DateTime DateFinGarantieEquip { get; set; }
        public string EtatEquipement { get; set; }
        public string ImageEquipement { get; set; }

        public virtual Piece IdPieceNavigation { get; set; }
    }
}
