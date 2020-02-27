namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EncounterImages",
                c => new
                    {
                        Image_Id = c.Int(nullable: false, identity: true),
                        Image_Data = c.Binary(),
                        Consent = c.String(),
                        Appointment_Time = c.DateTime(nullable: false),
                        PAT_MRN = c.String(),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.Image_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EncounterImages");
        }
    }
}
