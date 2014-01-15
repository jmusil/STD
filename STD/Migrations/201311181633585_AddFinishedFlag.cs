namespace STD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFinishedFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Finished", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "Finished");
        }
    }
}
