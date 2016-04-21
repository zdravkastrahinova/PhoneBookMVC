using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace PhoneBookMVC.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<PhoneBookMVC.AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PhoneBookMVC.AppContext context)
        {
            context.Users.AddOrUpdate(
                u => u.Username,
                new Models.User { Username = "admin", Password = "adminpass", FirstName = "Admin", LastName = "Admin", Email = "admin@mail.com" }
                );
        }
    }
}
