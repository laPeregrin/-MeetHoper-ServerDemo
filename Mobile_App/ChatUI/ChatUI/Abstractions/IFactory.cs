namespace ChatUI.Abstractions
{
    public interface IFactory<TInput, TOutput>
    {
        
        TOutput GetValue(TInput input);

    }
}
