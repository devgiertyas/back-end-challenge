using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TodoAPI.DataTransferObjects.User
{
    public class UserPutDTO
    {
        [ValidateNever]
        public string Name { get; set; }

        [ValidateNever]
        public string Email { get; set; }

    }
}
