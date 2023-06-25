using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Learn_core_mvc.Repository.EFDBFirstRepo.Models;

namespace Learn_core_mvc.Repository.EFDBFirstRepo
{
    public partial class MyDBDbContext : DbContext
    {
        public MyDBDbContext()
        {
        }

        public MyDBDbContext(DbContextOptions<MyDBDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<TblCheckPayStatus> TblCheckPayStatus { get; set; }
        public virtual DbSet<TblCompany> TblCompany { get; set; }
        public virtual DbSet<TblCustomer> TblCustomer { get; set; }
        public virtual DbSet<TblDatatableEmp> TblDatatableEmp { get; set; }
        public virtual DbSet<TblEmployee> TblEmployee { get; set; }
        public virtual DbSet<TblExceptionLogger> TblExceptionLogger { get; set; }
        public virtual DbSet<TblProduct> TblProduct { get; set; }
        public virtual DbSet<TblStudent> TblStudent { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }
        public virtual DbSet<TblUserRole> TblUserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-F92A9QM\\SQLEXPRESS;Database=MyDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<TblCheckPayStatus>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.ToTable("tbl_check_pay_status");

                entity.Property(e => e.EmpId).HasColumnName("emp_id");

                entity.Property(e => e.EmpName)
                    .HasColumnName("emp_name")
                    .HasMaxLength(50);

                entity.Property(e => e.EmpSalary)
                    .HasColumnName("emp_salary")
                    .HasColumnType("money");

                entity.Property(e => e.PaidStatus)
                    .HasColumnName("paid_status")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblCompany>(entity =>
            {
                entity.HasKey(e => e.CmpId);

                entity.ToTable("tbl_company");

                entity.Property(e => e.CmpId).HasColumnName("cmp_id");

                entity.Property(e => e.CmpAddress)
                    .HasColumnName("cmp_address")
                    .HasMaxLength(50);

                entity.Property(e => e.CmpName)
                    .HasColumnName("cmp_name")
                    .HasMaxLength(50);

                entity.Property(e => e.CmpRating).HasColumnName("cmp_rating");
            });

            modelBuilder.Entity<TblCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.ToTable("tbl_customer");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(50);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(50);

                entity.Property(e => e.CompanyName)
                    .HasColumnName("company_name")
                    .HasMaxLength(50);

                entity.Property(e => e.ContactName)
                    .HasColumnName("contact_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(50);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(50);

                entity.Property(e => e.Zip)
                    .HasColumnName("zip")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblDatatableEmp>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.ToTable("tbl_datatable_emp");

                entity.Property(e => e.EmpId).HasColumnName("emp_id");

                entity.Property(e => e.EmpCity)
                    .HasColumnName("emp_city")
                    .HasMaxLength(50);

                entity.Property(e => e.EmpEmail)
                    .HasColumnName("emp_email")
                    .HasMaxLength(50);

                entity.Property(e => e.EmpGender)
                    .HasColumnName("emp_gender")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.EmpName)
                    .HasColumnName("emp_name")
                    .HasMaxLength(50);

                entity.Property(e => e.EmpSalary)
                    .HasColumnName("emp_salary")
                    .HasColumnType("money");
            });

            modelBuilder.Entity<TblEmployee>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.ToTable("tbl_employee");

                entity.Property(e => e.EmpId).HasColumnName("emp_id");

                entity.Property(e => e.EmpAddress)
                    .HasColumnName("emp_address")
                    .HasMaxLength(50);

                entity.Property(e => e.EmpCity)
                    .HasColumnName("emp_city")
                    .HasMaxLength(50);

                entity.Property(e => e.EmpCountry)
                    .HasColumnName("emp_country")
                    .HasMaxLength(50);

                entity.Property(e => e.EmpEmail)
                    .HasColumnName("emp_email")
                    .HasMaxLength(50);

                entity.Property(e => e.EmpName)
                    .HasColumnName("emp_name")
                    .HasMaxLength(50);

                entity.Property(e => e.EmpPhone)
                    .HasColumnName("emp_phone")
                    .HasMaxLength(50);

                entity.Property(e => e.EmpState)
                    .HasColumnName("emp_state")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblExceptionLogger>(entity =>
            {
                entity.ToTable("tbl_Exception_logger");

                entity.Property(e => e.ExceptionMessage).HasColumnName("Exception_message");

                entity.Property(e => e.ExceptionStackTrace).HasColumnName("Exception_stack_trace");

                entity.Property(e => e.LogTime)
                    .HasColumnName("Log_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("tbl_product");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProductDesc).HasColumnName("product_desc");

                entity.Property(e => e.ProductName)
                    .HasColumnName("product_name")
                    .HasMaxLength(50);

                entity.Property(e => e.ProductPrice)
                    .HasColumnName("product_price")
                    .HasColumnType("money");

                entity.Property(e => e.ProductRating).HasColumnName("product_rating");
            });

            modelBuilder.Entity<TblStudent>(entity =>
            {
                entity.HasKey(e => e.StdId);

                entity.ToTable("tbl_student");

                entity.Property(e => e.StdClass).HasMaxLength(50);

                entity.Property(e => e.StdName).HasMaxLength(50);

                entity.Property(e => e.StdRollNumber).HasMaxLength(50);

                entity.Property(e => e.StdSubject).HasMaxLength(50);
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("tbl_user");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserEmail)
                    .HasColumnName("user_email")
                    .HasMaxLength(50);

                entity.Property(e => e.UserPassword)
                    .HasColumnName("user_password")
                    .HasMaxLength(50);

                entity.Property(e => e.UserRoleId).HasColumnName("user_role_id");

                entity.Property(e => e.UserSalt)
                    .HasColumnName("user_salt")
                    .HasMaxLength(50);

                entity.Property(e => e.UserToken).HasColumnName("user_token");
            });

            modelBuilder.Entity<TblUserRole>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tbl_user_role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasColumnName("role_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
