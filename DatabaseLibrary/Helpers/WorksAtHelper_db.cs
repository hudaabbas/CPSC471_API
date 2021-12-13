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
    public class WorksAtHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static WorksAt_db Add(int medID, int clinicNo,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // // Validate
                // if (string.IsNullOrEmpty(name?.Trim()))
                //     throw new StatusException(HttpStatusCode.BadRequest, "Please provide a first name.");
                // if (string.IsNullOrEmpty(password?.Trim()))
                //     throw new StatusException(HttpStatusCode.BadRequest, "Please provide a last name.");

                // Generate a new instance
                WorksAt_db instance = new WorksAt_db
                    (
                        medID, //Guid.NewGuid().ToString(), //This can be ignored is PK in your DB is auto increment
                        clinicNo
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO Works_at (MedId, ClinicNo) values (@1, @2)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@1", instance.MedID },
                            { "@2", instance.ClinicNo },
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Work at added successfully");
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
        public static List<WorksAt_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM works_at",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<WorksAt_db> instances = new List<WorksAt_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new WorksAt_db
                            (
                                medID: (int)row["MedID"],
                                clinicNo: (int)row["ClinicNo"]
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("The doctors and thier associated clinics list have been retrieved successfully.");
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