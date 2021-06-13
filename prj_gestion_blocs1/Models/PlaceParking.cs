using System;
using System.Collections.Generic;

#nullable disable

namespace projetIntegrateur.Models
{
    public partial class PlaceParking
    {
        public int IdPlace { get; set; }
        public int IdBloc { get; set; }
        public int IdLocataire { get; set; }
        public string DispoPark { get; set; }

        public virtual Bloc IdBlocNavigation { get; set; }
        public virtual Locataire IdLocataireNavigation { get; set; }
    }
}
