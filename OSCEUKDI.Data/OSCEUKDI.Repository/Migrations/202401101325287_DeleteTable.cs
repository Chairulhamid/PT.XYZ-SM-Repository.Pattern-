namespace OSCEUKDI.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Kompetensi");
            DropTable("dbo.RuangUjian");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RuangUjian",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        NamaRuangan = c.String(nullable: false, maxLength: 50, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Kompetensi",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        NamaKompetensi = c.String(nullable: false, maxLength: 100, unicode: false),
                        KategoriOsce = c.String(maxLength: 8000, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
