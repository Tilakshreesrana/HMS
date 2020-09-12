namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedpictureentities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccomodationPackageImages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomodationPackageID = c.Int(nullable: false),
                        ImageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Images", t => t.ImageID, cascadeDelete: true)
                .ForeignKey("dbo.AccomodationPackages", t => t.AccomodationPackageID, cascadeDelete: true)
                .Index(t => t.AccomodationPackageID)
                .Index(t => t.ImageID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AccomodationImages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomodationID = c.Int(nullable: false),
                        ImageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Images", t => t.ImageID, cascadeDelete: true)
                .ForeignKey("dbo.Accomodations", t => t.AccomodationID, cascadeDelete: true)
                .Index(t => t.AccomodationID)
                .Index(t => t.ImageID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccomodationImages", "AccomodationID", "dbo.Accomodations");
            DropForeignKey("dbo.AccomodationImages", "ImageID", "dbo.Images");
            DropForeignKey("dbo.AccomodationPackageImages", "AccomodationPackageID", "dbo.AccomodationPackages");
            DropForeignKey("dbo.AccomodationPackageImages", "ImageID", "dbo.Images");
            DropIndex("dbo.AccomodationImages", new[] { "ImageID" });
            DropIndex("dbo.AccomodationImages", new[] { "AccomodationID" });
            DropIndex("dbo.AccomodationPackageImages", new[] { "ImageID" });
            DropIndex("dbo.AccomodationPackageImages", new[] { "AccomodationPackageID" });
            DropTable("dbo.AccomodationImages");
            DropTable("dbo.Images");
            DropTable("dbo.AccomodationPackageImages");
        }
    }
}
