namespace Hello1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddata : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Genres ON");
            Sql("Insert INTO Genres (ID,Name,MoviesNumber) VALUES(1,'Comedy',0)");
            Sql("Insert INTO Genres (ID,Name,MoviesNumber) VALUES(2,'Action',0)");
            Sql("Insert INTO Genres (ID,Name,MoviesNumber) VALUES(3,'Drama',0)");
            Sql("Insert INTO Genres (ID,Name,MoviesNumber) VALUES(4,'Adventure',0)");
            Sql("Insert INTO Genres (ID,Name,MoviesNumber) VALUES(5,'Family',0)");
            Sql("Insert INTO Genres (ID,Name,MoviesNumber) VALUES(6,'Fantasy',0)");
            Sql("SET IDENTITY_INSERT Genres OFF");

            Sql("Insert INTO MovieGenres (GenreID,MovieID) VALUES(1,1) ");
            Sql("Insert INTO MovieGenres (GenreID,MovieID) VALUES(2,1) ");
            Sql("Insert INTO MovieGenres (GenreID,MovieID) VALUES(3,1) ");
            Sql("Insert INTO MovieGenres (GenreID,MovieID) VALUES(1,2) ");
            Sql("Insert INTO MovieGenres (GenreID,MovieID) VALUES(5,3) ");

        }
        
        public override void Down()
        {
        }
    }
}
