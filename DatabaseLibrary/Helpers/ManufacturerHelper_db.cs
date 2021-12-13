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
    public class ManufacturerHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Manufacturer_db Add(int manu_id, string name, 
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(name?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a first name.");
                // if (string.IsNullOrEmpty(password?.Trim()))
                //     throw new StatusException(HttpStatusCode.BadRequest, "Please provide a last name.");

                // Generate a new instance
                Manufacturer_db instance = new Manufacturer_db
                    (
                        manu_id, //Guid.NewGuid().ToString(), //This can be ignored is PK in your DB is auto increment
                        name
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO manufacturer (MID, Name) values (@manu_id, @name)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@manu_id", instance.MID },
                            { "@name", instance.Name },
                            // { "@password", instance.Password }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Manufacturer added successfully");
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
        public static List<Manufacturer_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM manufacturer",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Manufacturer_db> instances = new List<Manufacturer_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Manufacturer_db
                            (
                                manu_id: (int)row["MID"],
                                name: row["Name"].ToString() 
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Manufacturer list has been retrieved successfully.");
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
