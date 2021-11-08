namespace Hello1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedCustomerManger : DbMigration
    {
        public override void Up()
        {
            Sql(@"

            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'38565423-48a5-4829-9b83-4fda4fc1a430', N'canMangeCustomers')

            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'980b1691-c494-43a3-af96-08bcd2112249', N'38565423-48a5-4829-9b83-4fda4fc1a430')

            " );
        }
        
        public override void Down()
        {
        }
    }
}
