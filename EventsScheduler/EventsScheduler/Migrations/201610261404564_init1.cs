namespace EventsScheduler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Users", "UserRole", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("public.Users", "UserRole");
        }
    }
}
