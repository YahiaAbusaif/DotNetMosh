namespace Hello1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addgenres : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        MoviesNumber = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MovieGenres",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GenreID = c.Int(nullable: false),
                        MovieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Genres", t => t.GenreID, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieID, cascadeDelete: true)
                .Index(t => t.GenreID)
                .Index(t => t.MovieID);
            
            AddColumn("dbo.Movies", "AverageReviewOutOfTen", c => c.Double());
            AddColumn("dbo.Movies", "NumberInStock", c => c.Int());
            AddColumn("dbo.Movies", "Price", c => c.Double());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieGenres", "MovieID", "dbo.Movies");
            DropForeignKey("dbo.MovieGenres", "GenreID", "dbo.Genres");
            DropIndex("dbo.MovieGenres", new[] { "MovieID" });
            DropIndex("dbo.MovieGenres", new[] { "GenreID" });
            DropColumn("dbo.Movies", "Price");
            DropColumn("dbo.Movies", "NumberInStock");
            DropColumn("dbo.Movies", "AverageReviewOutOfTen");
            DropTable("dbo.MovieGenres");
            DropTable("dbo.Genres");
        }
    }
}
