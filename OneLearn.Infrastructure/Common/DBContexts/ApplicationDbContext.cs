using Microsoft.EntityFrameworkCore;
using OneLearn.Domain.Test;
using OneLearn.Domain.User;
using OneLearn.Domain.VoiceTranslation;

namespace OneLearn.Infrastructure.Common.DBContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Language>()
                .HasIndex(x => new { x.language, x.language_Code }).IsUnique();


            modelBuilder.Entity<Passage>();
            modelBuilder.Entity<Passage>()
                .HasOne<Language>()
                .WithMany()
                .HasForeignKey(key => key.langauge_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Test>().HasData(
                new Test { id = 1, a = "a", b = "b", c = "c", d = "d", e = "e", f = "f", g = "g", h = "h", i = "i", j = "j", k = "k", l = "l", m = "m", n = "n", created_by = "asd", created_on = new DateTime(2024, 12, 16) },
                new Test { id = 2, a = "a", b = "b", c = "c", d = "d", e = "e", f = "f", g = "g", h = "h", i = "i", j = "j", k = "k", l = "l", m = "m", n = "n", created_by = "asd", created_on = new DateTime(2024, 12, 16) },
                new Test { id = 3, a = "a", b = "b", c = "c", d = "d", e = "e", f = "f", g = "g", h = "h", i = "i", j = "j", k = "k", l = "l", m = "m", n = "n", created_by = "asd", created_on = new DateTime(2024, 12, 16) },
                new Test { id = 4, a = "a", b = "b", c = "c", d = "d", e = "e", f = "f", g = "g", h = "h", i = "i", j = "j", k = "k", l = "l", m = "m", n = "n", created_by = "asd", created_on = new DateTime(2024, 12, 16) },
                new Test { id = 5, a = "a", b = "b", c = "c", d = "d", e = "e", f = "f", g = "g", h = "h", i = "i", j = "j", k = "k", l = "l", m = "m", n = "n", created_by = "asd", created_on = new DateTime(2024, 12, 16) },
                new Test { id = 6, a = "a", b = "b", c = "c", d = "d", e = "e", f = "f", g = "g", h = "h", i = "i", j = "j", k = "k", l = "l", m = "m", n = "n", created_by = "asd", created_on = new DateTime(2024, 12, 16) },
                new Test { id = 7, a = "a", b = "b", c = "c", d = "d", e = "e", f = "f", g = "g", h = "h", i = "i", j = "j", k = "k", l = "l", m = "m", n = "n", created_by = "asd", created_on = new DateTime(2024, 12, 16) },
                new Test { id = 8, a = "a", b = "b", c = "c", d = "d", e = "e", f = "f", g = "g", h = "h", i = "i", j = "j", k = "k", l = "l", m = "m", n = "n", created_by = "asd", created_on = new DateTime(2024, 12, 16) },
                new Test { id = 9, a = "a", b = "b", c = "c", d = "d", e = "e", f = "f", g = "g", h = "h", i = "i", j = "j", k = "k", l = "l", m = "m", n = "n", created_by = "asd", created_on = new DateTime(2024, 12, 16) },
                new Test { id = 10, a = "a", b = "b", c = "c", d = "d", e = "e", f = "f", g = "g", h = "h", i = "i", j = "j", k = "k", l = "l", m = "m", n = "n", created_by = "asd", created_on = new DateTime(2024, 12, 16) }
             );

            modelBuilder.Entity<User>()
                .HasOne<UserType>()
                .WithMany()
                .HasForeignKey(u => u.UserTypeId);


            modelBuilder.Entity<UserRoleMapping>()
                .HasIndex(x => new { x.UserId, x.RoleId })
                .IsUnique();

            modelBuilder.Entity<UserRoleMapping>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId);


            modelBuilder.Entity<UserRoleMapping>()
                .HasOne<Role>()
                .WithMany()
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<RolePrivillege>()
                .HasOne<Role>()
                .WithMany()
                .HasForeignKey(rp => rp.RoleId);

            //modelBuilder.Entity<Role>().HasData(
            //    new Role { RoleName = "Admin", IsActive = true, IsDeleted = false, created_by = "Auto", created_on = new DateTime(2024, 12, 26) },
            //    new Role { RoleName = "User", IsActive = true, IsDeleted = false, created_by = "Auto", created_on = new DateTime(2024, 12, 26) }
            //);
        }

        public DbSet<Language> Language { get; set; }
        public DbSet<Passage> Passages { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoleMapping> UserRoles { get; set; }
        public DbSet<RolePrivillege> RolePrivileges { get; set; }

    }
}
