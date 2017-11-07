namespace Elmonte.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class repairClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CondenaDelitos", "CondenaId", "dbo.Condenas");
            DropIndex("dbo.CondenaDelitos", new[] { "CondenaId" });
            RenameColumn(table: "dbo.Condenas", name: "CondenaId", newName: "CondenaDelito_Id");
            AddColumn("dbo.CondenaDelitos", "Condena_Id", c => c.Int());
            AddColumn("dbo.CondenaDelitos", "Condenas_Id", c => c.Int());
            CreateIndex("dbo.CondenaDelitos", "Condena_Id");
            CreateIndex("dbo.CondenaDelitos", "Condenas_Id");
            CreateIndex("dbo.Condenas", "CondenaDelito_Id");
            AddForeignKey("dbo.CondenaDelitos", "Condena_Id", "dbo.Condenas", "Id");
            AddForeignKey("dbo.CondenaDelitos", "Condenas_Id", "dbo.Condenas", "Id");
            AddForeignKey("dbo.Condenas", "CondenaDelito_Id", "dbo.CondenaDelitos", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Condenas", "CondenaDelito_Id", "dbo.CondenaDelitos");
            DropForeignKey("dbo.CondenaDelitos", "Condenas_Id", "dbo.Condenas");
            DropForeignKey("dbo.CondenaDelitos", "Condena_Id", "dbo.Condenas");
            DropIndex("dbo.Condenas", new[] { "CondenaDelito_Id" });
            DropIndex("dbo.CondenaDelitos", new[] { "Condenas_Id" });
            DropIndex("dbo.CondenaDelitos", new[] { "Condena_Id" });
            DropColumn("dbo.CondenaDelitos", "Condenas_Id");
            DropColumn("dbo.CondenaDelitos", "Condena_Id");
            RenameColumn(table: "dbo.Condenas", name: "CondenaDelito_Id", newName: "CondenaId");
            CreateIndex("dbo.CondenaDelitos", "CondenaId");
            AddForeignKey("dbo.CondenaDelitos", "CondenaId", "dbo.Condenas", "Id", cascadeDelete: true);
        }
    }
}
