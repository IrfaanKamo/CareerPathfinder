using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;


namespace ProjectPathfinder.Models
{
    public class MemberResult
    {
        public virtual int Id { get; set; }
        public virtual byte[] ResultObject { get; set; }
        public virtual DateTime SubmittedDate { get; set; }
        public virtual MemberUser MemberUser { get; set; }
        public virtual User Submitter { get; set; }
    }

    public class MemberResultMap : ClassMapping<MemberResult>
    {
        public MemberResultMap()
        {
            Table("members_result");

            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.ResultObject, x => x.NotNullable(false));
            Property(x => x.SubmittedDate, x => x.NotNullable(false));

            ManyToOne(x => x.MemberUser, x =>
            {
                x.Column("member_id");
                x.Cascade(Cascade.None);
                x.Fetch(FetchKind.Join);
            });

            ManyToOne(x => x.Submitter, x =>
            {
                x.Column("submitter_id");
                x.Cascade(Cascade.None);
                x.Fetch(FetchKind.Join);
                x.NotNullable(false);
        });
        }
    }
}