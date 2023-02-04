using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using PPM.Model;

namespace PPM.DAL
{
    public class EmployeeDal
    {
        public void InsertIntoEmployeeTable(int employeeId, string firstName, string lastName, string email, string mobileNumber, string address, int roleId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string CommandString = @"
                INSERT INTO EMPLOYEES VALUES(" + employeeId + ",'" + firstName + "','" + lastName + "','" + email + "','" + mobileNumber + "','" + address + "'," + roleId + ");";
                    MySqlCommand command = new MySqlCommand(CommandString, connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! error occured");
            }
        }

        public Boolean IfExistsInEmployeeTable(int employeeId)
        {
            string connnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connnectionString))
            {
                string commandString = "Select * from EMPLOYEES where employeeId = " + employeeId + "";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["employeeId"] };
                if (dataTable.Rows.Contains(employeeId))
                {
                    return true;
                }
            }
            return false;
        }

        public void ReadData()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string commandString = @"
                                            SELECT EMPLOYEES.employeeid,EMPLOYEES.firstName,EMPLOYEES.lastName,EMPLOYEES.email,EMPLOYEES.mobileNumber,EMPLOYEES.address,EMPLOYEES.roleid,ROLES.roleName
                                            FROM employees
                                            INNER JOIN roles
                                            ON employees.roleid = roles.roleid;";
                    MySqlCommand command = new MySqlCommand(commandString, connection);
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["employeeId"] };
                    if(dataTable.Rows.Count != 0)
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MySqlDataReader dataReader = command.ExecuteReader();
                        Console.WriteLine("\n");
                        Console.WriteLine("   ----Available Employees---- \n");
                        while (dataReader.Read())
                        {
                            Console.WriteLine(" Employee Id - " + dataReader["employeeId"] + "\n Employee first name - " + dataReader["firstName"] + "\n Employee last name - " + dataReader["lastName"] + "\n Employee email id - " + dataReader["email"] + "\n Employee mobile number - " + dataReader["mobileNumber"] + "\n Employee address - " + dataReader["address"] + "\n Role Id - " + dataReader["roleId"] + "\n Role Name - " + dataReader["roleName"] + "\n");
                            Console.WriteLine();
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No employees available");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops error occured");
            }
        }

        public void ReadDataById(int employeeId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string commandString = @"SELECT EMPLOYEES.employeeid,EMPLOYEES.firstName,EMPLOYEES.lastName,EMPLOYEES.email,EMPLOYEES.mobileNumber,EMPLOYEES.address,EMPLOYEES.roleid,ROLES.roleName
                                            FROM employees
                                            INNER JOIN roles
                                            ON employees.roleid = roles.roleid where employeeId = " + employeeId + ";";
                    MySqlCommand command = new MySqlCommand(commandString, connection);
                    if (IfExistsInEmployeeTable(employeeId))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MySqlDataReader dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            Console.WriteLine("");
                            Console.WriteLine(" Employee Id - " + dataReader["employeeId"] + "\n Employee first name - " + dataReader["firstName"] + "\n Employee last name - " + dataReader["lastName"] + "\n Employee email id - " + dataReader["email"] + "\n Employee mobile number - " + dataReader["mobileNumber"] + "\n Employee address - " + dataReader["address"] + "\n Role Id - " + dataReader["roleId"] + "\n Role Name - " + dataReader["roleName"] + "\n");
                            Console.WriteLine();
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("employee id doesn't exist");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops error occured" + ex);
            }
        }

        public void DeleteEmployeeFromTable(int employeeId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string commandString = "Delete from EMPLOYEES where employeeId = " + employeeId + "";
                    MySqlCommand command = new MySqlCommand(commandString, connection);
                            connection.Open();
                            command.ExecuteNonQuery();
                            Console.WriteLine("Deleted successfully !");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Cannot find id" + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops, error occured" + ex);
            }
        }
    }
}