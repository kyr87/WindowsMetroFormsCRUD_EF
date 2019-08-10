namespace WindowsFormsCRUD_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeToDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Birthday", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Birthday", c => c.String());
        }
    }
}
