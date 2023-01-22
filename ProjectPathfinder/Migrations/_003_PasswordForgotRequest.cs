using System;
using FluentMigrator;
using System.Data;

namespace ProjectPathfinder.Migrations
{
    [Migration(3)]
    public class _003_PasswordForgotRequest : Migration
    {
        public override void Up()
        {
            Create.Table("password_forgot_request")
                .WithColumn("id").AsString(128).Unique().PrimaryKey()
                .WithColumn("email").AsCustom("VARCHAR(256)")
                .WithColumn("expiry_time").AsDateTime();
        }

        public override void Down()
        {
            Delete.Table("password_forgot_request");
        }
    }
}