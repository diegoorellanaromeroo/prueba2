namespace Elmonte.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class context : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CondenaDelitos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CondenaId = c.Int(nullable: false),
                        DelitoId = c.Int(nullable: false),
                        Condena = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Condenas", t => t.CondenaId, cascadeDelete: true)
                .ForeignKey("dbo.Delitos", t => t.DelitoId, cascadeDelete: true)
                .Index(t => t.CondenaId)
                .Index(t => t.DelitoId);
            
            CreateTable(
                "dbo.Condenas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaInicioCondena = c.DateTime(nullable: false),
                        FechaCondena = c.DateTime(nullable: false),
                        PresoId = c.Int(nullable: false),
                        JuezId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jueces", t => t.JuezId, cascadeDelete: true)
                .ForeignKey("dbo.Presos", t => t.PresoId, cascadeDelete: true)
                .Index(t => t.PresoId)
                .Index(t => t.JuezId);
            
            CreateTable(
                "dbo.Jueces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Rut = c.String(),
                        Sexo = c.Boolean(nullable: false),
                        Domicilio = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Presos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rut = c.String(),
                        Apellido = c.String(),
                        FechaNacimiento = c.DateTime(nullable: false),
                        Domicilio = c.String(),
                        Sexo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Delitos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        CondenaMinima = c.Int(nullable: false),
                        CondenaMaxima = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CondenaDelitos", "DelitoId", "dbo.Delitos");
            DropForeignKey("dbo.Condenas", "PresoId", "dbo.Presos");
            DropForeignKey("dbo.Condenas", "JuezId", "dbo.Jueces");
            DropForeignKey("dbo.CondenaDelitos", "CondenaId", "dbo.Condenas");
            DropIndex("dbo.Condenas", new[] { "JuezId" });
            DropIndex("dbo.Condenas", new[] { "PresoId" });
            DropIndex("dbo.CondenaDelitos", new[] { "DelitoId" });
            DropIndex("dbo.CondenaDelitos", new[] { "CondenaId" });
            DropTable("dbo.Delitos");
            DropTable("dbo.Presos");
            DropTable("dbo.Jueces");
            DropTable("dbo.Condenas");
            DropTable("dbo.CondenaDelitos");
        }
    }
}
