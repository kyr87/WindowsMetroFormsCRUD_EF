namespace WindowsFormsCRUD_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeToNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "Birthday", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Birthday", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Employees", "Email", c => c.String());
        }
    }
}
