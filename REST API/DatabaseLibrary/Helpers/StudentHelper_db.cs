using DatabaseLibrary.Core;
using DatabaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Text;

namespace DatabaseLibrary.Helpers
{
    public class StudentHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Student_db Add(string firstName, string lastName,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(firstName?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a first name.");
                if (string.IsNullOrEmpty(lastName?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a last name.");

                // Generate a new instance
                Student_db instance = new Student_db
                    (
                        id: Guid.NewGuid().ToString(), //This can be ignored is PK in your DB is auto increment
                        firstName, lastName
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO students (id, first_name, last_name) values (@id, @first_name, @last_name)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@id", instance.Id },
                            { "@first_name", instance.FirstName },
                            { "@last_name", instance.LastName }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Student added successfully");
                return instance;
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
                return null;
            }
        }

        /// <summary>
        /// Retrieves a list of instances.
        /// </summary>
        public static List<Student_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM students",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Student_db> instances = new List<Student_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Student_db
                            (
                                id: row["id"].ToString(),
                                firstName: row["first_name"].ToString(), 
                                lastName: row["last_name"].ToString()
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Students list has been retrieved successfully.");
                return instances;
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
                return null;
            }
        }

    }
}
