using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using ProjectPathfinder.Models;
using System.Web;

namespace ProjectPathfinder
{
    public static class Database
    {
        private const string SessionKey = "ProjectPathfinder.Database.SessionKey";
        private static ISessionFactory _sessionFactory;

        public static ISession Session
        {
            get { return (ISession)HttpContext.Current.Items[SessionKey]; }
        }

        public static void Configure()
        {
            var config = new Configuration();

            //configure the connection string
            config.Configure();

            //add our mappings
            var mapper = new ModelMapper();
            mapper.AddMapping<RoleMap>();
            mapper.AddMapping<UserMap>();
            mapper.AddMapping<MemberUserMap>();
            mapper.AddMapping<MemberTestMap>();
            mapper.AddMapping<MemberResultMap>();
            mapper.AddMapping<PasswordForgotRequestMap>();
            mapper.AddMapping<ProductMap>();
            mapper.AddMapping<MemberInvoiceMap>();
            mapper.AddMapping<MemberInvoiceItemMap>();

            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            //create our session factory
            _sessionFactory = config.BuildSessionFactory();

        }

        public static void OpenSession()
        {
            HttpContext.Current.Items[SessionKey] = _sessionFactory.OpenSession();
        }

        public static void CloseSession()
        {
            var session = HttpContext.Current.Items[SessionKey] as ISession;
            if (session != null)
                session.Close();

            HttpContext.Current.Items.Remove(SessionKey);
        }
    }
}