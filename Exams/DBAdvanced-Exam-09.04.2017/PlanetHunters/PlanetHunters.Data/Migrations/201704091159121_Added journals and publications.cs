namespace PlanetHunters.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedjournalsandpublications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Publications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        Journal_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Discoveries", t => t.Id)
                .ForeignKey("dbo.Journals", t => t.Journal_Id)
                .Index(t => t.Id)
                .Index(t => t.Journal_Id);
            
            CreateTable(
                "dbo.Journals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Publications", "Journal_Id", "dbo.Journals");
            DropForeignKey("dbo.Publications", "Id", "dbo.Discoveries");
            DropIndex("dbo.Publications", new[] { "Journal_Id" });
            DropIndex("dbo.Publications", new[] { "Id" });
            DropTable("dbo.Journals");
            DropTable("dbo.Publications");
        }
    }
}
