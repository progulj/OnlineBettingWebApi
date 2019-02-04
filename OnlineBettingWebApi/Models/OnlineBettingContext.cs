using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OnlineBettingWebApi.Models
{
    public partial class OnlineBettingContext : DbContext
    {
        public OnlineBettingContext()
        {
        }

        public OnlineBettingContext(DbContextOptions<OnlineBettingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Offer> Offer { get; set; }
        public virtual DbSet<SpecialOffer> SpecialOffer { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<Wallet> Wallet { get; set; }
        public virtual DbSet<WinOffer> WinOffer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           /** if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=Pero-PC\\SQLEXPRESS;Database=OnlineBetting;UID=sa;PWD=QWERTZ;");
            }**/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date)
                    .HasColumnName("DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("GAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdOffer).HasColumnName("ID_OFFER");

                entity.Property(e => e.IdTicket).HasColumnName("ID_TICKET");

                entity.Property(e => e.Odds)
                    .HasColumnName("ODDS")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.OddsType)
                    .HasColumnName("ODDS_TYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Special).HasColumnName("SPECIAL");

                entity.HasOne(d => d.IdOfferNavigation)
                    .WithMany(p => p.GameNavigation)
                    .HasForeignKey(d => d.IdOffer)
                    .HasConstraintName("FK_Game_Offer");

                entity.HasOne(d => d.IdTicketNavigation)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.IdTicket)
                    .HasConstraintName("FK_Game_Ticket");
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Game)
                    .HasColumnName("GAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OddsAway)
                    .HasColumnName("ODDS_AWAY")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.OddsDraw)
                    .HasColumnName("ODDS_DRAW")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.OddsDrawAway)
                    .HasColumnName("ODDS_DRAW_AWAY")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.OddsDrawHome)
                    .HasColumnName("ODDS_DRAW_HOME")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.OddsHome)
                    .HasColumnName("ODDS_HOME")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.OddsHomeAway)
                    .HasColumnName("ODDS_HOME_AWAY")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.Special).HasColumnName("SPECIAL");
            });

            modelBuilder.Entity<SpecialOffer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdOffer).HasColumnName("ID_OFFER");

                entity.Property(e => e.OddsAway)
                    .HasColumnName("ODDS_AWAY")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.OddsDraw)
                    .HasColumnName("ODDS_DRAW")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.OddsDrawAway)
                    .HasColumnName("ODDS_DRAW_AWAY")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.OddsDrawHome)
                    .HasColumnName("ODDS_DRAW_HOME")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.OddsHome)
                    .HasColumnName("ODDS_HOME")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.OddsHomeAway)
                    .HasColumnName("ODDS_HOME_AWAY")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdOfferNavigation)
                    .WithMany(p => p.SpecialOffer)
                    .HasForeignKey(d => d.IdOffer)
                    .HasConstraintName("FK_SpecialOffer_Offer");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Commission)
                    .HasColumnName("COMMISSION")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Date)
                    .HasColumnName("DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.EstimatedWin)
                    .HasColumnName("ESTIMATED_WIN")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.FullPayment)
                    .HasColumnName("FULL_PAYMENT")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.IdWallet).HasColumnName("ID_WALLET");

                entity.Property(e => e.TotalOdds)
                    .HasColumnName("TOTAL_ODDS")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdWalletNavigation)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.IdWallet)
                    .HasConstraintName("FK_Ticket_Wallet");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount)
                    .HasColumnName("AMOUNT")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Date)
                    .HasColumnName("DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdWallet).HasColumnName("ID_WALLET");

                entity.Property(e => e.Type)
                    .HasColumnName("TYPE")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdWalletNavigation)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.IdWallet)
                    .HasConstraintName("FK_Transaction_Wallet");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Account)
                    .HasColumnName("ACCOUNT")
                    .HasMaxLength(34)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnName("DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.WalletBalance)
                    .HasColumnName("WALLET_BALANCE")
                    .HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<WinOffer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdOffer).HasColumnName("ID_OFFER");

                entity.Property(e => e.Type)
                    .HasColumnName("TYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdOfferNavigation)
                    .WithMany(p => p.WinOffer)
                    .HasForeignKey(d => d.IdOffer)
                    .HasConstraintName("FK_WinOffer_Offer");
            });
        }
    }
}
