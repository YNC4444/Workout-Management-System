namespace PassionProjectn01681774.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Weight : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "ExerciseWeight", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "ExerciseWeight");
        }
    }
}
