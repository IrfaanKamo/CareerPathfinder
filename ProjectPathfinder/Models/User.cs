using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ProjectPathfinder.Models
{
    public class User
    {
        private const int WorkFactor = 13;

        public static void FakeHash()
        {
            BCrypt.Net.BCrypt.HashPassword("", WorkFactor);
        }

        // database attributes
        public virtual int Id { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual Role Role { get; set; }

        public virtual void SetPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
        }

        public virtual bool CheckPassword(string password)
        {
            if (password == null)
                return false;

            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }
    }

    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Table("users");

            //primary key
            Id(x => x.Id, x => x.Generator(Generators.Identity));

            //other columns
            Property(x => x.Email, x => x.NotNullable(true));
            Property(x => x.PasswordHash, x =>
            {
                x.Column("password_hash");
                x.NotNullable(true);
            });

            ManyToOne(x => x.Role, x =>
            {
                x.Column("role_id");
                x.Cascade(Cascade.None);
                x.Fetch(FetchKind.Join);
            });
        }
    }
}