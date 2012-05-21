using System;
using System.Collections.Generic;

namespace dotDash.Domain
{
    public interface IServiceRepository
    {
        void Add(Service service);

        void Update(Service service);

        void Remove(Service service);

        Service GetById(Guid serviceId);

        Service GetByName(string name);
    }
}