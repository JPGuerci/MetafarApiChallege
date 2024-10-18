using MetafarApiChallege.Infrastructure.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace MetafarApiChallege.Infrastructure.Repositories;

public partial class ChallengeContext : DbContext
{
    public ChallengeContext()
    {
    }

    public ChallengeContext(DbContextOptions<ChallengeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Operation> Operations { get; set; }

    public virtual DbSet<OperationType> OperationTypes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC077681201F");

            entity.ToTable("Account");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CurrentBalance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Card__3214EC07AD61E38E");

            entity.ToTable("Card");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Account).WithMany(p => p.Cards)
                .HasForeignKey(d => d.IdAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Card_Account");
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Operatio__3214EC07B522224B");

            entity.ToTable("Operation");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.FinalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.InitialAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Account).WithMany(p => p.Operations)
                .HasForeignKey(d => d.IdAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Operation_Account");

            entity.HasOne(d => d.ExecutingCard).WithMany(p => p.Operations)
                .HasForeignKey(d => d.IdExecutingCard)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Operation_Card");

            entity.HasOne(d => d.OperationType).WithMany(p => p.Operations)
                .HasForeignKey(d => d.IdOperationType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Operation_OperationType");
        });

        modelBuilder.Entity<OperationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Operatio__3214EC075BF7AA53");

            entity.ToTable("OperationType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
