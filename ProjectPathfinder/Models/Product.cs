using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ProjectPathfinder.Models
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual double Price { get; set; }
    }

    public class ProductMap : ClassMapping<Product>
    {
        public ProductMap()
        {
            Table("product");

            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.Name, x => x.NotNullable(true));
            Property(x => x.Description, x => x.NotNullable(false));
            Property(x => x.Price, x => x.NotNullable(true));
        }
    }
}