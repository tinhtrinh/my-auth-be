namespace Domain.Roles;

public class RolePermission
{
    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    public ICollection<Role>? Roles { get; private set; }

    public RolePermission(int roleId, int permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }
}