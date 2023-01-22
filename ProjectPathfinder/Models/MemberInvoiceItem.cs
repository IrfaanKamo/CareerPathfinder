using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ProjectPathfinder.Models
{
    public class MemberInvoiceItem
    {
        public virtual int Id { get; set; }
        public virtual Product Product { get; set; }
        public virtual MemberInvoice MemberInvoice { get; set; }
        public virtual double Price { get; set; }
        public virtual double DiscountApplied { get; set; }
    }

    public class MemberInvoiceItemMap : ClassMapping<MemberInvoiceItem>
    {
        public MemberInvoiceItemMap()
        {
            Table("member_invoice_item");

            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.Price, x => x.NotNullable(true));
            Property(x => x.DiscountApplied, x => x.NotNullable(true));

            ManyToOne(x => x.Product, x =>
            {
                x.Column("product_id");
                x.Cascade(Cascade.None);
                x.Fetch(FetchKind.Join);
            });

            ManyToOne(x => x.MemberInvoice, x =>
            {
                x.Column("invoice_number");
                x.Cascade(Cascade.None);
                x.Fetch(FetchKind.Join);
            });
        }
    }
}