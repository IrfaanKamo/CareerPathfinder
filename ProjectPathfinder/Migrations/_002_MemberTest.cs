using System;
using FluentMigrator;
using System.Data;

namespace ProjectPathfinder.Migrations
{
    [Migration(2)]
    public class _002_MemberTest : Migration
    {
        public override void Up()
        {
            Create.Table("members_test")
               .WithColumn("id").AsInt32().Identity().PrimaryKey()
               .WithColumn("member_id").AsInt32().ForeignKey("members", "id").OnDelete(Rule.Cascade)
               .WithColumn("testObject").AsCustom("BLOB").Nullable()
               .WithColumn("submittedDate").AsDateTime().Nullable();

            Create.Table("members_result")
               .WithColumn("id").AsInt32().Identity().PrimaryKey()
               .WithColumn("member_id").AsInt32().ForeignKey("members", "id").OnDelete(Rule.Cascade)
               .WithColumn("resultObject").AsCustom("BLOB").Nullable()
               .WithColumn("submittedDate").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("members_test");
            Delete.Table("members_result");
        }
    }
}