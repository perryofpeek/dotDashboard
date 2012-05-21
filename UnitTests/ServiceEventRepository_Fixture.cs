using System;

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
    public class ServiceEventRepository_Fixture
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



        private  ServiceEvent[] serviceEvents;

        private void CreateInitialData()
        {
            var sId = Guid.NewGuid();

            serviceEvents = new[]
                 {
                     new ServiceEvent {ServiceId  = sId, Description = "build desc", State = "Up",Created = new DateTime(2012,3,8,11,00,00)},
                     new ServiceEvent {ServiceId  = sId, Description = "dev desc", State = "Down",Created = new DateTime(2012,3,8,12,00,00)},
                     new ServiceEvent {ServiceId  = sId, Description = "env Description", State = "Error",Created = new DateTime(2012,3,8,13,00,00)},
                     new ServiceEvent {ServiceId  = Guid.NewGuid(), Description = "thing description", State = "Up",Created = new DateTime(2012,11,8,10,52,00)},
                     new ServiceEvent {ServiceId  = Guid.NewGuid(), Description = "Desc", State = "dwn",Created = new DateTime(2012,3,7,12,00,00)}
                 };

            using (ISession session = _sessionFactory.OpenSession())
            {
               using (ITransaction transaction = session.BeginTransaction())
                {
                    foreach (var serviceEvent in this.serviceEvents)
                    {
                        session.Save(serviceEvent);
                    }

                    transaction.Commit();
                }
            }
        }

        [Test]
        public void Can_add_new_service_event()
        {
            var serviceEvent = new ServiceEvent { Description = "Desc", State = "Open", Created = new DateTime(2011, 3, 4, 5, 6, 7) };
            IServiceEventRepository repository = new ServiceEventRepository();
            repository.Add(serviceEvent);

            // use session to try to load the service
            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<ServiceEvent>(serviceEvent.EventId);
                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(serviceEvent, fromDb);
                Assert.AreEqual(serviceEvent.State, fromDb.State);
                Assert.AreEqual(serviceEvent.Description, fromDb.Description);
                Assert.AreEqual(serviceEvent.EventId, fromDb.EventId);
                Assert.AreEqual(serviceEvent.ServiceId, fromDb.ServiceId);
                Assert.AreEqual(serviceEvent.Created, fromDb.Created);
            }
        }

        [Test]
        public void Can_get_existing_serviceEvent_by_id()
        {
            IServiceEventRepository repository = new ServiceEventRepository();
            var fromDb = repository.GetById(this.serviceEvents[1].EventId);
            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(this.serviceEvents[1], fromDb);
            Assert.AreEqual(this.serviceEvents[1].Created, fromDb.Created);
            Assert.AreEqual(this.serviceEvents[1].State, fromDb.State);
        }


        [Test]
        public void Can_get_existing_serviceEvents_by_service_id()
        {
            IServiceEventRepository repository = new ServiceEventRepository();
            var fromDb = repository.GetByServiceId(this.serviceEvents[0].ServiceId);
            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(this.serviceEvents[0], fromDb[0]);

            Assert.AreEqual(this.serviceEvents[0].EventId , fromDb[0].EventId);           
            Assert.AreEqual(this.serviceEvents[0].State, fromDb[0].State);
            Assert.AreEqual(this.serviceEvents[0].Created, fromDb[0].Created);
        }

        [Test]
        public void Can_get_last_x_existing_serviceEvents_by_id()
        {
            IServiceEventRepository repository = new ServiceEventRepository();
            var id = this.serviceEvents[0].ServiceId;
            var fromDb = repository.GetLastServiceEventsById(id, 3);
            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(this.serviceEvents[0], fromDb[0]);
            Assert.That(fromDb.Count, Is.EqualTo(3));
            Assert.AreEqual(this.serviceEvents[0].ServiceId, fromDb[0].ServiceId);            
            Assert.AreEqual(this.serviceEvents[0].EventId, fromDb[0].EventId);
            Assert.AreEqual(this.serviceEvents[0].State, fromDb[0].State);
            Assert.AreEqual(this.serviceEvents[0].Created, fromDb[0].Created);
            Assert.AreEqual(this.serviceEvents[1].ServiceId, fromDb[1].ServiceId);
            Assert.AreEqual(this.serviceEvents[1].EventId, fromDb[1].EventId);
            Assert.AreEqual(this.serviceEvents[1].State, fromDb[1].State);
            Assert.AreEqual(this.serviceEvents[1].Created, fromDb[1].Created);
        }
    }
}