using PPM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEntityOperation
{
    public interface ISqlRole
    {
        void InsertIntoRoleTable(int roleId, string roleName);
    }
}
