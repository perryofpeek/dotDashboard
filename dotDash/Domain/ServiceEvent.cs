using System;

namespace dotDash.Domain
{
    public class ServiceEvent
    {
        public virtual Guid EventId { get; set; }
        public virtual Guid ServiceId { get; set; }
        public virtual string State { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime Created { get; set; }
    }
}