namespace Hello1.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class addPromoCodeAndEditMovieAndRental : DbMigration
	{
		public override void Up()
		{

			CreateTable(
				"dbo.PromoCodes",
				c => new
					{
						ID = c.Int(nullable: false, identity: true),
						Name = c.String(nullable: false, maxLength: 25),
						NumberofUse = c.Int(nullable: false),
						Discount = c.Int(nullable: false),
						MaxDiscountInCents = c.Int(nullable: false),
						MembershipRequired_ID = c.Byte(),
					})
				.PrimaryKey(t => t.ID)
				.ForeignKey("dbo.MembershipTypes", t => t.MembershipRequired_ID)
				.Index(t => t.MembershipRequired_ID);
			
			AddColumn("dbo.Customers", "Money", c => c.Int(nullable: false));
			AddColumn("dbo.Movies", "RentalPriceInCentsForDay", c => c.Int());
			AddColumn("dbo.Rentals", "Promo_ID", c => c.Int());
			CreateIndex("dbo.Rentals", "Promo_ID");
			AddForeignKey("dbo.Rentals", "Promo_ID", "dbo.PromoCodes", "ID");
			DropColumn("dbo.Movies", "Price");
			CreateIndex(
			name: "IX_PromoCodes_Name",
			table: "PromoCodes",
			column: "Name");
		}
		
		public override void Down()
		{
			AddColumn("dbo.Movies", "Price", c => c.Double());
			DropForeignKey("dbo.Rentals", "Promo_ID", "dbo.PromoCodes");
			DropForeignKey("dbo.PromoCodes", "MembershipRequired_ID", "dbo.MembershipTypes");
			DropIndex("dbo.Rentals", new[] { "Promo_ID" });
			DropIndex("dbo.PromoCodes", new[] { "MembershipRequired_ID" });
			DropColumn("dbo.Rentals", "Promo_ID");
			DropColumn("dbo.Movies", "RentalPriceInCentsForDay");
			DropColumn("dbo.Customers", "Money");
			DropTable("dbo.PromoCodes");
		}
	}
}
