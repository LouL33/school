using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace DotnetUnivesrity
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = 
                @"Server=localhost\SQLEXPRESS;Database=DotNet University;Trusted_Connection=True;";

            var courses = new List<Course>();

            using(var connection = new SqlConnection(connectionString))
            {
                Console.WriteLine("Connected");
                connection.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.CommandText = @"SELECT *
                                            FROM [DotNet University].[dbo].[Course]
                                            JOIN Instructors on Course.[Instrutor Id] = Instructors.Id";
                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    var course = new Course
                    {
                        Title = reader["Title"].ToString(),
                        Id = (int)reader["Id"],
                        InstrutorId = (int)reader["InstrutorId"],
                        CourseNumber = (int)reader["CourseNumber"],
                        DepartmentId = (int)reader["DepartmentId"]

                    };
                    courses.Add(course);

                    /*
                    Console.WriteLine(reader["Title"] + "--" + reader["Instrutor Id"]);
                    Console.WriteLine();
                    Console.WriteLine(reader["Name"] + "--" + reader["Id"]);*/
                }     
                connection.Close();
            }
            foreach(var course in courses)
            {
                Console.WriteLine(course.Title);
            }



        }
    }
}
