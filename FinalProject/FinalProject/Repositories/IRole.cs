using FinalProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Repositories
{
    public interface IRoleRepo
    {
        Role GetRoleByName(string roleName);
    }

    public class RoleRepo : IRoleRepo
    {
        private NhatNgheDbContext _context;

        public RoleRepo(NhatNgheDbContext context)
        {
            _context = context;
        }

        public Role GetRoleByName(string roleName)
        {
            return _context.Roles.SingleOrDefault(r => r.RoleName == roleName);
        }
    }
}
