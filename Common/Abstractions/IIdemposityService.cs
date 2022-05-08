using System.Threading.Tasks;

namespace Common.Abstractions
{
    public interface IIdemposityService<TId, TTaskResult>
    {
        Task<TTaskResult> TryAddProcess(TId id, Task<TTaskResult> task);
        Task<TTaskResult> TryAddPriorityProcess(TId id, Task<TTaskResult> task);
    }
}
