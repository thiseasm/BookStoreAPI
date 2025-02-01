namespace BookStore.Core.Abstractions.Models.Users
{
    public class UpdateUserRolesRequest
    {
        public required IList<int> RoleIds { get; set; }
    }
}
