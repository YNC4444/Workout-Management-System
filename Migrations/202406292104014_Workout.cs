namespace PassionProjectn01681774.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Workout : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        WorkoutId = c.Int(nullable: false, identity: true),
                        muscleId = c.Int(nullable: false),
                        WorkoutDate = c.String(),
                    })
                .PrimaryKey(t => t.WorkoutId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Workouts");
        }
    }
}
