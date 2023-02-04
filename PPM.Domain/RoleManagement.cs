﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPM.DAL;
using PPM.Model;
using IEntityOperation;

namespace PPM.Domain
{
    public class RoleManagement : IRole
    {
        readonly RoleDal roleDal = new RoleDal();
        public void RoleAdd(Role role)
        {
            roleDal.InsertIntoRoleTable(role.roleId,role.roleName);
        }

        public void ViewRoles()
        {
            roleDal.ReadData();
        }

        public void SearchById(int roleId)
        {
            roleDal.ReadDataById(roleId);
        }

        public void DeleteById(int roleId)
        {
            roleDal.DeleteRoleFromTable(roleId);
        }
    }
}