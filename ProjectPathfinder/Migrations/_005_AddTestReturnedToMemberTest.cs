using FluentMigrator;

namespace ProjectPathfinder.Migrations
{
    [Migration(5)]
    public class _005_AddTestReturnedToMemberTest : Migration
    {
        public override void Up()
        {
            Create.Column("testToResubmit").OnTable("members_test").AsBoolean().SetExistingRowsTo(false).NotNullable();
        }

        public override void Down()
        {
            Delete.Column("testToResubmit").FromTable("members_test");
        }
    }
}