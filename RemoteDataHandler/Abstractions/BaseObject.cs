using System;

namespace Common.Abstractions
{
    public class BaseObject : IEntity<Guid>
    {
        public Guid Id { get; set; }
    }
}
