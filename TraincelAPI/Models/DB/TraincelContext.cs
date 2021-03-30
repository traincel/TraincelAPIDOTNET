using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TraincelAPI.Models.DB
{
    public partial class TraincelContext : DbContext
    {
        public TraincelContext()
        {
        }

        public TraincelContext(DbContextOptions<TraincelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CartItems> CartItems { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Faculties> Faculties { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<LoginTable> LoginTable { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<PurchaseOptionType> PurchaseOptionType { get; set; }
        public virtual DbSet<PurchaseOptions> PurchaseOptions { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<UserCartMapping> UserCartMapping { get; set; }
        public virtual DbSet<UserTable> UserTable { get; set; }
        public virtual DbSet<WebinarPurchasedOptionsDetails> WebinarPurchasedOptionsDetails { get; set; }
        public virtual DbSet<WebinarTypes> WebinarTypes { get; set; }
        public virtual DbSet<Webinars> Webinars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:traincel.database.windows.net,1433;Initial Catalog=Traincel;Persist Security Info=False;User ID=traincel;Password=train@2020cel;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItems>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.LocalId })
                    .HasName("PK__CartItem__968DD99C8ADD3CE7");

                entity.Property(e => e.LocalId).ValueGeneratedOnAdd();

                entity.Property(e => e.PurchaseOptionId).HasColumnName("PurchaseOptionID");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CartItems__CartI__73BA3083");

                entity.HasOne(d => d.PurchaseOption)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.PurchaseOptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CartItems__Purch__74AE54BC");

                entity.HasOne(d => d.Webinar)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => new { d.WebinarId, d.WebinarLocalId })
                    .HasConstraintName("FK__CartItems__1332DBDC");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.LocalId })
                    .HasName("PK__Company__968DD99C1780D552");

                entity.Property(e => e.LocalId).ValueGeneratedOnAdd();

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Countries>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryCode).HasMaxLength(5);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Faculties>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.LocalId })
                    .HasName("PK__Facultie__968DD9BAA9C2238A");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LocalId)
                    .HasColumnName("LocalID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.BecameFacultyOn).HasColumnType("datetime");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Designation)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.EmailId)
                    .HasColumnName("EmailID")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.MobileNo).HasMaxLength(30);

                entity.Property(e => e.ProfileImageUrl)
                    .IsRequired()
                    .HasColumnName("ProfileImageURl");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Faculties)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Faculties__Count__778AC167");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.LocalId })
                    .HasName("PK__Invoice__968DD9BA355ABEFF");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LocalId)
                    .HasColumnName("LocalID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceUrl).HasColumnName("InvoiceURL");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => new { d.OrderId, d.OrderLocalId })
                    .HasConstraintName("FK__Invoice__7F2BE32F");
            });

            modelBuilder.Entity<LoginTable>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasColumnName("EmailID")
                    .HasMaxLength(500);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.PasswordChangeCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LoginTable)
                    .HasForeignKey(d => new { d.UserId, d.UserLocalId })
                    .HasConstraintName("FK__LoginTable__00200768");
            });

            modelBuilder.Entity<OrderItems>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.PurchaseOption)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.PurchaseOptionId)
                    .HasConstraintName("FK__OrderItem__Purch__02084FDA");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => new { d.OrderId, d.OrderLocalId })
                    .HasConstraintName("FK__OrderItems__01142BA1");

                entity.HasOne(d => d.Webinar)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => new { d.WebinarId, d.WebinarLocalId })
                    .HasConstraintName("FK__OrderItems__02FC7413");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.LocalId })
                    .HasName("PK__Orders__968DD9BA10EEB938");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LocalId)
                    .HasColumnName("LocalID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.PurchasedDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => new { d.UserId, d.UserLocalId })
                    .HasConstraintName("FK__Orders__04E4BC85");
            });

            modelBuilder.Entity<PurchaseOptionType>(entity =>
            {
                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<PurchaseOptions>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PurchasedOptionType)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.PurchaseOptions)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK__PurchaseO__TypeI__05D8E0BE");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.RoleName).HasMaxLength(32);
            });

            modelBuilder.Entity<UserCartMapping>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCartMapping)
                    .HasForeignKey(d => new { d.UserId, d.UserLocalId })
                    .HasConstraintName("FK__UserCartMapping__06CD04F7");
            });

            modelBuilder.Entity<UserTable>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.LocalId })
                    .HasName("PK__UserTabl__968DD9BA376EB97D");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LocalId)
                    .HasColumnName("LocalID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Designation).HasMaxLength(100);

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasColumnName("EmailID")
                    .HasMaxLength(500);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.MobileNo)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.UserTable)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserTable__Count__08B54D69");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.UserTable)
                    .HasForeignKey(d => new { d.CompanyId, d.CompanyLocalId })
                    .HasConstraintName("FK__UserTable__07C12930");
            });

            modelBuilder.Entity<WebinarPurchasedOptionsDetails>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.PurchaseOptionId).HasColumnName("PurchaseOptionID");

                entity.Property(e => e.WebinarId).HasColumnName("WebinarID");

                entity.HasOne(d => d.PurchaseOption)
                    .WithMany(p => p.WebinarPurchasedOptionsDetails)
                    .HasForeignKey(d => d.PurchaseOptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WebinarPu__Purch__09A971A2");

                entity.HasOne(d => d.Webinar)
                    .WithMany(p => p.WebinarPurchasedOptionsDetails)
                    .HasForeignKey(d => new { d.WebinarId, d.WebinarLocalId })
                    .HasConstraintName("FK__WebinarPurchased__0A9D95DB");
            });

            modelBuilder.Entity<WebinarTypes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Webinars>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.LocalId })
                    .HasName("PK__Webinars__968DD9BAB8A3404F");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LocalId)
                    .HasColumnName("LocalID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Duration).HasMaxLength(1000);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ThumbImageUrl).HasColumnName("ThumbImageURL");

                entity.Property(e => e.WebinarName).IsRequired();

                entity.Property(e => e.WebinarTypeId).HasColumnName("WebinarTypeID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Webinars)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Webinars__Catego__787EE5A0");

                entity.HasOne(d => d.WebinarType)
                    .WithMany(p => p.Webinars)
                    .HasForeignKey(d => d.WebinarTypeId)
                    .HasConstraintName("FK__Webinars__Webina__0D7A0286");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Webinars)
                    .HasForeignKey(d => new { d.FacultyId, d.FacultyLocalId })
                    .HasConstraintName("FK__Webinars__0C85DE4D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
