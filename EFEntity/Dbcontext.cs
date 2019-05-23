namespace EFEntity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Dbcontext : DbContext
    {
        public Dbcontext()
            : base("name=connStr")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<AdminUser> AdminUser { get; set; }
        public virtual DbSet<Banner> Banner { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Guide> Guide { get; set; }
        public virtual DbSet<Line> Line { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderLine> OrderLine { get; set; }
        public virtual DbSet<OrderUser> OrderUser { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Raiders> Raiders { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<T_Line> T_Line { get; set; }
        public virtual DbSet<Travels> Travels { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<LinesInfo> LinesInfo { get; set; }
        public virtual DbSet<GuidLines> GuidLines { get; set; }
        public virtual DbSet<GuideUser> GuideUser { get; set; }
        public virtual DbSet<school> school { get; set; }
        public virtual DbSet<UserTravels> UserTravels { get; set; }
        public virtual DbSet<AdminUserRoles> AdminUserRoles { get; set; }
        //public virtual DbSet<RolePermissions1> RolePermissions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminUser>()
                .Property(e => e.PhoneNum)
                .IsUnicode(false);

            modelBuilder.Entity<AdminUser>()
                .Property(e => e.Name).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<AdminUser>()
                .Property(e => e.PasswordHash)
                .IsUnicode(false);

            modelBuilder.Entity<AdminUser>()
                .Property(e => e.PasswordSalt)
                .IsUnicode(false);

            modelBuilder.Entity<AdminUser>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.bannerurl)
                .IsUnicode(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.bannertitle)
                .IsUnicode(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.bannerlink)
                .IsUnicode(false);

            modelBuilder.Entity<Comment>()
                .Property(e => e.Context)
                .IsUnicode(false);

            modelBuilder.Entity<Guide>()
                .Property(e => e.school)
                .IsUnicode(false);

            modelBuilder.Entity<Guide>()
                .Property(e => e.picture)
                .IsUnicode(false);

            modelBuilder.Entity<Guide>()
                .Property(e => e.intro)
                .IsUnicode(false);

            modelBuilder.Entity<Line>()
                .Property(e => e.intro)
                .IsUnicode(false);

            modelBuilder.Entity<Line>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Raiders>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Raiders>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<Travels>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Travels>()
                .Property(e => e.content)
                .IsUnicode(false);


            modelBuilder.Entity<User>()
                .Property(e => e.PhoneNum)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
            .HasMany(r => r.Permissions).WithMany(p => p.Roles).Map(m => m.ToTable("RolePermissions")
                .MapLeftKey("RoleId").MapRightKey("PermissionId"));


            //modelBuilder.Entity<AdminUser>()
            //.HasMany(u => u.Roles).WithMany(r => r.AdminUsers).Map(m => m.ToTable("AdminUserRoles")
            //    .MapLeftKey("AdminUserId").MapRightKey("RoleId"));



            modelBuilder.Entity<Comment>().HasRequired(h => h.User).WithMany().HasForeignKey(h => h.Uid).WillCascadeOnDelete(false);
            //modelBuilder.Entity<Guide>().HasRequired(m => m.Users).WithOptional(n => n.Guide);
            //modelBuilder.Entity<Guide>().HasOptional(h => h.Users).WithMany().HasForeignKey(h => h.Id).WillCascadeOnDelete(false);
            modelBuilder.Entity<Comment>().HasRequired(h => h.Raiders).WithMany().HasForeignKey(h => h.Typeid).WillCascadeOnDelete(false);

            //       modelBuilder.Entity<Comment>().HasRequired(x => x.user)
            //.WithRequiredPrincipal(x => x.BindingRole).HasForeignKey(x => x.MenusManageID)

            //  modelBuilder.Entity<Raiders>()
            //.HasMany(r => r.Comment).WithMany(p => p.Raiders).Map(m => m.ToTable("UserComment")
            //    .MapLeftKey("Rid").MapRightKey("Cid"));
        }
    }
}
