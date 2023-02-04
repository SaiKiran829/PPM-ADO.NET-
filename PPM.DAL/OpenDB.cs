using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace PPM.DAL
{
    public class OpenDB
    {
        public void StartApp()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string commandString = @"     CREATE TABLE IF NOT EXISTS ROLES
                                            (
                                                    roleid int NOT NULL AUTO_INCREMENT,
                                                    roleName varchar(50),
                                                    PRIMARY KEY (roleid)
                                            );
                     INSERT INTO roles
SELECT * FROM (SELECT 1, 'Technical Lead') AS tmp
WHERE NOT EXISTS (
    SELECT roleid FROM roles WHERE roleid = 1
) LIMIT 1;
                     INSERT INTO roles
SELECT * FROM (SELECT 2, 'Software Engineer') AS tmp
WHERE NOT EXISTS (
    SELECT roleid FROM roles WHERE roleid = 2
) LIMIT 1;
                     INSERT INTO roles
SELECT * FROM (SELECT 3, 'Associate Software Engineer') AS tmp
WHERE NOT EXISTS (
    SELECT roleid FROM roles WHERE roleid = 3
) LIMIT 1;
                    INSERT INTO roles
SELECT * FROM (SELECT 4, 'Trainee Software Engineer') AS tmp
WHERE NOT EXISTS (
    SELECT roleid FROM roles WHERE roleid = 4
) LIMIT 1;";
                    MySqlCommand command = new MySqlCommand(commandString, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using(MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string commandString = @" CREATE TABLE IF NOT EXISTS PROJECTS
                                            (
                                                    projectId int NOT NULL AUTO_INCREMENT,
                                                    projectName varchar(50),
                                                    startDate varchar(15),
                                                    endDate varchar(15),
                                                    PRIMARY KEY (projectId)
                                            );";
                    MySqlCommand command = new MySqlCommand(commandString,connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Oops error occured");
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string CommandString = @"CREATE TABLE IF NOT EXISTS EMPLOYEES (
                            employeeid int NOT NULL, 
                firstName varchar(50),
                lastName varchar(50),
                email varchar(100),
                mobileNumber varchar(15),
                address varchar(200),
                roleId int NOT NULL,
                PRIMARY KEY (employeeid),
                FOREIGN KEY (roleId) REFERENCES ROLES(roleid));";
                    MySqlCommand command = new MySqlCommand(CommandString, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! error occured");
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string CommandString = @"CREATE TABLE IF NOT EXISTS `ProjectsWithEmployees` (
    `projectId` INT NOT NULL,
    `employeeId` INT NOT NULL,
    PRIMARY KEY (`projectId`, `employeeId`),
    CONSTRAINT `Constr_ProjectsWithEmployees_projectId_fk`
        FOREIGN KEY `projectId_fk` (`projectId`) REFERENCES `projects` (`projectId`)
        ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT `Constr_ProjectsWithEmployees_employeeId_fk`
        FOREIGN KEY `employeeId_fk` (`employeeId`) REFERENCES `employees` (`employeeId`)
        ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=INNODB CHARACTER SET ascii COLLATE ascii_general_ci";
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

    public Boolean IfExistsInProjectsWithEmployees(int projectId,int employeeId)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using(MySqlConnection connection =new MySqlConnection(connectionString))
            {
                string commandString = @"select * from ProjectsWithEmployees where projectid =  "+projectId+" and employeeId = "+employeeId+";";
                MySqlCommand command = new MySqlCommand(commandString,connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable= new DataTable();
                dataAdapter.Fill(dataTable);
                dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["projectId"] };
                if (dataTable.Rows.Contains(projectId) )
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
    
 public Boolean IfExistsInProjectsWithEmployeesTable(int projectId)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using(MySqlConnection connection =new MySqlConnection(connectionString))
            {
                string commandString = @"select * from ProjectsWithEmployees where projectid =  "+projectId+";";
                MySqlCommand command = new MySqlCommand(commandString,connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable= new DataTable();
                dataAdapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                    {
                        if(Convert.ToInt32(row["projectId"])    == projectId)
                        {
                            return true;
                        }
                    }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("Oops, error occured"+ex);
        }
        return false;
    }

    public void DeleteProjectFromProjectWithEmployees(int projectId)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using(MySqlConnection connection =new MySqlConnection(connectionString))
            {
                string commandString = @"delete from ProjectsWithEmployees where projectid =  "+projectId+";";
                MySqlCommand command = new MySqlCommand(commandString,connection);
                if(IfExistsInProjectsWithEmployeesTable(projectId))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                else
                {
                    Console.WriteLine("Project with this id does not exist");
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("Oops, error occured"+ex);
        }
    }
    }
}