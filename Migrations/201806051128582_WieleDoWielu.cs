namespace Egzaminy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WieleDoWielu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserRoks",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Rok_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Rok_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roks", t => t.Rok_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Rok_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserRoks", "Rok_Id", "dbo.Roks");
            DropForeignKey("dbo.ApplicationUserRoks", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserRoks", new[] { "Rok_Id" });
            DropIndex("dbo.ApplicationUserRoks", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserRoks");
        }
    }
}
