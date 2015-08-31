namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class token_added4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Token", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Token");
        }
    }
}
