using FluentMigrator;

namespace ProjectPathfinder.Migrations
{
    [Migration(7)]
    public class _007_InvoiceAdditions : Migration
    {
        public override void Up()
        {
            Create.Column("paymentReceived").OnTable("member_invoice").AsBoolean().WithDefaultValue(false);
        }

        public override void Down()
        {
            Delete.Column("paymentReceived").FromTable("member_invoice");
        }
    }
}