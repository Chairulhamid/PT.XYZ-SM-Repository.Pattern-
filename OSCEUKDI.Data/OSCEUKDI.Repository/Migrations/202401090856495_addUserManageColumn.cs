namespace OSCEUKDI.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserManageColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Phone", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.User", "BidangUsaha", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.User", "JenisPerusahaan", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.User", "Photo", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.User", "StatusApproval", c => c.String(maxLength: 8000, unicode: false));
            DropColumn("dbo.User", "Token");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Token", c => c.String(maxLength: 500, unicode: false));
            DropColumn("dbo.User", "StatusApproval");
            DropColumn("dbo.User", "Photo");
            DropColumn("dbo.User", "JenisPerusahaan");
            DropColumn("dbo.User", "BidangUsaha");
            DropColumn("dbo.User", "Phone");
        }
    }
}
