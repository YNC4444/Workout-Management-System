namespace PassionProjectn01681774.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExerciseXMuscle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExerciseXMuscles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ExerciseId = c.Int(nullable: false),
                        muscleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Exercises", t => t.ExerciseId, cascadeDelete: true)
                .ForeignKey("dbo.Muscles", t => t.muscleId, cascadeDelete: true)
                .Index(t => t.ExerciseId)
                .Index(t => t.muscleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExerciseXMuscles", "muscleId", "dbo.Muscles");
            DropForeignKey("dbo.ExerciseXMuscles", "ExerciseId", "dbo.Exercises");
            DropIndex("dbo.ExerciseXMuscles", new[] { "muscleId" });
            DropIndex("dbo.ExerciseXMuscles", new[] { "ExerciseId" });
            DropTable("dbo.ExerciseXMuscles");
        }
    }
}
