namespace STD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserNames : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "UserProfile_UserId", "dbo.UserProfile");
            DropIndex("dbo.Tasks", new[] { "UserProfile_UserId" });
            AddColumn("dbo.Tasks", "UserName", c => c.String());
            DropColumn("dbo.Tasks", "UserProfile_UserId");
            DropTable("dbo.UserProfile");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            AddColumn("dbo.Tasks", "UserProfile_UserId", c => c.Int());
            DropColumn("dbo.Tasks", "UserName");
            CreateIndex("dbo.Tasks", "UserProfile_UserId");
            AddForeignKey("dbo.Tasks", "UserProfile_UserId", "dbo.UserProfile", "UserId");
        }
    }
}
