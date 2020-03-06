namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedecnounter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Encounters",
                c => new
                    {
                        PAT_something = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.PAT_something);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Encounters");
        }
    }
}
