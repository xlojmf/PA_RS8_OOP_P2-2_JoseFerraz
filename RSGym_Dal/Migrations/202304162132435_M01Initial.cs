namespace RSGym_Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M01Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ClientID = c.Int(nullable: false, identity: true),
                        PostalCodeID = c.Int(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 100),
                        BirthDate = c.DateTime(nullable: false),
                        Nif = c.String(nullable: false, maxLength: 9),
                        Address = c.String(nullable: false, maxLength: 200),
                        Phone = c.String(nullable: false, maxLength: 9),
                        Email = c.String(nullable: false),
                        Notes = c.String(maxLength: 255),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ClientID)
                .ForeignKey("dbo.PostalCode", t => t.PostalCodeID)
                .Index(t => t.PostalCodeID);
            
            CreateTable(
                "dbo.PostalCode",
                c => new
                    {
                        PostalCodeID = c.Int(nullable: false, identity: true),
                        PostalCodeValue = c.String(nullable: false, maxLength: 8),
                        Locality = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.PostalCodeID);
            
            CreateTable(
                "dbo.PersonalTrainer",
                c => new
                    {
                        PersonalTrainerID = c.Int(nullable: false, identity: true),
                        PostalCodeID = c.Int(nullable: false),
                        PersonalTrainerCode = c.String(nullable: false, maxLength: 4),
                        FullName = c.String(nullable: false, maxLength: 100),
                        Nif = c.String(nullable: false, maxLength: 9),
                        Address = c.String(nullable: false, maxLength: 200),
                        Phone = c.String(nullable: false, maxLength: 9),
                        Email = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.PersonalTrainerID)
                .ForeignKey("dbo.PostalCode", t => t.PostalCodeID)
                .Index(t => t.PostalCodeID);
            
            CreateTable(
                "dbo.Request",
                c => new
                    {
                        RequestID = c.Int(nullable: false, identity: true),
                        ClientID = c.Int(nullable: false),
                        PersonalTrainerID = c.Int(nullable: false),
                        Booking = c.DateTime(nullable: false),
                        State = c.Int(nullable: false),
                        Notes = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.RequestID)
                .ForeignKey("dbo.Client", t => t.ClientID)
                .ForeignKey("dbo.PersonalTrainer", t => t.PersonalTrainerID)
                .Index(t => t.ClientID)
                .Index(t => t.PersonalTrainerID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        UserName = c.String(nullable: false, maxLength: 6),
                        Password = c.String(nullable: false, maxLength: 12),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Request", "PersonalTrainerID", "dbo.PersonalTrainer");
            DropForeignKey("dbo.Request", "ClientID", "dbo.Client");
            DropForeignKey("dbo.PersonalTrainer", "PostalCodeID", "dbo.PostalCode");
            DropForeignKey("dbo.Client", "PostalCodeID", "dbo.PostalCode");
            DropIndex("dbo.Request", new[] { "PersonalTrainerID" });
            DropIndex("dbo.Request", new[] { "ClientID" });
            DropIndex("dbo.PersonalTrainer", new[] { "PostalCodeID" });
            DropIndex("dbo.Client", new[] { "PostalCodeID" });
            DropTable("dbo.User");
            DropTable("dbo.Request");
            DropTable("dbo.PersonalTrainer");
            DropTable("dbo.PostalCode");
            DropTable("dbo.Client");
        }
    }
}
