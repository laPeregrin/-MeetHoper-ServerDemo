using System;

namespace Common.Abstractions
{
    public interface IEntity<T>
    {
        public Guid Id { get; set; }
    }

}
