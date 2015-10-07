namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userModelChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserRole", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserRole");
        }
    }
}
