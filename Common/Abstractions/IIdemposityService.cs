using System.Threading.Tasks;

namespace Common.Abstractions
{
    public interface IIdemposityService<TId, TTaskResult>
    {
        Task<TTaskResult> TryAddProcessAsync(TId id, Task<TTaskResult> task);
    }
}
