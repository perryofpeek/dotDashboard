using System;
using System.Collections.Generic;

namespace dotDash.Domain
{
    public interface IServiceEventRepository
    {
        void Add(ServiceEvent serviceEvent);

        ServiceEvent GetById(Guid productId);

        IList<ServiceEvent> GetByServiceId(Guid serviceId);

        IList<ServiceEvent> GetLastServiceEventsById(Guid id, int number);
    }
}