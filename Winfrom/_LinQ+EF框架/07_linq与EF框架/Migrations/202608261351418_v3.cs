namespace _07_linq与EF框架.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassRooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassRoomName = c.String(nullable: false, maxLength: 50, unicode: false),
                        CreateUserId = c.Int(),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        LastUpdateUserId = c.Int(),
                        LastUpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StuName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Age = c.Int(),
                        Sex = c.Boolean(),
                        ClassId = c.Int(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        LastUpdateUserId = c.Int(),
                        LastUpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 50, unicode: false),
                        Status = c.Int(nullable: false),
                        Create = c.DateTime(nullable: false),
                        LastUpdateUserId = c.Int(nullable: false),
                        LastUpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserInfo");
            DropTable("dbo.StudentInfo");
            DropTable("dbo.ClassRooms");
        }
    }
}
