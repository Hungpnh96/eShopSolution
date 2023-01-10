using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
               new AppConfig() { Key = "HomeTitle", Value = "This is home page of eShopSolution" },
               new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of eShopSolution" },
               new AppConfig() { Key = "HomeDescription", Value = "This is description of eShopSolution" }
               );

            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = "vi-VN", Name = "Tiếng Việt", IsDefault = true },
                new Language() { Id = "en-US", Name = "English", IsDefault = false });

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 1,
                    Status = Status.Active,
                },
                 new Category()
                 {
                     Id = 2,
                     IsShowOnHome = true,
                     ParentId = null,
                     SortOrder = 2,
                     Status = Status.Active
                 });

            modelBuilder.Entity<CategoryTranslation>().HasData(
                  new CategoryTranslation() { Id = 1, CategoryId = 1, Name = "Sản phẩm 1", LanguageId = "vi-VN", SeoAlias = "San-pham-1", SeoDescription = "Sản phẩm 1", SeoTitle = "Sản phẩm 1" },
                  new CategoryTranslation() { Id = 2, CategoryId = 1, Name = "Product Name 1", LanguageId = "en-US", SeoAlias = "Product-name-1", SeoDescription = "The products name 1", SeoTitle = "The products name 1" },
                  new CategoryTranslation() { Id = 3, CategoryId = 2, Name = "Sản phẩm 2", LanguageId = "vi-VN", SeoAlias = "San-pham-2", SeoDescription = "Sản phẩm 2", SeoTitle = "Sản phẩm 2" },
                  new CategoryTranslation() { Id = 4, CategoryId = 2, Name = "Product Name 2", LanguageId = "en-US", SeoAlias = "Product-Name-2", SeoDescription = "The Product Name 2", SeoTitle = "The Product Name 2" }
                    );

            modelBuilder.Entity<Product>().HasData(
           new Product()
           {
               Id = 1,
               DateCreated = DateTime.Now,
               OriginalPrice = 100000,
               Price = 200000,
               Stock = 0,
               ViewCount = 0,
           });
            modelBuilder.Entity<ProductTranslation>().HasData(
                 new ProductTranslation()
                 {
                     Id = 1,
                     ProductId = 1,
                     Name = "Sản phẩm 1",
                     LanguageId = "vi-VN",
                     SeoAlias = "San-pham-1",
                     SeoDescription = "Sản phẩm 1",
                     SeoTitle = "Sản phẩm 1",
                     Details = "Sản phẩm 1",
                     Description = "Sản phẩm 1"
                 },
                    new ProductTranslation()
                    {
                        Id = 2,
                        ProductId = 1,
                        Name = "Product Name 1",
                        LanguageId = "en-US",
                        SeoAlias = "Product-name-1",
                        SeoDescription = "Product Name 1",
                        SeoTitle = "Product Name 1",
                        Details = "Product Name 1",
                        Description = "Product Name 1"
                    });
            modelBuilder.Entity<ProductInCategory>().HasData(
                new ProductInCategory() { ProductId = 1, CategoryId = 1 }
                );

            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "hungpnh96@gmail.com",
                NormalizedEmail = "hungpnh96@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Jupenhju@12"),
                SecurityStamp = string.Empty,
                FullName = "Hung",
                Dob = new DateTime(1995, 12, 17)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}
