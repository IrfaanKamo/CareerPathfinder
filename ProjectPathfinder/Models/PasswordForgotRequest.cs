using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace ProjectPathfinder.Models
{
    public class PasswordForgotRequest
    {
        private const int WorkFactor = 13;

        // database attributes
        public virtual string Id { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime ExpiryDate { get; set; }

        public virtual void SetGuid(string guid)
        {
            Id = BCrypt.Net.BCrypt.HashPassword(guid, WorkFactor);
        }

        public virtual bool CheckGuid(string guid)
        {
            if (guid == null)
                return false;

            return BCrypt.Net.BCrypt.Verify(guid, Id);
        }
    }

    public class PasswordForgotRequestMap : ClassMapping<PasswordForgotRequest>
    {
        public PasswordForgotRequestMap()
        {
            Table("password_forgot_request");

            Id(x => x.Id);
            Property(x => x.Email, x => x.NotNullable(true));
            Property(x => x.ExpiryDate, x =>
            {
                x.Column("expiry_time");
                x.NotNullable(true);
            });
        }
    }
}