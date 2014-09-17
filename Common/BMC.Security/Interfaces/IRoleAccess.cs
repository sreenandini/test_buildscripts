namespace BMC.Security.Interfaces
{
    public interface IRoleAccess
    {
        int RoleAccessID { get; set; }
        int? ParentID { get; set; }
        string ParentName { get; set; }
        string RoleAccessName { get; set; }
        string Description { get; set; }
    }
}
