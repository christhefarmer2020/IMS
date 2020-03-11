namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databaseupdate123 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Encounters", "First_Name", c => c.String());
            AddColumn("dbo.Encounters", "Middle_Name", c => c.String());
            AddColumn("dbo.Encounters", "Last_Name", c => c.String());
            AddColumn("dbo.Encounters", "SSN", c => c.String());
            AddColumn("dbo.Encounters", "Date_Of_Birth", c => c.DateTime(nullable: false));
            AddColumn("dbo.Encounters", "Gender", c => c.String());
            AddColumn("dbo.Encounters", "Race", c => c.String());
            AddColumn("dbo.Encounters", "Ethnic_Group", c => c.String());
            AddColumn("dbo.Encounters", "Email_Address", c => c.String());
            AddColumn("dbo.Encounters", "Nurse_Wait_Time", c => c.String());
            AddColumn("dbo.Encounters", "PAT_ID", c => c.String());
            AddColumn("dbo.Encounters", "Phys_Wait_Time", c => c.String());
            AddColumn("dbo.Encounters", "Phys_Time_W_Paitent", c => c.String());
            AddColumn("dbo.Encounters", "Post_Visit_Charting", c => c.String());
            AddColumn("dbo.Encounters", "Pre_Visit_Charting", c => c.String());
            AddColumn("dbo.Encounters", "Referal_ID", c => c.String());
            AddColumn("dbo.Encounters", "Cancelation_Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Encounters", "Cancelation_Date");
            DropColumn("dbo.Encounters", "Referal_ID");
            DropColumn("dbo.Encounters", "Pre_Visit_Charting");
            DropColumn("dbo.Encounters", "Post_Visit_Charting");
            DropColumn("dbo.Encounters", "Phys_Time_W_Paitent");
            DropColumn("dbo.Encounters", "Phys_Wait_Time");
            DropColumn("dbo.Encounters", "PAT_ID");
            DropColumn("dbo.Encounters", "Nurse_Wait_Time");
            DropColumn("dbo.Encounters", "Email_Address");
            DropColumn("dbo.Encounters", "Ethnic_Group");
            DropColumn("dbo.Encounters", "Race");
            DropColumn("dbo.Encounters", "Gender");
            DropColumn("dbo.Encounters", "Date_Of_Birth");
            DropColumn("dbo.Encounters", "SSN");
            DropColumn("dbo.Encounters", "Last_Name");
            DropColumn("dbo.Encounters", "Middle_Name");
            DropColumn("dbo.Encounters", "First_Name");
        }
    }
}
