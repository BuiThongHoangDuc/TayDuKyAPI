using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TayDuKyAPI.Models
{
    public partial class PRM391Context : DbContext
    {
        public PRM391Context()
        {
        }

        public PRM391Context(DbContextOptions<PRM391Context> options)
            : base(options)
        {
        }

        public virtual DbSet<ActorRole> ActorRoles { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<EquipmentInScenario> EquipmentInScenarios { get; set; }
        public virtual DbSet<RoleScenario> RoleScenarios { get; set; }
        public virtual DbSet<Scenario> Scenarios { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=PRM391_Final");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<ActorRole>(entity =>
            {
                entity.ToTable("ActorRole");

                entity.Property(e => e.ActorRoleId).HasColumnName("ActorRoleID");

                entity.Property(e => e.DateUpdate).HasColumnType("datetime");

                entity.Property(e => e.RoleScenarioId).HasColumnName("RoleScenarioID");

                entity.HasOne(d => d.ActorInScenarioNavigation)
                    .WithMany(p => p.ActorRoles)
                    .HasForeignKey(d => d.ActorInScenario)
                    .HasConstraintName("FK_ActorRole_User");

                entity.HasOne(d => d.RoleScenario)
                    .WithMany(p => p.ActorRoles)
                    .HasForeignKey(d => d.RoleScenarioId)
                    .HasConstraintName("FK_ActorRole_RoleScenarios");

                entity.HasOne(d => d.Scenario)
                    .WithMany(p => p.ActorRoles)
                    .HasForeignKey(d => d.ScenarioId)
                    .HasConstraintName("FK_ActorRole_Scenario");
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.ToTable("Equipment");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.EquipmentDes).HasMaxLength(50);

                entity.Property(e => e.EquipmentImage).HasMaxLength(100);

                entity.Property(e => e.EquipmentName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EquipmentInScenario>(entity =>
            {
                entity.HasKey(e => e.EquipInScenario);

                entity.ToTable("EquipmentInScenario");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.ScenarioId).HasColumnName("ScenarioID");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.EquipmentInScenarios)
                    .HasForeignKey(d => d.EquipmentId)
                    .HasConstraintName("FK_EquipmentInScenario_Equipment");

                entity.HasOne(d => d.Scenario)
                    .WithMany(p => p.EquipmentInScenarios)
                    .HasForeignKey(d => d.ScenarioId)
                    .HasConstraintName("FK_EquipmentInScenario_Scenario");
            });

            modelBuilder.Entity<RoleScenario>(entity =>
            {
                entity.ToTable("RoleScenario");

                entity.Property(e => e.RoleScenarioId).HasColumnName("RoleScenarioID");

                entity.Property(e => e.RoleScenarioName).HasMaxLength(50);
            });

            modelBuilder.Entity<Scenario>(entity =>
            {
                entity.ToTable("Scenario");

                entity.Property(e => e.ScenarioId).HasColumnName("ScenarioID");

                entity.Property(e => e.ScenarioDes)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ScenarioLocation)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ScenarioName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ScenarioTimeFrom).HasColumnType("datetime");

                entity.Property(e => e.ScenarioTimeTo).HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserAdress).HasMaxLength(100);

                entity.Property(e => e.UserDescription).HasMaxLength(255);

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserImage).HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserPhoneNum)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });
        }
    }
}
