namespace Posts.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bookmarks", "UserId", "dbo.Users");
            DropForeignKey("dbo.Posts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bookmarks", "PostId", "dbo.Posts");
            DropIndex("dbo.Bookmarks", new[] { "UserId" });
            DropIndex("dbo.Bookmarks", new[] { "PostId" });
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            AddColumn("dbo.Comments", "UserLogin", c => c.String(nullable: false));
            DropColumn("dbo.Posts", "UserId");
            DropColumn("dbo.Comments", "UserId");
            DropTable("dbo.Bookmarks");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Bookmarks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Comments", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Comments", "UserLogin");
            CreateIndex("dbo.Comments", "UserId");
            CreateIndex("dbo.Posts", "UserId");
            CreateIndex("dbo.Bookmarks", "PostId");
            CreateIndex("dbo.Bookmarks", "UserId");
            AddForeignKey("dbo.Bookmarks", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Posts", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Bookmarks", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Comments", "UserId", "dbo.Users", "Id");
        }
    }
}
