namespace Egzaminy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Wieledowielu11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserEgzamins",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Egzamin_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Egzamin_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Egzamins", t => t.Egzamin_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Egzamin_Id);
            
            CreateTable(
                "dbo.ApplicationUserPrzedmioties",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Przedmioty_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Przedmioty_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Przedmioties", t => t.Przedmioty_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Przedmioty_Id);
            
            DropColumn("dbo.Egzamins", "id_wykladowcy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Egzamins", "id_wykladowcy", c => c.String());
            DropForeignKey("dbo.ApplicationUserPrzedmioties", "Przedmioty_Id", "dbo.Przedmioties");
            DropForeignKey("dbo.ApplicationUserPrzedmioties", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserEgzamins", "Egzamin_Id", "dbo.Egzamins");
            DropForeignKey("dbo.ApplicationUserEgzamins", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserPrzedmioties", new[] { "Przedmioty_Id" });
            DropIndex("dbo.ApplicationUserPrzedmioties", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserEgzamins", new[] { "Egzamin_Id" });
            DropIndex("dbo.ApplicationUserEgzamins", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserPrzedmioties");
            DropTable("dbo.ApplicationUserEgzamins");
        }
    }
}
