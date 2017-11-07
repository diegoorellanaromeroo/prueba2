namespace Elmonte.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class coreccionBd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Presos", "Nombre", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Presos", "Nombre");
        }
    }
}
