namespace Egzaminy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprzatnieBurdelu : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Egzamins", "Podgrupa1_Id", "dbo.Podgrupas");
            DropForeignKey("dbo.Podgrupas", "Rok_Id", "dbo.Roks");
            DropForeignKey("dbo.Egzamins", "Sale_Id", "dbo.Sales");
            DropIndex("dbo.Egzamins", new[] { "Podgrupa1_Id" });
            DropIndex("dbo.Egzamins", new[] { "Sale_Id" });
            DropIndex("dbo.Podgrupas", new[] { "Rok_Id" });
            CreateTable(
                "dbo.RokEgzamins",
                c => new
                    {
                        Rok_Id = c.Int(nullable: false),
                        Egzamin_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Rok_Id, t.Egzamin_Id })
                .ForeignKey("dbo.Roks", t => t.Rok_Id, cascadeDelete: true)
                .ForeignKey("dbo.Egzamins", t => t.Egzamin_Id, cascadeDelete: true)
                .Index(t => t.Rok_Id)
                .Index(t => t.Egzamin_Id);
            
            CreateTable(
                "dbo.SaleEgzamins",
                c => new
                    {
                        Sale_Id = c.Int(nullable: false),
                        Egzamin_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Sale_Id, t.Egzamin_Id })
                .ForeignKey("dbo.Sales", t => t.Sale_Id, cascadeDelete: true)
                .ForeignKey("dbo.Egzamins", t => t.Egzamin_Id, cascadeDelete: true)
                .Index(t => t.Sale_Id)
                .Index(t => t.Egzamin_Id);
            
            DropColumn("dbo.Egzamins", "Podgrupa");
            DropColumn("dbo.Egzamins", "Podgrupa1_Id");
            DropColumn("dbo.Egzamins", "Sale_Id");
            DropTable("dbo.Podgrupas");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Egzamins", "Sale_Id", c => c.Int());
            AddColumn("dbo.Egzamins", "Podgrupa1_Id", c => c.Int());
            AddColumn("dbo.Egzamins", "Podgrupa", c => c.Int(nullable: false));
            DropForeignKey("dbo.SaleEgzamins", "Egzamin_Id", "dbo.Egzamins");
            DropForeignKey("dbo.SaleEgzamins", "Sale_Id", "dbo.Sales");
            DropForeignKey("dbo.RokEgzamins", "Egzamin_Id", "dbo.Egzamins");
            DropForeignKey("dbo.RokEgzamins", "Rok_Id", "dbo.Roks");
            DropIndex("dbo.SaleEgzamins", new[] { "Egzamin_Id" });
            DropIndex("dbo.SaleEgzamins", new[] { "Sale_Id" });
            DropIndex("dbo.RokEgzamins", new[] { "Egzamin_Id" });
            DropIndex("dbo.RokEgzamins", new[] { "Rok_Id" });
            DropTable("dbo.SaleEgzamins");
            DropTable("dbo.RokEgzamins");
            CreateIndex("dbo.Podgrupas", "Rok_Id");
            CreateIndex("dbo.Egzamins", "Sale_Id");
            CreateIndex("dbo.Egzamins", "Podgrupa1_Id");
            AddForeignKey("dbo.Egzamins", "Sale_Id", "dbo.Sales", "Id");
            AddForeignKey("dbo.Podgrupas", "Rok_Id", "dbo.Roks", "Id");
            AddForeignKey("dbo.Egzamins", "Podgrupa1_Id", "dbo.Podgrupas", "Id");
        }
    }
}
