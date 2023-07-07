namespace KendoProjectDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigrationTwo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Employees_Id", "dbo.Employees");
            DropIndex("dbo.Orders", new[] { "Employees_Id" });
            RenameColumn(table: "dbo.Orders", name: "Employees_Id", newName: "EmployeeId");
            AlterColumn("dbo.Orders", "EmployeeId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Orders", "EmployeeId");
            AddForeignKey("dbo.Orders", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Orders", new[] { "EmployeeId" });
            AlterColumn("dbo.Orders", "EmployeeId", c => c.Guid());
            RenameColumn(table: "dbo.Orders", name: "EmployeeId", newName: "Employees_Id");
            CreateIndex("dbo.Orders", "Employees_Id");
            AddForeignKey("dbo.Orders", "Employees_Id", "dbo.Employees", "Id");
        }
    }
}
