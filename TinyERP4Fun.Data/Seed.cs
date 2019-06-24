using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Interfaces;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.Models.Expenses;

namespace TinyERP4Fun.Models
{
    public static class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = Constants.defaultAdminName;
            string password = Constants.adminFirstPwd;

            //add Admin Role
            if (await roleManager.FindByNameAsync(Constants.adminRoleName) == null)
                await roleManager.CreateAsync(new IdentityRole(Constants.adminRoleName));

            //add Other Roles
            foreach (var roleName in Constants.rolesList)
                if (await roleManager.FindByNameAsync(roleName) == null)
                    await roleManager.CreateAsync(new IdentityRole(roleName));

            //Add Admin User
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                IdentityUser admin = new IdentityUser { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Constants.adminRoleName);
                }
            }
        }
    }
    public static class SeedData
    {
        private static async Task SeedUser(UserManager<IdentityUser> userManager, string userEmail)
        {
            if (await userManager.FindByNameAsync(userEmail) == null)
            {
                IdentityUser newuser = new IdentityUser { Email = userEmail, UserName = userEmail };
                await userManager.CreateAsync(newuser, Constants.defaultPwd);
            }
        }
        public static async Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            await SeedUser(userManager, "user1@gmail.com");
            await SeedUser(userManager, "user2@gmail.com");
            await SeedUser(userManager, "user3@gmail.com");
            await SeedUser(userManager, "user4@gmail.com");
        }
        private static async Task Seed<T>(IServiceProvider serviceProvider, T param) where T : class, IHaveName
        {
            using (var context = new DefaultContext(serviceProvider.GetRequiredService<DbContextOptions<DefaultContext>>()))
                if (context.Set<T>().Where(x => x.Name == param.Name).Count() == 0)
                {
                    context.Update(param);
                    await context.SaveChangesAsync();
                }
        }
        private static async Task SeedCompanies(IServiceProvider serviceProvider)
        {
            await Seed(serviceProvider, new Company { Name = "EPAM Brest", Address = "Masherova 6a", TIN = "12345678", OurCompany = true });
            await Seed(serviceProvider, new Company { Name = "EPAM Minsk", OurCompany = true });
            await Seed(serviceProvider, new Company { Name = "EPAM Amsterdam", OurCompany = true });
            await Seed(serviceProvider, new Company { Name = "Default Customer 1" });
            await Seed(serviceProvider, new Company { Name = "Default Customer 2" });
            await Seed(serviceProvider, new Company { Name = "Default Customer 3" });
            await Seed(serviceProvider, new Company { Name = "Default Supplier 1" });
            await Seed(serviceProvider, new Company { Name = "Default Supplier 2" });
            await Seed(serviceProvider, new Company { Name = "Default Supplier 3" });
        }

        private static async Task SeedBusinessDirections(IServiceProvider serviceProvider)
        {
            await Seed(serviceProvider, new BusinessDirection { Name = "IT Consulting" });
            await Seed(serviceProvider, new BusinessDirection { Name = "Software Development" });
            await Seed(serviceProvider, new BusinessDirection { Name = "Application Integration" });
            await Seed(serviceProvider, new BusinessDirection { Name = "Porting and Migrating Applications" });
            await Seed(serviceProvider, new BusinessDirection { Name = "Software Testing" });
            await Seed(serviceProvider, new BusinessDirection { Name = "Creation of Dedicated Development Centers" });
            await Seed(serviceProvider, new BusinessDirection { Name = "Digital Strategy Development" });

        }

        private static async Task SeedCostItems(IServiceProvider serviceProvider)
        {
            await Seed(serviceProvider, new CostItem { Name = "Material Costs" });
            await Seed(serviceProvider, new CostItem { Name = "Labor Costs" });
            await Seed(serviceProvider, new CostItem { Name = "Social Contributions" });
            await Seed(serviceProvider, new CostItem { Name = "Depreciation" });
            await Seed(serviceProvider, new CostItem { Name = "Rent" });
            await Seed(serviceProvider, new CostItem { Name = "Other Costs" });
        }

        private static async Task SeedDepartments(IServiceProvider serviceProvider)
        {
            long? companyId = null;
            using (var context = new DefaultContext(serviceProvider.GetRequiredService<DbContextOptions<DefaultContext>>()))
            {
                companyId = context.Company.Where(x => x.Name == "EPAM Brest").First()?.Id;
            }
            if (companyId != null)
            {
                await Seed(serviceProvider, new Department { Name = "RD Lab", CompanyId = (long)companyId });
                await Seed(serviceProvider, new Department { Name = "HR Department", CompanyId = (long)companyId });
            }

        }
        private static async Task SeedPositions(IServiceProvider serviceProvider)
        {
            await Seed(serviceProvider, new Position { Name = "Junior Software Developer" });
            await Seed(serviceProvider, new Position { Name = "Middle Software Developer" });
            await Seed(serviceProvider, new Position { Name = "Senior Software Developer" });
            await Seed(serviceProvider, new Position { Name = "Business Analyst" });
            await Seed(serviceProvider, new Position { Name = "Project Manager" });
            await Seed(serviceProvider, new Position { Name = "Human Resources Manager" });
        }
        private static async Task SeedPeople(IServiceProvider serviceProvider)
        {
            await Seed(serviceProvider, new Person { FirstName = "Yauheni", LastName = "Tarhonski" });
            await Seed(serviceProvider, new Person { FirstName = "Aliaksandr", LastName = "Orgish" });
            await Seed(serviceProvider, new Person { FirstName = "Alena", LastName = "Kazakevich" });
            await Seed(serviceProvider, new Person { FirstName = "Nadzeya", LastName = "Pus" });
            await Seed(serviceProvider, new Person { FirstName = "Yauheniya", LastName = "Lialko" });
            await Seed(serviceProvider, new Person { FirstName = "Tatsiana", LastName = "Artsisheuskaya" });
            await Seed(serviceProvider, new Person { FirstName = "Kate", LastName = "Pretkel" });
            await Seed(serviceProvider, new Person { FirstName = "Neeladri", LastName = "Roy" });
            await Seed(serviceProvider, new Person { FirstName = "NoName", LastName = "NoLastname" });
        }

        private static async Task SeedCurrencies(IServiceProvider serviceProvider)
        {
            await Seed(serviceProvider, new Currency
            {
                Code = "USD",
                Name = "US Dollar",
                Name2 = "US Dollars",
                Active = true,
                Base = true,
                Part001Name = "Cent",
                Part001Name2 = "Cents"
            });
            await Seed(serviceProvider, new Currency
            {
                Code = "EUR",
                Name = "Euro",
                Name2 = "Euro",
                Active = true,
                Part001Name = "Cent",
                Part001Name2 = "Cents"
            });
            await Seed(serviceProvider, new Currency
            {
                Code = "RUB",
                Name = "Russian ruble",
                Name2 = "Russian rubles",
                Active = true,
                Part001Name = "Kopek",
                Part001Name2 = "Kopeks"
            });
            await Seed(serviceProvider, new Currency
            {
                Code = "BYN",
                Name = "Belorussian ruble",
                Name2 = "Belorussian rubles",
                Active = true,
                Part001Name = "Kopek",
                Part001Name2 = "Kopeks"
            });
        }
        private static async Task SeedCountries(IServiceProvider serviceProvider)
        {
            await Seed(serviceProvider, new Country { Name = "Netherlands" });
            await Seed(serviceProvider, new Country { Name = "Poland" });
            await Seed(serviceProvider, new Country { Name = "Belarus" });
            await Seed(serviceProvider, new Country { Name = "USA" });
            await Seed(serviceProvider, new Country { Name = "Finland" });
        }
        private static async Task SeedStates(IServiceProvider serviceProvider)
        {
            await SeedCountries(serviceProvider);
            using (var context = new DefaultContext(serviceProvider.GetRequiredService<DbContextOptions<DefaultContext>>()))
            {
                var Netherlands = context.Country.Where(x => x.Name == "Netherlands").First()?.Id;
                var Poland = context.Country.Where(x => x.Name == "Poland").First()?.Id;
                var Belarus = context.Country.Where(x => x.Name == "Belarus").First()?.Id;
                var USA = context.Country.Where(x => x.Name == "USA").First()?.Id;
                var Finland = context.Country.Where(x => x.Name == "Finland").First()?.Id;

                if (Netherlands!=null)
                {
                    await Seed(serviceProvider, new State { Name = "Noord-Holland", CountryId = (long)Netherlands });
                    await Seed(serviceProvider, new State { Name = "Utrecht", CountryId = (long)Netherlands });
                    await Seed(serviceProvider, new State { Name = "Zuid-Holland", CountryId = (long)Netherlands });
                }
                if (Poland != null)
                {
                    await Seed(serviceProvider, new State { Name = "Mazowieckie", CountryId = (long)Poland });
                    await Seed(serviceProvider, new State { Name = "Łódzkie", CountryId = (long)Poland });
                    await Seed(serviceProvider, new State { Name = "Pomorskie", CountryId = (long)Poland });
                }
                if (Belarus != null)
                {
                    await Seed(serviceProvider, new State { Name = "Brest Region", CountryId = (long)Belarus });
                    await Seed(serviceProvider, new State { Name = "Minsk Region", CountryId = (long)Belarus });
                }
                if (USA != null)
                {
                    await Seed(serviceProvider, new State { Name = "Kentucky", CountryId = (long)USA });
                    await Seed(serviceProvider, new State { Name = "Oregon", CountryId = (long)USA });
                }
                if (Finland != null)
                    await Seed(serviceProvider, new State { Name = "North Ostrobothnia", CountryId = (long)Finland });
            }
        }
        public static async Task SeedCities(IServiceProvider serviceProvider)
        {
            await SeedStates(serviceProvider);
            using (var context = new DefaultContext(serviceProvider.GetRequiredService<DbContextOptions<DefaultContext>>()))
            {
                var NoordHolland = context.State.Where(x => x.Name == "Noord-Holland").First()?.Id;
                var Utrecht = context.State.Where(x => x.Name == "Utrecht").First()?.Id;
                var ZuidHolland = context.State.Where(x => x.Name == "Zuid-Holland").First()?.Id;

                var Mazowieckie = context.State.Where(x => x.Name == "Mazowieckie").First()?.Id;
                var Lodzkie = context.State.Where(x => x.Name == "Łódzkie").First()?.Id;
                var Pomorskie = context.State.Where(x => x.Name == "Pomorskie").First()?.Id;

                var BrestRegion = context.State.Where(x => x.Name == "Brest Region").First()?.Id;
                var MinskRegion = context.State.Where(x => x.Name == "Minsk Region").First()?.Id;

                var Kentucky = context.State.Where(x => x.Name == "Kentucky").First()?.Id;
                var Oregon = context.State.Where(x => x.Name == "Oregon").First()?.Id;

                var NorthOstrobothnia = context.State.Where(x => x.Name == "North Ostrobothnia").First()?.Id;

                if (NoordHolland != null)
                    await Seed(serviceProvider, new City { Name = "Amsterdam", StateId = (long)NoordHolland });
                if (Utrecht != null)
                    await Seed(serviceProvider, new City { Name = "Utrecht", StateId = (long)Utrecht });
                if (ZuidHolland != null)
                {
                    await Seed(serviceProvider, new City { Name = "Den Haag", StateId = (long)ZuidHolland });
                    await Seed(serviceProvider, new City { Name = "Rotterdam", StateId = (long)ZuidHolland });
                }

                if (Mazowieckie != null)
                    await Seed(serviceProvider, new City { Name = "Warshawa", StateId = (long)Mazowieckie });
                if (Lodzkie != null)
                    await Seed(serviceProvider, new City { Name = "Łódz", StateId = (long)Lodzkie });
                if (Pomorskie != null)
                    await Seed(serviceProvider, new City { Name = "Gdansk", StateId = (long)Pomorskie });

                if (BrestRegion != null)
                    await Seed(serviceProvider, new City { Name = "Brest", StateId = (long)BrestRegion });
                if (MinskRegion != null)
                    await Seed(serviceProvider, new City { Name = "Minsk", StateId = (long)MinskRegion });

                if (Kentucky != null)
                    await Seed(serviceProvider, new City { Name = "Owensboro", StateId = (long)Kentucky });
                if (Oregon != null)
                    await Seed(serviceProvider, new City { Name = "Portland", StateId = (long)Oregon });

                if (NorthOstrobothnia != null)
                {
                    await Seed(serviceProvider, new City { Name = "Oulu", StateId = (long)NorthOstrobothnia });
                    await Seed(serviceProvider, new City { Name = "Muhos", StateId = (long)NorthOstrobothnia });
                }

            }
        }

        public static async Task SeedAll(IServiceProvider serviceProvider)
        {
            await SeedData.SeedCities(serviceProvider);
            await SeedData.SeedPeople(serviceProvider);
            await SeedData.SeedCompanies(serviceProvider);
            await SeedData.SeedDepartments(serviceProvider);
            await SeedData.SeedCostItems(serviceProvider);
            await SeedData.SeedBusinessDirections(serviceProvider);
            await SeedData.SeedPositions(serviceProvider);
            await SeedData.SeedCurrencies(serviceProvider);
        }
    }
}
