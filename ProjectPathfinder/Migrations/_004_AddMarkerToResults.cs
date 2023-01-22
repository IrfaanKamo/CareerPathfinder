using FluentMigrator;
using System.Data;

namespace ProjectPathfinder.Migrations
{
    [Migration(4)]
    public class _004_AddMarkerToResults : Migration
    {
        public override void Up()
        {
            Create.Column("submitter_id").OnTable("members_result").AsInt32().ForeignKey("users", "id").OnDelete(Rule.SetNull).Nullable();
        }

        public override void Down()
        {
            Delete.Column("submitter_id").FromTable("members_result");
        }
    }
}