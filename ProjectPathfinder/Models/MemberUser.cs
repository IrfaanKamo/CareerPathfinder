using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace ProjectPathfinder.Models
{
    public class MemberUser
    {
        public virtual int Id { get; set; }
        public virtual string FirstNames { get; set; }
        public virtual string Surname { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual string PrimaryNumber { get; set; }
        public virtual string SecondaryNumber { get; set; }
        public virtual string School { get; set; }
        public virtual int Grade { get; set; }
        public virtual DateTime DateOfRegistration { get; set; }
        public virtual User User { get; set; }
    }

    public class MemberUserMap : ClassMapping<MemberUser>
    {
        public MemberUserMap()
        {
            Table("members");

            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.FirstNames, x => x.NotNullable(true));
            Property(x => x.Surname, x => x.NotNullable(true));
            Property(x => x.DateOfBirth, x => x.NotNullable(true));
            Property(x => x.PrimaryNumber, x => x.NotNullable(true));
            Property(x => x.SecondaryNumber, x => x.NotNullable(false));
            Property(x => x.School, x => x.NotNullable(true));
            Property(x => x.Grade, x => x.NotNullable(true));
            Property(x => x.DateOfRegistration, x => x.NotNullable(true));

            ManyToOne(x => x.User, x =>
            {
                x.Column("user_id");
                x.Cascade(Cascade.None);
                x.Fetch(FetchKind.Join);
            });
        }
    }
}