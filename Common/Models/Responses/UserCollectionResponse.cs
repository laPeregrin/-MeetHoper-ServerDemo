using System.Collections.Generic;

namespace Common.Models.Responses
{
    public class UserCollectionResponse
    {
        public IEnumerable<UserPublicDataResponse> UsersArray { get; set; }

        public UserCollectionResponse() { }

        public UserCollectionResponse(IEnumerable<UserPublicDataResponse> usersArray)
        {
            UsersArray = usersArray;
        }
    }
}
