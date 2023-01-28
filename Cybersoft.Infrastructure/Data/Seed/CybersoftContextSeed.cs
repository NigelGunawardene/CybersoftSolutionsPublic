using Cybersoft.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Cybersoft.Infrastructure.Data.Seed
{
    public class CybersoftContextSeed
    {

        // Enter preliminary values for tables here
        public static async Task SeedAsync(CyberSoftContext cybersoftContext)
        {
            if (!cybersoftContext.Users.Any())
            {
                // users
            }

            if (!cybersoftContext.Products.Any())
            {
                cybersoftContext.Products.Add(new Products
                {
                    Name = "Windows 10 Pro + Office 2019",
                    Description = "BUNDLE: Windows 10 Pro & Office 2019 Pro Plus",
                    Price = 2690,
                    ImageUrl = "assets/windows10office2019.jpg"
                });
                cybersoftContext.Products.Add(new Products
                {
                    Name = "Windows 10 Pro + Office 2021",
                    Description = "BUNDLE: Windows 10 Pro + Office 2021 Pro Plus",
                    Price = 3290,
                    ImageUrl = "assets/windows10office2021.jpg"
                });
                cybersoftContext.Products.Add(new Products
                {
                    Name = "Windows 11 Pro + Office 2021",
                    Description = "BUNDLE: Windows 10 Pro + Office 2021 Pro Plus",
                    Price = 3490,
                    ImageUrl = "assets/windows11office2021.jpg"
                });
                cybersoftContext.Products.Add(new Products
                {
                    Name = "Windows 10 Pro",
                    Description = "Lifetime License for Windows 10 Pro",
                    Price = 1690,
                    ImageUrl = "assets/windows10Pro.jpg"
                });
                cybersoftContext.Products.Add(new Products
                {
                    Name = "Windows 10 Home",
                    Description = "Lifetime License for Windows 10 Home",
                    Price = 1690,
                    ImageUrl = "assets/windows10home.jpg"
                });
                cybersoftContext.Products.Add(new Products
                {
                    Name = "Windows 10 Enterprise",
                    Description = "Lifetime License for Windows 10 Enterprise",
                    Price = 1690,
                    ImageUrl = "assets/windows10enterprise.jpg"
                });
                cybersoftContext.Products.Add(new Products
                {
                    Name = "Office 2021 Pro Plus",
                    Description = "Lifetime License for Office 2021 Pro Plus",
                    Price = 2490,
                    ImageUrl = "assets/office2021professionalplus.jpg"
                });
                cybersoftContext.Products.Add(new Products
                {
                    Name = "Office 2019 Pro Plus",
                    Description = "Lifetime License for Office 2019 Pro Plus",
                    Price = 1890,
                    ImageUrl = "assets/office2019professionalplus.jpg"
                });
                cybersoftContext.Products.Add(new Products
                {
                    Name = "Windows 11 Pro",
                    Description = "Lifetime License for Windows 11 Pro",
                    Price = 1890,
                    ImageUrl = "assets/windows11pro.jpg"
                });
                cybersoftContext.Products.Add(new Products
                {
                    Name = "Windows 11 Home",
                    Description = "Lifetime License for Windows 11 Home",
                    Price = 1890,
                    ImageUrl = "assets/windows11home.jpg"
                });
                cybersoftContext.Products.Add(new Products
                {
                    Name = "Project 2021 Professional",
                    Description = "Lifetime License for Project 2021 Professional",
                    Price = 2490,
                    ImageUrl = "assets/project2021.jpg"
                });
                cybersoftContext.Products.Add(new Products
                {
                    Name = "Project 2019 Professional",
                    Description = "Lifetime License for Project 2019 Professional",
                    Price = 1990,
                    ImageUrl = "assets/project2019.jpg"
                });
                cybersoftContext.Products.Add(new Products
                {
                    Name = "Visio 2021 Professional",
                    Description = "Lifetime License for Visio 2021 Professional",
                    Price = 2490,
                    ImageUrl = "assets/visio2021.jpg"
                });
                cybersoftContext.Products.Add(new Products
                {
                    Name = "Visio 2019 Professional",
                    Description = "Lifetime License for Visio 2019 Professional",
                    Price = 1990,
                    ImageUrl = "assets/visio2019.jpg"
                });
                cybersoftContext.Products.Add(new Products
                {
                    Name = "Office 2021 Home & Business (Mac)",
                    Description = "Lifetime License for Office 2021 Home & Business for Mac",
                    Price = 8490,
                    ImageUrl = "assets/office2021homebusiness.jpg"
                });
                cybersoftContext.Products.Add(new Products
                {
                    Name = "Office 2019 Home & Business (Mac)",
                    Description = "Lifetime License for Office 2019 Home & Business for Mac",
                    Price = 7990,
                    ImageUrl = "assets/office2019homebusiness.jpg"
                });
                cybersoftContext.Products.Add(new Products
                {
                    Name = "Visual Studio Professional 2022",
                    Description = "Lifetime License for Visual Studio Professional 2022",
                    Price = 9990,
                    ImageUrl = "assets/visualstudioprofessional2022.jpg"
                });
                cybersoftContext.Products.Add(new Products
                {
                    Name = "Visual Studio Enterprise 2022",
                    Description = "Lifetime License for Visual Studio Enterprise 2022",
                    Price = 10490,
                    ImageUrl = "assets/visualstudioenterprise2022.jpg"
                });
                cybersoftContext.Products.Add(new Products
                {
                    Name = "Visual Studio Professional 2019",
                    Description = "Lifetime License for Visual Studio Professional 2019",
                    Price = 8490,
                    ImageUrl = "assets/visualstudioprofessional2019.jpg"
                });
                cybersoftContext.Products.Add(new Products
                {
                    Name = "Visual Studio Enterprise 2019",
                    Description = "Lifetime License for Visual Studio Enterprise 2019",
                    Price = 8990,
                    ImageUrl = "assets/visualstudioenterprise2019.jpg"
                });
                await cybersoftContext.SaveChangesAsync();
            }
        }

    }
}
