namespace PassionProjectn01681774.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkoutXExercises : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkoutXExercises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkoutId = c.Int(nullable: false),
                        ExerciseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercises", t => t.ExerciseId, cascadeDelete: true)
                .ForeignKey("dbo.Workouts", t => t.WorkoutId, cascadeDelete: true)
                .Index(t => t.WorkoutId)
                .Index(t => t.ExerciseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkoutXExercises", "WorkoutId", "dbo.Workouts");
            DropForeignKey("dbo.WorkoutXExercises", "ExerciseId", "dbo.Exercises");
            DropIndex("dbo.WorkoutXExercises", new[] { "ExerciseId" });
            DropIndex("dbo.WorkoutXExercises", new[] { "WorkoutId" });
            DropTable("dbo.WorkoutXExercises");
        }
    }
}
