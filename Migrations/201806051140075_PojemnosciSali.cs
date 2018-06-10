namespace Egzaminy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PojemnosciSali : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "Pojemnosc", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "Pojemnosc");
        }
    }
}
