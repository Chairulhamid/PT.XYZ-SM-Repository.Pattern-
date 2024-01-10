namespace OSCEUKDI.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteNoPegawaiUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.User", "NoPegawai");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "NoPegawai", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
    }
}
