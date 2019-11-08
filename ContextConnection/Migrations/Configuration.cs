namespace ContextConnection.Migrations
{
    using ContextDB;
    using DOMAIN.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ContextConnection.ContextDB.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ContextConnection.ContextDB.Context context)
        {
            if (!context.Units.Any())
            {
                CreateDataSampleUnit(context);
            }

            if (!context.Categories.Any())
            {
                CreateDataProductionParentSampleCategory(context);
                CreateDataNewsParentSampleCategory(context);
                CreateDataTypeProductionSampleCategory(context);
                CreateDataTypeNewsSampleCategory(context);
            }

            if (!context.Designs.Any())
            {
                CreateDataSampleDesign(context);
            }

            if (!context.Meterials.Any())
            {
                CreateDataSampleMeterial(context);
            }

            if (!context.Sizes.Any())
            {
                CreateDataSampleSize(context);
            }

            if (!context.Users.Any())
            {
                CreateUserDataSample(context);
            }
        }

        private void CreateDataSampleUnit(ContextConnection.ContextDB.Context context)
        {
            var lst = new List<Unit>();

            for (int i = 1; i <= 100; i++)
            {
                var model = new Unit();
                model.Name = "Đơn vị " + i;
                model.CreateDate = DateTime.Now;
                if ((i % 2) > 0)
                {
                    model.Status = true;
                }
                else
                {
                    model.Status = false;
                }
                lst.Add(model);
            }

            context.Units.AddRange(lst);
            context.SaveChanges();
        }

        private void CreateDataProductionParentSampleCategory(ContextConnection.ContextDB.Context context)
        {
            var lst = new List<Category>();
            for (int i = 1; i <= 10; i++)
            {
                var model = new Category();
                model.Name = "Danh mục sản phẩm cha " + i;
                model.CreateDate = DateTime.Now;
                model.CreateBy = "hadacduong";
                model.Type = 1;
                model.Status = true;
                model.ParentId = null;
                lst.Add(model);
            }
            context.Categories.AddRange(lst);
            context.SaveChanges();
        }

        private void CreateDataNewsParentSampleCategory(ContextConnection.ContextDB.Context context)
        {
            var lst = new List<Category>();
            for (int i = 1; i <= 10; i++)
            {
                var model = new Category();
                model.Name = "Danh mục tin tức cha " + i;
                model.CreateDate = DateTime.Now;
                model.CreateBy = "hadacduong";
                model.Type = 2;
                model.Status = true;
                model.ParentId = null;
                lst.Add(model);
            }
            context.Categories.AddRange(lst);
            context.SaveChanges();
        }

        private void CreateDataTypeProductionSampleCategory(ContextConnection.ContextDB.Context context)
        {
            var lst = new List<Category>();
            var lstParent = context.Categories.Where(n => n.Type == 1 && n.ParentId == null).ToList();
            int j = 0;
            for (int i = 1; i <= 100; i++)
            {
                var model = new Category();
                model.Name = "Danh mục sản phẩm " + i;
                model.CreateDate = DateTime.Now;
                model.CreateBy = "hadacduong";
                model.Type = 1;
                model.Status = true;
                model.ParentId = lstParent[j].Id;
                lst.Add(model);
                if (i % 10 == 0)
                {
                    j++;
                }
            }
            context.Categories.AddRange(lst);
            context.SaveChanges();
        }

        private void CreateDataTypeNewsSampleCategory(ContextConnection.ContextDB.Context context)
        {
            var lst = new List<Category>();
            var lstParent = context.Categories.Where(n => n.Type == 2 && n.ParentId == null).ToList();
            int j = 0;
            for (int i = 1; i <= 100; i++)
            {
                var model = new Category();
                model.Name = "Danh mục tin tức " + i;
                model.CreateDate = DateTime.Now;
                model.CreateBy = "hadacduong";
                model.Type = 1;
                model.Status = true;
                model.ParentId = lstParent[j].Id;
                lst.Add(model);
                if (i % 10 == 0)
                {
                    j++;
                }
                lst.Add(model);
            }
            context.Categories.AddRange(lst);
            context.SaveChanges();
        }

        private void CreateDataSampleDesign(ContextConnection.ContextDB.Context context)
        {
            var lst = new List<Design>();

            var firstParent = context.Categories.Where(n => n.ParentId == null).FirstOrDefault();

            for (int i = 1; i <= 100; i++)
            {
                var model = new Design();
                model.Name = "Kiểu dáng " + i;
                model.ParentCategoryId = firstParent.Id;
                model.CreateDate = DateTime.Now;
                if ((i % 2) > 0)
                {
                    model.Status = true;
                }
                else
                {
                    model.Status = false;
                }
                lst.Add(model);
            }

            context.Designs.AddRange(lst);
            context.SaveChanges();
        }

        private void CreateDataSampleMeterial(ContextConnection.ContextDB.Context context)
        {
            var lst = new List<Meterial>();

            for (int i = 1; i <= 100; i++)
            {
                var model = new Meterial();
                model.Name = "Chất liệu " + i;
                model.CreateDate = DateTime.Now;
                if ((i % 2) > 0)
                {
                    model.Status = true;
                }
                else
                {
                    model.Status = false;
                }
                lst.Add(model);
            }

            context.Meterials.AddRange(lst);
            context.SaveChanges();
        }

        private void CreateDataSampleSize(ContextConnection.ContextDB.Context context)
        {
            var lst = new List<Size>();

            double sampleWidth = 1;
            double sampleHeight = 1.5;
            for (int i = 1; i <= 20; i++)
            {
                var model = new Size();
                model.Width = sampleWidth;
                model.Height = sampleHeight;
                model.Unit = "Mét";
                model.Status = true;
                model.CreateBy = "hadacduong";
                model.CreateDate = DateTime.Now;
                sampleWidth = sampleWidth + 0.1;
                sampleWidth = sampleHeight + 0.2;
                lst.Add(model);
            }

            context.Sizes.AddRange(lst);
            context.SaveChanges();
        }

        private void CreateUserDataSample(ContextConnection.ContextDB.Context context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new Context()));

            var user = new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin@123.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Hà Đắc Dương",
                Status = true
            };
            manager.Create(user, "123456");
        }
    }
}