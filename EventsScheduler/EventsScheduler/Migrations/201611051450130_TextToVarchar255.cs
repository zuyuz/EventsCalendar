namespace EventsScheduler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TextToVarchar255 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("public.Events", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("public.Locations", "Address", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("public.Users", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("public.Users", "Login", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("public.Users", "Password", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("public.Users", "Password", c => c.String(nullable: false));
            AlterColumn("public.Users", "Login", c => c.String(nullable: false));
            AlterColumn("public.Users", "Name", c => c.String(nullable: false));
            AlterColumn("public.Locations", "Address", c => c.String(nullable: false));
            AlterColumn("public.Events", "Name", c => c.String(nullable: false));
        }
    }
}
