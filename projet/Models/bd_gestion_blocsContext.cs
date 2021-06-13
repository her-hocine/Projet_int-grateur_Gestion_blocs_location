using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace projetIntegrateur.Models
{
    public partial class bd_gestion_blocsContext : DbContext
    {
        public bd_gestion_blocsContext()
        {
        }

        public bd_gestion_blocsContext(DbContextOptions<bd_gestion_blocsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appartement> Appartements { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Bloc> Blocs { get; set; }
        public virtual DbSet<Equipement> Equipements { get; set; }
        public virtual DbSet<Locataire> Locataires { get; set; }
        public virtual DbSet<Membre> Membres { get; set; }
        public virtual DbSet<Partenaire> Partenaires { get; set; }
        public virtual DbSet<Piece> Pieces { get; set; }
        public virtual DbSet<PlaceParking> PlaceParkings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=bd_gestion_blocs;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Appartement>(entity =>
            {
                entity.HasKey(e => e.IdAppartement);

                entity.ToTable("Appartement");

                entity.Property(e => e.NbBalcons).HasColumnName("Nb_balcons");

                entity.Property(e => e.PrixLocation).HasColumnName("Prix_location");

                entity.Property(e => e.SuperficieAppartement).HasColumnName("Superficie_appartement");

                entity.HasOne(d => d.IdBlocNavigation)
                    .WithMany(p => p.Appartements)
                    .HasForeignKey(d => d.IdBloc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appartement_Bloc");

                entity.HasOne(d => d.IdLocataireNavigation)
                    .WithMany(p => p.Appartements)
                    .HasForeignKey(d => d.IdLocataire)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appartement_Locataire");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Bloc>(entity =>
            {
                entity.HasKey(e => e.IdBloc);

                entity.ToTable("Bloc");

                entity.Property(e => e.AdresseBloc)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("Adresse_bloc");

                entity.Property(e => e.AnneeConstruction)
                    .HasColumnType("date")
                    .HasColumnName("Annee_construction");

                entity.Property(e => e.Contracteur)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ImageBloc)
                    .IsRequired()
                    .HasColumnName("Image_bloc");

                entity.Property(e => e.NbEtages).HasColumnName("Nb_etages");
            });

            modelBuilder.Entity<Equipement>(entity =>
            {
                entity.HasKey(e => e.IdEquipement);

                entity.ToTable("Equipement");

                entity.Property(e => e.CategorieEquipement)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Categorie_equipement");

                entity.Property(e => e.DateAchatEquip)
                    .HasColumnType("date")
                    .HasColumnName("Date_achat_equip");

                entity.Property(e => e.DateFinGarantieEquip)
                    .HasColumnType("date")
                    .HasColumnName("date_fin_garantie_equip");

                entity.Property(e => e.EtatEquipement)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Etat_equipement");

                entity.Property(e => e.FournisseurEquipement)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Fournisseur_equipement");

                entity.Property(e => e.ImageEquipement)
                    .IsRequired()
                    .HasColumnName("Image_equipement");

                entity.Property(e => e.MarqueEquipement)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Marque_equipement");

                entity.Property(e => e.NomEquipement)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Nom_equipement");

                entity.HasOne(d => d.IdPieceNavigation)
                    .WithMany(p => p.Equipements)
                    .HasForeignKey(d => d.IdPiece)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Equipement_Piece");
            });

            modelBuilder.Entity<Locataire>(entity =>
            {
                entity.HasKey(e => e.IdLocataire);

                entity.ToTable("Locataire");

                entity.Property(e => e.DateDebutBail)
                    .HasColumnType("date")
                    .HasColumnName("Date_debut_bail");

                entity.Property(e => e.EmailLocataire)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Email_locataire");

                entity.Property(e => e.EtatCompteLocataire)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Etat_compte_locataire");

                entity.Property(e => e.NomLocataire)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Nom_locataire");

                entity.Property(e => e.PrenomLocataire)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Prenom_locataire");
            });

            modelBuilder.Entity<Membre>(entity =>
            {
                entity.HasKey(e => e.IdMembre);

                entity.ToTable("Membre");

                entity.Property(e => e.EmailMembre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Email_membre");

                entity.Property(e => e.MotPasseMembre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("mot_passe_membre");

                entity.Property(e => e.NomMembre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Nom_membre");

                entity.Property(e => e.PrenomMembre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Prenom_membre");

                entity.Property(e => e.TelMembre).HasColumnName("Tel_membre");
            });

            modelBuilder.Entity<Partenaire>(entity =>
            {
                entity.HasKey(e => e.IdPartenaire);

                entity.ToTable("Partenaire");

                entity.Property(e => e.EmailPartenaire)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Email_partenaire");

                entity.Property(e => e.FonctionPartenaire)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Fonction_partenaire");

                entity.Property(e => e.NomPartenaire)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Nom_partenaire");

                entity.Property(e => e.PrenomPartenaire)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Prenom_partenaire");

                entity.Property(e => e.TelPartenaire).HasColumnName("tel_partenaire");
            });

            modelBuilder.Entity<Piece>(entity =>
            {
                entity.HasKey(e => e.IdPiece);

                entity.ToTable("Piece");

                entity.Property(e => e.CouleurPiece)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Couleur_piece");

                entity.Property(e => e.DateRenovation)
                    .HasColumnType("date")
                    .HasColumnName("Date_renovation");

                entity.Property(e => e.ImagePiece)
                    .IsRequired()
                    .HasColumnName("Image_piece");

                entity.Property(e => e.SuperficiePiece).HasColumnName("Superficie_piece");

                entity.HasOne(d => d.IdAppartementNavigation)
                    .WithMany(p => p.Pieces)
                    .HasForeignKey(d => d.IdAppartement)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Piece_Appartement");
            });

            modelBuilder.Entity<PlaceParking>(entity =>
            {
                entity.HasKey(e => e.IdPlace);

                entity.ToTable("Place_parking");

                entity.Property(e => e.DispoPark)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Dispo_park");

                entity.HasOne(d => d.IdBlocNavigation)
                    .WithMany(p => p.PlaceParkings)
                    .HasForeignKey(d => d.IdBloc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Place_parking_Bloc");

                entity.HasOne(d => d.IdLocataireNavigation)
                    .WithMany(p => p.PlaceParkings)
                    .HasForeignKey(d => d.IdLocataire)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Place_parking_Locataire");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
