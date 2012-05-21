using System;
using System.Collections.Generic;

using NHibernate;
using NHibernate.Criterion;

using dotDash.Domain;

namespace dotDash.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        public void Add(Service service)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(service);
                    transaction.Commit();
                }
            }
        }

        public void Update(Service service)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(service);
                    transaction.Commit();
                }
            }
        }

        public void Remove(Service service)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(service);
                    transaction.Commit();
                }
            }
        }

        public Service GetById(Guid serviceId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Get<Service>(serviceId);
            }
        }

        public Service GetByName(string name)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var product = session
                    .CreateCriteria(typeof(Service))
                    .Add(Restrictions.Eq("Name", name))
                    .UniqueResult<Service>();
                return product;
            }
        }
    }
}