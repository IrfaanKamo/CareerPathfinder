using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace ProjectPathfinder.Models
{
    public class MemberTest
    {
        public virtual int Id { get; set; }
        public virtual byte[] TestObject { get; set; }
        public virtual DateTime SubmittedDate { get; set; }
        public virtual bool TestToResubmit { get; set; }
        public virtual MemberUser MemberUser { get; set; }
    }

    public class MemberTestMap : ClassMapping<MemberTest>
    {
        public MemberTestMap()
        {
            Table("members_test");

            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.TestObject, x => x.NotNullable(false));
            Property(x => x.SubmittedDate, x => x.NotNullable(false));
            Property(x => x.TestToResubmit, x => x.NotNullable(false));

            ManyToOne(x => x.MemberUser, x =>
            {
                x.Column("member_id");
                x.Cascade(Cascade.None);
                x.Fetch(FetchKind.Join);
            });
        }
    }
}