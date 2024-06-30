namespace PassionProjectn01681774.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkoutMusclefk : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Workouts", "muscleId");
            AddForeignKey("dbo.Workouts", "muscleId", "dbo.Muscles", "MuscleId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workouts", "muscleId", "dbo.Muscles");
            DropIndex("dbo.Workouts", new[] { "muscleId" });
        }
    }
}
