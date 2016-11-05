namespace EventsScheduler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        FreePlaces = c.Int(nullable: false),
                        EventLocation_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.Locations", t => t.EventLocation_Id, cascadeDelete: true)
                .Index(t => t.EventLocation_Id);
            
            CreateTable(
                "public.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        UserRole = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.UserEvents",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Event_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Event_Id })
                .ForeignKey("public.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("public.Events", t => t.Event_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Event_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.UserEvents", "Event_Id", "public.Events");
            DropForeignKey("public.UserEvents", "User_Id", "public.Users");
            DropForeignKey("public.Events", "EventLocation_Id", "public.Locations");
            DropIndex("public.UserEvents", new[] { "Event_Id" });
            DropIndex("public.UserEvents", new[] { "User_Id" });
            DropIndex("public.Events", new[] { "EventLocation_Id" });
            DropTable("public.UserEvents");
            DropTable("public.Users");
            DropTable("public.Locations");
            DropTable("public.Events");
        }
    }
}
