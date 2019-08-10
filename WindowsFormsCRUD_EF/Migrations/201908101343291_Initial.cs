namespace WindowsFormsCRUD_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmpID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Birthday = c.String(),
                        Address = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.EmpID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
