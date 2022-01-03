using Common.Models.DTOs;
using Common.Models.Responses;
using System;

namespace _MeetHoper_ServerDemo.Extensions
{
    public static class ModelsExtensions
    {
        public static UserPublicDataResponse ToResponse(this User user) =>
            new UserPublicDataResponse()
            {
                Id = user.Id,
                UserName = user.UserName,
                Description = user.Description,
                ImageUrl = user.ImageUrl,
                Rate = user.Rate
            };

        public static TOut[] ConvertArrayByArray<TOut, TInput>(this TInput[] inputs, Func<TInput, TOut> func)
        {
            var result = new TOut[inputs.Length];
            for(var i = 0; i < inputs.Length - 1; i++)
            {
                result[i] = func(inputs[i]);
            }

            return result;
        }
    }
}
