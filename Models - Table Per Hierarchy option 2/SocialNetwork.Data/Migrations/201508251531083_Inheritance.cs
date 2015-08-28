namespace SocialNetwork.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inheritance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            //CreateTable(
            //   "dbo.UserGroups",
            //   c => new
            //   {
            //       UserId = c.Int(nullable: false, identity: false),
            //       GroupId = c.Int(nullable: false, identity: false)
            //   })
            //    .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
            //    .ForeignKey("dbo.Groups", t => t.GroupId);

            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        ProfilePicture = c.String(),
                        WallPicture = c.String(),
                        Username = c.String(nullable: false, maxLength: 50),
                        Group_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .Index(t => t.Group_Id);

            CreateTable(
                "dbo.Postings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        PostedOn = c.DateTime(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        GroupId = c.Int(),
                        GroupPostId = c.Int(),
                        UserPostId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        UserWallPost_Id = c.Int(),
                        GroupWallPost_Id = c.Int(),
                        User_Id = c.Int(),
                        User_Id1 = c.Int(),
                        Group_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId, cascadeDelete: false)
                .ForeignKey("dbo.Postings", t => t.GroupPostId)
                .ForeignKey("dbo.Postings", t => t.UserWallPost_Id)
                .ForeignKey("dbo.Postings", t => t.UserPostId)
                .ForeignKey("dbo.Postings", t => t.GroupWallPost_Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.User_Id1)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .Index(t => t.AuthorId)
                .Index(t => t.GroupId)
                .Index(t => t.GroupPostId)
                .Index(t => t.UserPostId)
                .Index(t => t.UserWallPost_Id)
                .Index(t => t.GroupWallPost_Id)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1)
                .Index(t => t.Group_Id);

            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostingId = c.Int(nullable: false),
                        AuthorId = c.String(nullable: false),
                        Author_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Author_Id)
                .ForeignKey("dbo.Postings", t => t.PostingId, cascadeDelete: true)
                .Index(t => t.PostingId)
                .Index(t => t.Author_Id);

            //DropForeignKey("dbo.User", "WallOwnerId", "dbo.UserWallPost");
            //DropIndex("dbo.User", new[] { "WallOwnerId" });
            //DropForeignKey("dbo.UserWallPost", "UserPostId", "dbo.UserWallPostComment");
            //DropIndex("dbo.User", new[] { "WallOwnerId" });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Postings", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Users", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Postings", "User_Id1", "dbo.Users");
            DropForeignKey("dbo.Postings", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Postings", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Postings", "GroupWallPost_Id", "dbo.Postings");
            DropForeignKey("dbo.Postings", "UserPostId", "dbo.Postings");
            DropForeignKey("dbo.Postings", "GroupPostId", "dbo.Postings");
            DropForeignKey("dbo.Postings", "UserWallPost_Id", "dbo.Postings");            
            DropForeignKey("dbo.Likes", "PostingId", "dbo.Postings");
            DropForeignKey("dbo.Likes", "Author_Id", "dbo.Users");
            DropForeignKey("dbo.Postings", "AuthorId", "dbo.Users");
            DropIndex("dbo.Likes", new[] { "Author_Id" });
            DropIndex("dbo.Likes", new[] { "PostingId" });
            DropIndex("dbo.Postings", new[] { "Group_Id" });
            DropIndex("dbo.Postings", new[] { "User_Id1" });
            DropIndex("dbo.Postings", new[] { "User_Id" });
            DropIndex("dbo.Postings", new[] { "GroupWallPost_Id" });
            DropIndex("dbo.Postings", new[] { "UserWallPost_Id" });
            DropIndex("dbo.Postings", new[] { "UserPostId" });
            DropIndex("dbo.Postings", new[] { "GroupPostId" });
            DropIndex("dbo.Postings", new[] { "GroupId" });
            DropIndex("dbo.Postings", new[] { "AuthorId" });
            DropIndex("dbo.Users", new[] { "Group_Id" });
            DropTable("dbo.Likes");
            DropTable("dbo.Postings");
            DropTable("dbo.Users");
            DropTable("dbo.Groups");
        }
    }
}
