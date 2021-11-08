namespace Hello1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedUser : DbMigration
    {
        public override void Up()
        {
            Sql(@"

INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'980b1691-c494-43a3-af96-08bcd2112249', N'admin@gmail.com', 0, N'ABghpVxYaQcnfTRI54HuFxrCR4K6hNvLWGjRR7ZE6VWyDLY78zGbzzt5E6jj+5EOxw==', N'b4a8e4b6-5fe8-4661-871f-7c7b9c274403', NULL, 0, 0, NULL, 1, 0, N'admin@gmail.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c27c4c7d-62f7-41ab-ad55-2bbf5eaf54c7', N'yahiaali2014.ya@gmail.com', 0, N'AGhmA4mFGGIpwh9+YTgqgPNpj20kaaJftMGjzLSriHAzsVQ7f6/gEZ06OVyoPLZE1A==', N'771d9fe8-4b5b-44fe-a793-3dec5e7eb399', NULL, 0, 0, NULL, 1, 0, N'yahiaali2014.ya@gmail.com')


INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'38565423-48a5-4829-9b83-4fda4fc1a427', N'canMangeMovie')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'980b1691-c494-43a3-af96-08bcd2112249', N'38565423-48a5-4829-9b83-4fda4fc1a427')

            " );
        }
        
        public override void Down()
        {
        }
    }
}
