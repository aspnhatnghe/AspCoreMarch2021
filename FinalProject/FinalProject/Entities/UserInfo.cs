using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Entities
{
    public class UserInfo
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RandomKey { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastAccess { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserAddress> UserAddresses { get; set; }

        public UserInfo()
        {
            UserRoles = new HashSet<UserRole>();
            UserAddresses = new HashSet<UserAddress>();
        }
    }

    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
    }

    public class UserRole
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
        public bool Access { get; set; }
        public bool Add { get; set; }
        public bool Modify { get; set; }
        public bool Remove { get; set; }

        public Role Role { get; set; }
        public UserInfo User { get; set; }
    }
}
