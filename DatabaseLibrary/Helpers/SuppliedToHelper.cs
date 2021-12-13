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
    public class SuppliedToHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static SuppliedTo_db Add(int batch_id, int mid, string location, string name,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(name?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a first name.");
                if (string.IsNullOrEmpty(location?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a last name.");

                // Generate a new instance
                SuppliedTo_db instance = new SuppliedTo_db
                    (
                        batch_id,//Guid.NewGuid().ToString(), //This can be ignored is PK in your DB is auto increment
                        mid,
                        location,
                        name
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO supplied_to (Batch_id, MID, Location, Name) values (@batch_id, @mid, @location, @name)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@batch_id", instance.Batch_id },
                            { "@mid", instance.MID },
                            { "@name", instance.Name},
                            { "@location", instance.Location },
                     

                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Supplied_to added successfully");
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
        public static List<SuppliedTo_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM supplied_to",
                        parameters: new Dictionary<string, object>()
                        {
                          
                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<SuppliedTo_db> instances = new List<SuppliedTo_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new SuppliedTo_db
                            (
                                batch_id: (int) row["Batch_id"],
                                mid: (int) row["MID"],
                                name: row["Name"].ToString(),
                                location:  row["Location"].ToString()
                                 
                     
                                // password: row["Password"].ToString()
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Supplied_to list has been retrieved successfully.");
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
