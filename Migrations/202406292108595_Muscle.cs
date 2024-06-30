namespace PassionProjectn01681774.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Muscle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Muscles",
                c => new
                    {
                        MuscleId = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                    })
                .PrimaryKey(t => t.MuscleId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Muscles");
        }
    }
}
