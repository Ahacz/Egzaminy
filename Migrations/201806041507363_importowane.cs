namespace Egzaminy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class importowane : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Imie", c => c.String());
            AddColumn("dbo.AspNetUsers", "Nazwisko", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Nazwisko");
            DropColumn("dbo.AspNetUsers", "Imie");
        }
    }
}
