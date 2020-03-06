namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatcolumnsencounters : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Encounters");
            AddColumn("dbo.Encounters", "PAT_ENC_CSN_ID", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Encounters", "PAT_MRN_ID", c => c.String());
            AddColumn("dbo.Encounters", "DX_Codes", c => c.String());
            AddColumn("dbo.Encounters", "DX_Names", c => c.String());
            AddColumn("dbo.Encounters", "Provider_1", c => c.String());
            AddColumn("dbo.Encounters", "Provider_2", c => c.String());
            AddColumn("dbo.Encounters", "Provider_3", c => c.String());
            AddColumn("dbo.Encounters", "Provider_4", c => c.String());
            AddColumn("dbo.Encounters", "Encouter_CloseTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Encounters", "Encounter_Entry_Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.Encounters", "Encounter_Closed", c => c.String());
            AddColumn("dbo.Encounters", "Encounter_Type", c => c.String());
            AddColumn("dbo.Encounters", "Contact_Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Encounters", "Appointment_Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.Encounters", "Appointment_Status", c => c.String());
            AddColumn("dbo.Encounters", "Checkin_Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.Encounters", "Check_Out_Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.Encounters", "Last_Audit_Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Encounters", "Department_Name", c => c.String());
            AddColumn("dbo.Encounters", "Address_Line_1", c => c.String());
            AddColumn("dbo.Encounters", "Address_Line_2", c => c.String());
            AddColumn("dbo.Encounters", "City", c => c.String());
            AddColumn("dbo.Encounters", "State", c => c.String());
            AddColumn("dbo.Encounters", "Zip_Code", c => c.String());
            AddColumn("dbo.Encounters", "Living_Status", c => c.String());
            AddColumn("dbo.Encounters", "Visit_Type", c => c.String());
            AddColumn("dbo.Encounters", "Visit_Wait_Time", c => c.String());
            AddPrimaryKey("dbo.Encounters", "PAT_ENC_CSN_ID");
            DropColumn("dbo.Encounters", "PAT_something");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Encounters", "PAT_something", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Encounters");
            DropColumn("dbo.Encounters", "Visit_Wait_Time");
            DropColumn("dbo.Encounters", "Visit_Type");
            DropColumn("dbo.Encounters", "Living_Status");
            DropColumn("dbo.Encounters", "Zip_Code");
            DropColumn("dbo.Encounters", "State");
            DropColumn("dbo.Encounters", "City");
            DropColumn("dbo.Encounters", "Address_Line_2");
            DropColumn("dbo.Encounters", "Address_Line_1");
            DropColumn("dbo.Encounters", "Department_Name");
            DropColumn("dbo.Encounters", "Last_Audit_Date");
            DropColumn("dbo.Encounters", "Check_Out_Time");
            DropColumn("dbo.Encounters", "Checkin_Time");
            DropColumn("dbo.Encounters", "Appointment_Status");
            DropColumn("dbo.Encounters", "Appointment_Time");
            DropColumn("dbo.Encounters", "Contact_Date");
            DropColumn("dbo.Encounters", "Encounter_Type");
            DropColumn("dbo.Encounters", "Encounter_Closed");
            DropColumn("dbo.Encounters", "Encounter_Entry_Time");
            DropColumn("dbo.Encounters", "Encouter_CloseTime");
            DropColumn("dbo.Encounters", "Provider_4");
            DropColumn("dbo.Encounters", "Provider_3");
            DropColumn("dbo.Encounters", "Provider_2");
            DropColumn("dbo.Encounters", "Provider_1");
            DropColumn("dbo.Encounters", "DX_Names");
            DropColumn("dbo.Encounters", "DX_Codes");
            DropColumn("dbo.Encounters", "PAT_MRN_ID");
            DropColumn("dbo.Encounters", "PAT_ENC_CSN_ID");
            AddPrimaryKey("dbo.Encounters", "PAT_something");
        }
    }
}
