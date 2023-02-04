using System.Configuration;
using System.Data;
using IEntityOperation;
using MySql.Data.MySqlClient;

namespace PPM.DAL
{
    public class RoleDal : ISqlRole
    {
        public void InsertIntoRoleTable(int roleId, string roleName)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string commandString = @"     CREATE TABLE IF NOT EXISTS ROLES
                                            (
                                                    roleid int NOT NULL AUTO_INCREMENT,
                                                    roleName varchar(50),
                                                    PRIMARY KEY (roleid)
                                            );
                     INSERT INTO ROLES VALUES(" + roleId + ", '" + roleName + "');";
                    MySqlCommand command = new MySqlCommand(commandString, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("Added Successfully!");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Id already exists");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops!, an error occured \n \n" + ex);
            }
        }

        public void ReadData()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string commandString = "SELECT * FROM ROLES;";
                    MySqlCommand command = new MySqlCommand(commandString, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    MySqlDataReader dataReader= command.ExecuteReader();
                    while(dataReader.Read() )
                    {
                        Console.WriteLine(" Name of the Role - " + dataReader["roleName"] + "\n Role Id - " + dataReader["roleId"]);
                        Console.WriteLine();
                    }
                }
            }
            catch(MySqlException ex)
            {
                Console.WriteLine("Id already exists");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops error occured");
            }
        }

        public void ReadDataById(int roleId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string commandString = "SELECT * FROM ROLES  WHERE roleid = "+roleId+";";
                    MySqlCommand command = new MySqlCommand(commandString, connection);
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                    DataTable dataTable= new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["roleid"] };
                    if (dataTable.Rows.Contains(roleId))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MySqlDataReader dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            Console.WriteLine("Role Id : " + dataReader["roleid"] + ", Role Name : [" + dataReader["roleName"] + "]");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Role exists with that id");
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Id already exists"+ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops error occured"+ex);
            }
        }


        public void DeleteRoleFromTable(int roleId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string commandString = "delete from ROLES where roleid = " + roleId + "";
                    MySqlCommand command = new MySqlCommand(commandString,connection);
                    if (!IfRoleExistsInEmployeeTable(roleId))
                    {
                        if (IfExistsInRoleTable(roleId))
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            Console.WriteLine("Deleted successfully !");
                        }
                        else
                        {
                            Console.WriteLine("Role id does not exists");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Employee with this role id exists, please delete that first");
                    }
                }
            }
            catch(MySqlException ex)
            {
                Console.WriteLine("Cannot find id"+ex);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Oops, error occured"+ex);
            }
        }

        public Boolean IfExistsInRoleTable(int roleId)
        {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string commandString = "Select * from ROLES where roleid = " + roleId + "";
                    MySqlCommand command = new MySqlCommand(commandString, connection);
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["roleid"] };
                    if (dataTable.Rows.Contains(roleId))
                    {
                        return true;
                    }
                }
                return false;
        }

        public Boolean IfRoleExistsInEmployeeTable(int roleId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = "Select * from EMPLOYEES where roleId = " + roleId + "";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["roleid"] };
                if (dataTable.Rows.Contains(roleId))
                {
                    return true;
                }
            }
            return false;
        }
    }
}