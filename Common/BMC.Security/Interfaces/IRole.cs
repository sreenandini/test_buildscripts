namespace BMC.Security.Interfaces
{
    public interface IRole
    {
        int SecurityRoleID
        {
            get;
            set;
        }

        string RoleName
        {
            get;
            set;
        }

        string RoleDescription
        {
            get;
            set;
        }
    }
}