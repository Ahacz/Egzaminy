namespace Egzaminy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KontekstDodany : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Egzamins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        CzasRozpoczecia = c.Int(nullable: false),
                        CzasTrwania = c.Int(nullable: false),
                        Sala = c.Int(nullable: false),
                        Podgrupa = c.Int(nullable: false),
                        id_wykladowcy = c.String(),
                        Podgrupa1_Id = c.Int(),
                        Sale_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Podgrupas", t => t.Podgrupa1_Id)
                .ForeignKey("dbo.Sales", t => t.Sale_Id)
                .Index(t => t.Podgrupa1_Id)
                .Index(t => t.Sale_Id);
            
            CreateTable(
                "dbo.Podgrupas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                        Liczba = c.Int(nullable: false),
                        Idroku = c.Int(nullable: false),
                        Rok_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roks", t => t.Rok_Id)
                .Index(t => t.Rok_Id);
            
            CreateTable(
                "dbo.Roks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NazwaRoku = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Przedmioties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NazwaPrzedmiotu = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NazwaSali = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PrzedmiotyRoks",
                c => new
                    {
                        Przedmioty_Id = c.Int(nullable: false),
                        Rok_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Przedmioty_Id, t.Rok_Id })
                .ForeignKey("dbo.Przedmioties", t => t.Przedmioty_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roks", t => t.Rok_Id, cascadeDelete: true)
                .Index(t => t.Przedmioty_Id)
                .Index(t => t.Rok_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Egzamins", "Sale_Id", "dbo.Sales");
            DropForeignKey("dbo.PrzedmiotyRoks", "Rok_Id", "dbo.Roks");
            DropForeignKey("dbo.PrzedmiotyRoks", "Przedmioty_Id", "dbo.Przedmioties");
            DropForeignKey("dbo.Podgrupas", "Rok_Id", "dbo.Roks");
            DropForeignKey("dbo.Egzamins", "Podgrupa1_Id", "dbo.Podgrupas");
            DropIndex("dbo.PrzedmiotyRoks", new[] { "Rok_Id" });
            DropIndex("dbo.PrzedmiotyRoks", new[] { "Przedmioty_Id" });
            DropIndex("dbo.Podgrupas", new[] { "Rok_Id" });
            DropIndex("dbo.Egzamins", new[] { "Sale_Id" });
            DropIndex("dbo.Egzamins", new[] { "Podgrupa1_Id" });
            DropTable("dbo.PrzedmiotyRoks");
            DropTable("dbo.Sales");
            DropTable("dbo.Przedmioties");
            DropTable("dbo.Roks");
            DropTable("dbo.Podgrupas");
            DropTable("dbo.Egzamins");
        }
    }
}
