
using PPM.Model;
using PPM.Domain;
using MySql.Data.MySqlClient;
using PPM.DAL;

namespace NUnitTesting
{
    [TestFixture]
    public class Tests
    {
        /*[Test]
        public void InsertRole()
        {
            int roleId = 10;
            string roleName = "Prolifics";
            var role = new Role(roleId, roleName);
            using(var mock = AutoMock.GetLoose())
            {
                mock.Mock<ISqlRole>().Setup(x => x.InsertIntoRoleTable(roleId, roleName));
                var cls = mock.Create<RoleDal>();
                cls.InsertIntoRoleTable(10,"Prolifics");
                *//*                mock.Mock<ISqlRole>().Verify(x => x.InsertIntoRoleTable(roleId, roleName), Times.Exactly(1));
                *//*
                Assert.Equal(1, mock.Invocations.Count);
            }
        }*/

        [Test]
        public void InsertRole()
        {
            int roleId = 14;
            string roleName = "Prolifics";
            var role = new Role(roleId, roleName);
            RoleManagement roleManagement = new RoleManagement();
            roleManagement.RoleAdd(role);
            string connectionString = "Server=127.0.0.1;User ID=root;Password=123456879;Database=prolificsprojectmanagement";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = "SELECT * FROM ROLES  WHERE roleid = " + roleId + ";";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                connection.Open();
                command.ExecuteNonQuery();
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Assert.That(dataReader["roleId"], Is.EqualTo(roleId));
                }
            }
        }

        [Test]
        public void RoleExist()
        {
            int roleId = 1;
            RoleDal roleDal = new RoleDal();
            Assert.True(roleDal.IfExistsInRoleTable(roleId));
        }

        [Test]
        public void EmployeeExists()
        {
            int employeeId = 2;
            EmployeeDal employeeDal = new EmployeeDal();
            Assert.True(employeeDal.IfExistsInEmployeeTable(employeeId));
        }

    }
}