namespace Elmonte.Migrations
{
    using Elmonte.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Elmonte.Models.CarcelDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Elmonte.Models.CarcelDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            List<Delito> delitos = new List<Delito>()
        {
            new Delito() { Nombre="Homicidio", CondenaMinima=5, CondenaMaxima=20},
            new Delito() { Nombre="Femicidio", CondenaMinima=5, CondenaMaxima=20},
            new Delito() { Nombre="Robo Con intimidación", CondenaMinima=1, CondenaMaxima=12 },
            new Delito() { Nombre="Robo Con intimidación", CondenaMinima=1, CondenaMaxima=12 },
            new Delito() { Nombre="Robo en lugar no habitado", CondenaMinima=1, CondenaMaxima=5},
            new Delito() { Nombre="Cohecho", CondenaMinima=5, CondenaMaxima=8},
        };
            delitos.ForEach(person => context.Delitos.AddOrUpdate(person));
            context.SaveChanges();

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
