using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
namespace PPM.DAL;
public class ProjectDal
{
    public void InsertIntoProjectTable(int projectId,string projectName,string startDate,string endDate)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = @"INSERT INTO PROJECTS VALUES (" + projectId + ",'" + projectName + "','" + startDate + "','" + endDate + "')";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Oops, an error occured "+ex);
        }
    }

    public Boolean IfExistsInProjects(int projectId)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using(MySqlConnection connection =new MySqlConnection(connectionString))
            {
                string commandString = @"select * from projects where projectid =  "+projectId+";";
                MySqlCommand command = new MySqlCommand(commandString,connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable= new DataTable();
                dataAdapter.Fill(dataTable);
                dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["projectId"] };
                if (dataTable.Rows.Contains(projectId))
                {
                    return true;
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("Oops, error occured");
        }
        return false;
    }

    public void ReadData()
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using(MySqlConnection connection =new MySqlConnection(connectionString))
            {
                string commandString = @"select * from projects;";
                MySqlCommand command = new MySqlCommand(commandString,connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable= new DataTable();
                dataAdapter.Fill(dataTable);
                dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["projectId"] };
                if (dataTable.Rows.Count != 0)
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while(dataReader.Read() )
                    {
                        Console.WriteLine(" Name of the project - " + dataReader["projectName"] + "\n Project Id - " + dataReader["projectId"] + "\n Start date of project - " + dataReader["startDate"] + "\n End date of project - " + dataReader["endDate"]);
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No projects available");
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("Oops an error occured");
        }
    }

    public void ReadDataById(int projectId)
    {
        try
        {
           string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using(MySqlConnection connection =new MySqlConnection(connectionString))
            {
                string commandString = @"select * from projects where projectId = "+projectId+";";
                MySqlCommand command = new MySqlCommand(commandString,connection);
                if(IfExistsInProjects(projectId))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while(dataReader.Read())
                    {
                        Console.WriteLine(" Name of the project - " + dataReader["projectName"] + "\n Project Id - " + dataReader["projectId"] + "\n Start date of project - " + dataReader["startDate"] + "\n End date of project - " + dataReader["endDate"]);
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No project exists with this id");
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("Oops error occured");
        }
    }

    public void AddEmployeesIntoProject(int projectId,int employeeId)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using(MySqlConnection connection =new MySqlConnection(connectionString))
            {
                string commandString = @"INSERT INTO PROJECTSWITHEMPLOYEES VALUES ("+projectId+","+employeeId+")";
                MySqlCommand command = new MySqlCommand(commandString,connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("Oops error occured  "+ex);
        }
    }

    public void DisplayAllEmployeeInProjectById(int projectId)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using(MySqlConnection connection =new MySqlConnection(connectionString))
            {
                string commandString = @"SELECT
    `employees`.* , roles.roleName
FROM
    `employees`
    JOIN `projectswithemployees` ON `employees`.`employeeid` = `projectswithemployees`.`employeeId`
                                            INNER JOIN roles
                                            ON employees.roleid = roles.roleid
WHERE
    `projectswithemployees`.`projectId` = "+projectId+"  ORDER BY roleId;";
                MySqlCommand command = new MySqlCommand(commandString,connection);
                connection.Open();
                command.ExecuteNonQuery();
                MySqlDataReader dataReader = command.ExecuteReader();
                Console.WriteLine("Below are the details of employees in this project");
                Console.WriteLine("-------------------------------------------------------------------------------------");

                while(dataReader.Read())
                {
                    Console.WriteLine(dataReader["firstName"] + " [" + dataReader["roleName"] + "]");
                    Console.WriteLine("-------------------------------------------------------------------------------------");
                }
                
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("Oops error occured  "+ex);
        }
    }

    public void DeleteProject(int projectId)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using(MySqlConnection connection =new MySqlConnection(connectionString))
            {
                string commandString = @"delete from projects where projectid =  "+projectId+";";
                MySqlCommand command = new MySqlCommand(commandString,connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("Oops, error occured"+ex);
        }
    }

    public void DeleteEmployeeFromProject(int projectId,int employeeId)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = @"Delete from ProjectsWithEmployees where projectid =  " + projectId + " and employeeId = " + employeeId + ";";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Oops, error occured");
        }
    }
}