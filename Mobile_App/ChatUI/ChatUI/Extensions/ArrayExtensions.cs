using System;
using System.Linq;

namespace ChatUI.Extensions
{
    public static class ArrayExtensions
    {
        public static TResult[] MapTo<TInput, TResult>(this TInput[] inputs, Func<TInput, TResult> func)
        {
            if (inputs == null || !inputs.Any())
                return Array.Empty<TResult>();

            var results = new TResult[inputs.Length];
            for(var i = 0; i < inputs.Length - 1; i++)
                results[i] = func(inputs[i]);

            return results;
        }
    }
}
