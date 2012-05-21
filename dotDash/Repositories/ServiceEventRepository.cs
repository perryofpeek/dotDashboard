using System;
using System.Collections.Generic;

using NHibernate;
using NHibernate.Criterion;

using dotDash.Domain;

namespace dotDash.Repositories
{
    public class ServiceEventRepository : IServiceEventRepository
    {
        public void Add(ServiceEvent serviceEvent)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(serviceEvent);
                    transaction.Commit();
                }
            }
        }

        public ServiceEvent GetById(Guid eventId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var serviceEvent = session
                    .CreateCriteria(typeof(ServiceEvent))
                    .Add(Restrictions.Eq("EventId", eventId))
                    .UniqueResult<ServiceEvent>();
                return serviceEvent;
            }
        }

        public IList<ServiceEvent> GetByServiceId(Guid serviceId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var serviceEvent = session.CreateCriteria(typeof(ServiceEvent)).Add(Restrictions.Eq("ServiceId", serviceId));
                return serviceEvent.List<ServiceEvent>();                
            }
        }

        public IList<ServiceEvent> GetLastServiceEventsById(Guid serviceId, int number)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var serviceEvent = session.CreateCriteria(typeof(ServiceEvent))
                    .Add(Restrictions.Eq("ServiceId", serviceId))
                    .SetFirstResult(0)
                    .SetMaxResults(number).AddOrder(Order.Asc("Created"));
                return serviceEvent.List<ServiceEvent>();
            }            
        }
    }
}