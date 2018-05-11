namespace Webnfwithapi1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsersFKUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ADuser = c.String(),
                        Num_insp = c.String(),
                        FIO_User = c.String(),
                        user_permission = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Zayavkis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Num_insp = c.String(),
                        Num_zayavki = c.String(),
                        Opisanie = c.String(),
                        Data_start = c.DateTime(nullable: false),
                        Data_end = c.DateTime(nullable: false),
                        Zayavitel = c.String(),
                        Ispolnitel = c.String(),
                        Type_zayavki = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Zayavkis");
            DropTable("dbo.UsersFKUs");
        }
    }
}
