using FluentMigrator;
using System.Data;

namespace ProjectPathfinder.Migrations
{
    [Migration(6)]
    public class _006_Invoicing : Migration
    {
        public override void Up()
        {
            Create.Table("product")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("name").AsString(128)
                .WithColumn("description").AsCustom("VARCHAR(256)").Nullable()
                .WithColumn("price").AsDouble();

            Create.Table("member_invoice")
                .WithColumn("number").AsInt32().Identity().PrimaryKey()
                .WithColumn("dateOfPurchase").AsDateTime()
                .WithColumn("totalAmount").AsDouble()
                .WithColumn("member_id").AsInt32().ForeignKey("members", "id").OnDelete(Rule.None);

            Create.Table("member_invoice_item")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("product_id").AsInt32().ForeignKey("product", "id").OnDelete(Rule.None)
                .WithColumn("invoice_number").AsInt32().ForeignKey("member_invoice", "number").OnDelete(Rule.Cascade)
                .WithColumn("price").AsDouble()
                .WithColumn("discountApplied").AsDouble();
        }

        public override void Down()
        {
            Delete.Table("product");
            Delete.Table("member_invoice");
            Delete.Table("member_invoice_item");
        }
    }
}