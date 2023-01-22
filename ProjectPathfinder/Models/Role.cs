using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ProjectPathfinder.Models
{
    public class Role
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }       
    }

    public class RoleMap : ClassMapping<Role>
    {
        public RoleMap()
        {
            Table("roles");

            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.Name, x => x.NotNullable(true));
        }
    }
}