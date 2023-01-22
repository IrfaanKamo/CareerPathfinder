using System;
using FluentMigrator;
using System.Data;

namespace ProjectPathfinder.Migrations
{
    [Migration(1)]
    public class _001_UsersAndRoles : Migration
    {
        public override void Up()
        {
            Create.Table("roles")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("name").AsString(128);

            Create.Table("users")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("email").AsCustom("VARCHAR(256)")
                .WithColumn("password_hash").AsString(128)
                .WithColumn("role_id").AsInt32().ForeignKey("roles", "id").OnDelete(Rule.None);

            Create.Table("members")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("user_id").AsInt32().ForeignKey("users", "id").OnDelete(Rule.Cascade)
                .WithColumn("firstNames").AsString(128)
                .WithColumn("surname").AsString(128)
                .WithColumn("dateOfBirth").AsDateTime()
                .WithColumn("primaryNumber").AsString(40)
                .WithColumn("secondaryNumber").AsString(40).Nullable()
                .WithColumn("school").AsString(128)
                .WithColumn("grade").AsInt32()
                .WithColumn("dateOfRegistration").AsDateTime();
        }

        public override void Down()
        {
            Delete.Table("roles");
            Delete.Table("users");
            Delete.Table("members");
        }
    }
}