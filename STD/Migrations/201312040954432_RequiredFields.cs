namespace STD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tasks", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "Title", c => c.String());
        }
    }
}
