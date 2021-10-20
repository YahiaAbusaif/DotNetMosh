namespace Hello1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateMovie : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "Name", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Movies", "Release", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Release", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Movies", "Name", c => c.String());
        }
    }
}
