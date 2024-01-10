namespace OSCEUKDI.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProyek : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Proyek",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Nama = c.String(maxLength: 8000, unicode: false),
                        Deskripsi = c.String(maxLength: 8000, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Proyek");
        }
    }
}
