using Microsoft.EntityFrameworkCore;

namespace CondominiumWebApp.Models;

public partial class CondominiumContext : DbContext
{
    public CondominiumContext()
    {
    }

    public CondominiumContext(DbContextOptions<CondominiumContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Block> Blocks { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<Street> Streets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3307;database=condominium;uid=root;password=myPass", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Block>(entity =>
        {
            entity.HasKey(e => e.BlockId).HasName("PRIMARY");

            entity.ToTable("blocks");

            entity.Property(e => e.BlockId).HasColumnName("block_id");
            entity.Property(e => e.BlockName).HasColumnName("block_name");
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.OwnerId).HasName("PRIMARY");

            entity.ToTable("owners");

            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.OwnerName)
                .HasMaxLength(45)
                .HasColumnName("owner_name");
            entity.Property(e => e.OwnerSurname)
                .HasMaxLength(45)
                .HasColumnName("owner_surname");
            entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.PropertyId).HasName("PRIMARY");

            entity.ToTable("properties");

            entity.HasIndex(e => e.BlockId, "fk_properties_block_id_idx");

            entity.HasIndex(e => e.OwnerId, "fk_properties_owner_id_idx");

            entity.HasIndex(e => e.StreetId, "fk_properties_street_id_idx");

            entity.Property(e => e.PropertyId).HasColumnName("property_id");
            entity.Property(e => e.BlockId).HasColumnName("block_id");
            entity.Property(e => e.PropertyNumber).HasColumnName("property_number");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.PropertyDate).HasColumnName("property_date");
            entity.Property(e => e.PropertyPasscode)
                .HasMaxLength(5)
                .HasColumnName("property_passcode");
            entity.Property(e => e.PropertyType)
                .HasMaxLength(45)
                .HasColumnName("property_type");
            entity.Property(e => e.StreetId).HasColumnName("street_id");

            entity.HasOne(d => d.Block).WithMany(p => p.Properties)
                .HasForeignKey(d => d.BlockId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_properties_block_id");

            entity.HasOne(d => d.Owner).WithMany(p => p.Properties)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("fk_properties_owner_id");

            entity.HasOne(d => d.Street).WithMany(p => p.Properties)
                .HasForeignKey(d => d.StreetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_properties_street_id");
        });

        modelBuilder.Entity<Street>(entity =>
        {
            entity.HasKey(e => e.StreetId).HasName("PRIMARY");

            entity.ToTable("streets");

            entity.Property(e => e.StreetId).HasColumnName("street_id");
            entity.Property(e => e.StreetNumber).HasColumnName("street_number");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .HasColumnName("user_email");
            entity.Property(e => e.UserName)
                .HasMaxLength(45)
                .HasColumnName("user_name");
            entity.Property(e => e.UserNickname)
                .HasMaxLength(45)
                .HasColumnName("user_nickname");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(100)
                .HasColumnName("user_password");
            entity.Property(e => e.UserSurname)
                .HasMaxLength(45)
                .HasColumnName("user_surname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    internal Task GetUser(string email)
    {
        throw new NotImplementedException();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
