using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace ProjectPathfinder.Models
{
    public class MemberInvoice
    {
        public virtual int Number { get; set; }
        public virtual DateTime DateOfPurchase { get; set; }
        public virtual double TotalAmount { get; set; }
        public virtual MemberUser MemberUser { get; set; }
        public virtual bool PaymentReceived { get; set; }
    }

    public class MemberInvoiceMap : ClassMapping<MemberInvoice>
    {
        public MemberInvoiceMap()
        {
            Table("member_invoice");

            Id(x => x.Number, x => x.Generator(Generators.Identity));
            Property(x => x.DateOfPurchase, x => x.NotNullable(true));
            Property(x => x.TotalAmount, x => x.NotNullable(true));
            Property(x => x.PaymentReceived, x => x.NotNullable(true));

            ManyToOne(x => x.MemberUser, x =>
            {
                x.Column("member_id");
                x.Cascade(Cascade.None);
                x.Fetch(FetchKind.Join);
            });
        }
    }
}