namespace EventsScheduler.DAL.Migrations
{
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
                        Name = c.String(nullable: false, maxLength: 255),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        FreePlaces = c.Int(nullable: false),
                        Creator_Id = c.Int(nullable: false),
                        EventLocation_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.Users", t => t.Creator_Id, cascadeDelete: true)
                .ForeignKey("public.Locations", t => t.EventLocation_Id, cascadeDelete: true)
                .Index(t => t.Creator_Id)
                .Index(t => t.EventLocation_Id);
            
            CreateTable(
                "public.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Login = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false, maxLength: 255),
                        UserRole = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.UsersEvents",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CourseId })
                .ForeignKey("public.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("public.Events", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.Events", "EventLocation_Id", "public.Locations");
            DropForeignKey("public.Events", "Creator_Id", "public.Users");
            DropForeignKey("public.UsersEvents", "CourseId", "public.Events");
            DropForeignKey("public.UsersEvents", "UserId", "public.Users");
            DropIndex("public.UsersEvents", new[] { "CourseId" });
            DropIndex("public.UsersEvents", new[] { "UserId" });
            DropIndex("public.Events", new[] { "EventLocation_Id" });
            DropIndex("public.Events", new[] { "Creator_Id" });
            DropTable("public.UsersEvents");
            DropTable("public.Locations");
            DropTable("public.Users");
            DropTable("public.Events");
        }
    }
}
