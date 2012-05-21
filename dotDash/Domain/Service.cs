using System;

namespace dotDash.Domain
{
    public class Service
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }        
    }
}