using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

using NUnit.Framework;

using dotDash.Domain;
using dotDash.Repositories;

namespace UnitTests
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class ServiceRepository_Fixture
    {
        private ISessionFactory _sessionFactory;
        private Configuration _configuration;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this._configuration = new Configuration();
            this._configuration.Configure();
            this._configuration.AddAssembly(typeof(Service).Assembly);
            this._sessionFactory = this._configuration.BuildSessionFactory();
        }

        [SetUp]
        public void SetupContext()
        {
            new SchemaExport(this._configuration).Execute(false, true, false);
            CreateInitialData();
        }

        private readonly Service[] _services = new[]
                 {
                     new Service {Name = "Build", Description = "build desc"},
                     new Service {Name = "Dev", Description = "dev desc"},
                     new Service {Name = "env", Description = "env Description"},
                     new Service {Name = "thing", Description = "thing description"},
                     new Service {Name = "thing two", Description = "Desc"},
                 };

        private void CreateInitialData()
        {

            using (ISession session = _sessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (var service in this._services)
                    session.Save(service);
                transaction.Commit();
            }
        }

        [Test]
        public void Can_add_new_service()
        {
            var service = new Service { Name = "Build", Description = "Build stuff" };
            IServiceRepository repository = new ServiceRepository();
            repository.Add(service);

            // use session to try to load the service
            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<Service>(service.Id);                
                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(service, fromDb);
                Assert.AreEqual(service.Name, fromDb.Name);
                Assert.AreEqual(service.Description, fromDb.Description);
            }
        }

        [Test]
        public void Can_update_existing_service()
        {
            var service = this._services[0];
            service.Name = "endpoint";
            IServiceRepository repository = new ServiceRepository();
            repository.Update(service);
            
            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<Service>(service.Id);
                Assert.AreEqual(service.Name, fromDb.Name);
            }
        }

        [Test]
        public void Can_remove_existing_service()
        {
            var service = this._services[0];
            IServiceRepository repository = new ServiceRepository();
            repository.Remove(service);

            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<Service>(service.Id);
                Assert.IsNull(fromDb);
            }
        }

        [Test]
        public void Can_get_existing_service_by_id()
        {
            IServiceRepository repository = new ServiceRepository();
            var fromDb = repository.GetById(_services[1].Id);
            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(_services[1], fromDb);
            Assert.AreEqual(_services[1].Name, fromDb.Name);
        }

        [Test]
        public void Can_get_existing_service_by_name()
        {
            IServiceRepository repository = new ServiceRepository();
            var fromDb = repository.GetByName(_services[1].Name);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(_services[1], fromDb);
            Assert.AreEqual(_services[1].Id, fromDb.Id);
        }

    }
}