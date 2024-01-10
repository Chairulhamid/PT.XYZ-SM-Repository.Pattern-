namespace OSCEUKDI.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        NIK = c.String(maxLength: 8000, unicode: false),
                        Name = c.String(maxLength: 8000, unicode: false),
                        Email = c.String(maxLength: 8000, unicode: false),
                        CompanyCode = c.String(maxLength: 8000, unicode: false),
                        CompanyName = c.String(maxLength: 8000, unicode: false),
                        OrganizationCode = c.String(maxLength: 8000, unicode: false),
                        OrganizationName = c.String(maxLength: 8000, unicode: false),
                        JobPositionCode = c.String(maxLength: 8000, unicode: false),
                        JobPositionName = c.String(maxLength: 8000, unicode: false),
                        JobLevelCode = c.String(maxLength: 8000, unicode: false),
                        JobLevelName = c.String(maxLength: 8000, unicode: false),
                        ManagerialCode = c.String(maxLength: 8000, unicode: false),
                        ManagerialName = c.String(maxLength: 8000, unicode: false),
                        JobGrade = c.Int(nullable: false),
                        CostCenterCode = c.String(maxLength: 8000, unicode: false),
                        CostCenterName = c.String(maxLength: 8000, unicode: false),
                        JobFamilyCode = c.String(maxLength: 8000, unicode: false),
                        JobFamilyName = c.String(maxLength: 8000, unicode: false),
                        Approval = c.Boolean(nullable: false),
                        WorkLocationCode = c.String(maxLength: 8000, unicode: false),
                        WorkLocationName = c.String(maxLength: 8000, unicode: false),
                        JobStatusCode = c.String(maxLength: 8000, unicode: false),
                        JobStatusName = c.String(maxLength: 8000, unicode: false),
                        JabAkademikCode = c.String(maxLength: 8000, unicode: false),
                        JabAkademikName = c.String(maxLength: 8000, unicode: false),
                        Status = c.String(maxLength: 8000, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.GlobalPerformace",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TahunSemester = c.Long(nullable: false),
                        ACADCareer = c.String(nullable: false, maxLength: 50, unicode: false),
                        Nama = c.String(nullable: false, maxLength: 50, unicode: false),
                        NMin = c.Long(nullable: false),
                        NMax = c.Long(nullable: false),
                        RubrikKompetensiID = c.Long(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RubrikKompetensi", t => t.RubrikKompetensiID, cascadeDelete: true)
                .Index(t => t.RubrikKompetensiID);
            
            CreateTable(
                "dbo.RubrikKompetensi",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TahunSemester = c.Long(nullable: false),
                        ACADCareer = c.String(nullable: false, maxLength: 100, unicode: false),
                        JudulStation = c.String(maxLength: 8000, unicode: false),
                        AlokasiWaktu = c.Long(nullable: false),
                        TingkatKemampuan = c.String(maxLength: 8000, unicode: false),
                        Classification = c.String(maxLength: 8000, unicode: false),
                        InstruksiPeserta = c.String(maxLength: 8000, unicode: false),
                        InstruksiPenguji = c.String(maxLength: 8000, unicode: false),
                        InsNama = c.String(maxLength: 8000, unicode: false),
                        InsUsia = c.Long(nullable: false),
                        Insgender = c.String(maxLength: 8000, unicode: false),
                        InsPekerjaan = c.String(maxLength: 8000, unicode: false),
                        InsStatusPerkawinan = c.String(maxLength: 8000, unicode: false),
                        InsPendidikan = c.String(maxLength: 8000, unicode: false),
                        InsRiwayatPenySekarang = c.String(maxLength: 8000, unicode: false),
                        InsRiwayatPenyDahulu = c.String(maxLength: 8000, unicode: false),
                        InsRiwayatKeluarga = c.String(maxLength: 8000, unicode: false),
                        InsRiwayatKebiasaan = c.String(maxLength: 8000, unicode: false),
                        InsPertanyaanPasien = c.String(maxLength: 8000, unicode: false),
                        InsPeranPasien = c.String(maxLength: 8000, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MappingPertanyaan",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TahunSemester = c.Long(nullable: false),
                        ACADCareer = c.String(nullable: false, maxLength: 20, unicode: false),
                        NomorStation = c.Long(nullable: false),
                        NIPDosenInti = c.String(nullable: false, maxLength: 150, unicode: false),
                        NIPDosen2 = c.String(maxLength: 8000, unicode: false),
                        NIPDosen3 = c.String(maxLength: 8000, unicode: false),
                        RubrikKompetensiID = c.Long(nullable: false),
                        RuangUjianID = c.Long(nullable: false),
                        JadwalTestID = c.Long(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JadwalTest", t => t.JadwalTestID, cascadeDelete: true)
                .ForeignKey("dbo.RuangUjian", t => t.RuangUjianID, cascadeDelete: true)
                .ForeignKey("dbo.RubrikKompetensi", t => t.RubrikKompetensiID, cascadeDelete: true)
                .Index(t => t.RubrikKompetensiID)
                .Index(t => t.RuangUjianID)
                .Index(t => t.JadwalTestID);
            
            CreateTable(
                "dbo.JadwalTest",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ACADCareer = c.String(maxLength: 8000, unicode: false),
                        TahunSemester = c.Long(nullable: false),
                        NamaPeriode = c.String(maxLength: 8000, unicode: false),
                        KategoriOsce = c.String(maxLength: 8000, unicode: false),
                        TanggalUjian = c.String(maxLength: 8000, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MappingMahasiswa",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TahunSemester = c.Long(nullable: false),
                        ACADCareer = c.String(nullable: false, maxLength: 20, unicode: false),
                        AcadProg = c.String(nullable: false, maxLength: 100, unicode: false),
                        JadwalTestID = c.Long(nullable: false),
                        RuangUjianID = c.Long(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JadwalTest", t => t.JadwalTestID, cascadeDelete: true)
                .Index(t => t.JadwalTestID);
            
            CreateTable(
                "dbo.MappingMahasiswaDetail",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        MappingMahasiswaID = c.Long(nullable: false),
                        MahasiswaID = c.String(nullable: false, maxLength: 50, unicode: false),
                        TahunAngkatan = c.Long(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MappingMahasiswa", t => t.MappingMahasiswaID, cascadeDelete: true)
                .Index(t => t.MappingMahasiswaID);
            
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
            
            CreateTable(
                "dbo.RubrikNilaiKomp",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        RubrikKompetensiID = c.Long(nullable: false),
                        Nilai0 = c.String(maxLength: 8000, unicode: false),
                        Nilai1 = c.String(maxLength: 8000, unicode: false),
                        Nilai2 = c.String(maxLength: 8000, unicode: false),
                        Nilai3 = c.String(maxLength: 8000, unicode: false),
                        Nilai4 = c.String(maxLength: 8000, unicode: false),
                        Nilai5 = c.String(maxLength: 8000, unicode: false),
                        Bobot = c.Long(nullable: false),
                        KompetensiID = c.Long(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Kompetensi", t => t.KompetensiID, cascadeDelete: true)
                .ForeignKey("dbo.RubrikKompetensi", t => t.RubrikKompetensiID, cascadeDelete: true)
                .Index(t => t.RubrikKompetensiID)
                .Index(t => t.KompetensiID);
            
            CreateTable(
                "dbo.LogActivity",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserName = c.String(maxLength: 8000, unicode: false),
                        NoPegawai = c.String(maxLength: 8000, unicode: false),
                        Timestamp = c.DateTime(nullable: false),
                        Functional_Performed = c.String(maxLength: 8000, unicode: false),
                        IP_Address = c.String(maxLength: 8000, unicode: false),
                        Other_Information = c.String(maxLength: 8000, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Lookup",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Tipe = c.String(nullable: false, maxLength: 50, unicode: false),
                        Nama = c.String(nullable: false, maxLength: 100, unicode: false),
                        Nilai = c.String(nullable: false, maxLength: 100, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MenuRole",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        IsView = c.Boolean(nullable: false),
                        IsCreate = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        IsUpdate = c.Boolean(nullable: false),
                        RoleID = c.Long(nullable: false),
                        UserID = c.Long(),
                        MenuID = c.Long(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Menu", t => t.MenuID, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.RoleID)
                .Index(t => t.UserID)
                .Index(t => t.MenuID);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        MenuName = c.String(nullable: false, maxLength: 150, unicode: false),
                        MenuDescription = c.String(maxLength: 500, unicode: false),
                        MenuUrl = c.String(nullable: false, maxLength: 100, unicode: false),
                        MenuParent = c.String(maxLength: 8000, unicode: false),
                        MenuIcon = c.String(maxLength: 20, unicode: false),
                        MenuOrder = c.String(maxLength: 20, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 20, unicode: false),
                        RoleName = c.String(nullable: false, maxLength: 150, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 500, unicode: false),
                        Email = c.String(nullable: false, maxLength: 150, unicode: false),
                        NoPegawai = c.String(nullable: false, maxLength: 50, unicode: false),
                        Token = c.String(maxLength: 500, unicode: false),
                        RoleID = c.Long(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Role", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.NilaiUjianMahasiswa",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        MahasiswaID = c.String(maxLength: 8000, unicode: false),
                        TahunAngkatan = c.Long(nullable: false),
                        NilaiMahasiswa = c.Long(nullable: false),
                        GlobalRating = c.String(maxLength: 8000, unicode: false),
                        GlobalRatingName = c.String(maxLength: 8000, unicode: false),
                        ACADCareer = c.String(maxLength: 8000, unicode: false),
                        JudulStation = c.String(maxLength: 8000, unicode: false),
                        NomorStation = c.Long(nullable: false),
                        AlokasiWaktu = c.Long(nullable: false),
                        TingkatKemampuan = c.String(maxLength: 8000, unicode: false),
                        Classification = c.String(maxLength: 8000, unicode: false),
                        InstruksiPeserta = c.String(maxLength: 8000, unicode: false),
                        InstruksiPenguji = c.String(maxLength: 8000, unicode: false),
                        TanggalUjian = c.String(maxLength: 8000, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PenilaianUjian",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        MahasiswaID = c.String(maxLength: 8000, unicode: false),
                        TahunAngkatan = c.Long(nullable: false),
                        NilaiKompetensi = c.Long(nullable: false),
                        GlobalRating = c.String(maxLength: 8000, unicode: false),
                        NamaKompetensi = c.String(maxLength: 8000, unicode: false),
                        JudulSation = c.String(maxLength: 8000, unicode: false),
                        NomorStation = c.Long(nullable: false),
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
            DropForeignKey("dbo.MenuRole", "UserID", "dbo.User");
            DropForeignKey("dbo.User", "RoleID", "dbo.Role");
            DropForeignKey("dbo.MenuRole", "RoleID", "dbo.Role");
            DropForeignKey("dbo.MenuRole", "MenuID", "dbo.Menu");
            DropForeignKey("dbo.RubrikNilaiKomp", "RubrikKompetensiID", "dbo.RubrikKompetensi");
            DropForeignKey("dbo.RubrikNilaiKomp", "KompetensiID", "dbo.Kompetensi");
            DropForeignKey("dbo.MappingPertanyaan", "RubrikKompetensiID", "dbo.RubrikKompetensi");
            DropForeignKey("dbo.MappingPertanyaan", "RuangUjianID", "dbo.RuangUjian");
            DropForeignKey("dbo.MappingPertanyaan", "JadwalTestID", "dbo.JadwalTest");
            DropForeignKey("dbo.MappingMahasiswaDetail", "MappingMahasiswaID", "dbo.MappingMahasiswa");
            DropForeignKey("dbo.MappingMahasiswa", "JadwalTestID", "dbo.JadwalTest");
            DropForeignKey("dbo.GlobalPerformace", "RubrikKompetensiID", "dbo.RubrikKompetensi");
            DropIndex("dbo.User", new[] { "RoleID" });
            DropIndex("dbo.MenuRole", new[] { "MenuID" });
            DropIndex("dbo.MenuRole", new[] { "UserID" });
            DropIndex("dbo.MenuRole", new[] { "RoleID" });
            DropIndex("dbo.RubrikNilaiKomp", new[] { "KompetensiID" });
            DropIndex("dbo.RubrikNilaiKomp", new[] { "RubrikKompetensiID" });
            DropIndex("dbo.MappingMahasiswaDetail", new[] { "MappingMahasiswaID" });
            DropIndex("dbo.MappingMahasiswa", new[] { "JadwalTestID" });
            DropIndex("dbo.MappingPertanyaan", new[] { "JadwalTestID" });
            DropIndex("dbo.MappingPertanyaan", new[] { "RuangUjianID" });
            DropIndex("dbo.MappingPertanyaan", new[] { "RubrikKompetensiID" });
            DropIndex("dbo.GlobalPerformace", new[] { "RubrikKompetensiID" });
            DropTable("dbo.PenilaianUjian");
            DropTable("dbo.NilaiUjianMahasiswa");
            DropTable("dbo.User");
            DropTable("dbo.Role");
            DropTable("dbo.Menu");
            DropTable("dbo.MenuRole");
            DropTable("dbo.Lookup");
            DropTable("dbo.LogActivity");
            DropTable("dbo.RubrikNilaiKomp");
            DropTable("dbo.Kompetensi");
            DropTable("dbo.RuangUjian");
            DropTable("dbo.MappingMahasiswaDetail");
            DropTable("dbo.MappingMahasiswa");
            DropTable("dbo.JadwalTest");
            DropTable("dbo.MappingPertanyaan");
            DropTable("dbo.RubrikKompetensi");
            DropTable("dbo.GlobalPerformace");
            DropTable("dbo.Employee");
        }
    }
}
