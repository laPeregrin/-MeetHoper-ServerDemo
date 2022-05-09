using System;

namespace Common.Abstractions
{
    public interface IEntity<T>
    {
        Guid Id { get; set; }
    }

}
