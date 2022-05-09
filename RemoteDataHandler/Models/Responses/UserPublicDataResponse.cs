using Common.Abstractions;

namespace Common.Models.Responses
{
    public class UserPublicDataResponse : BaseObject
    {
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public double Rate { get; set; }
        public string Description { get; set; }

        public UserPublicDataResponse() { }
    }
}
