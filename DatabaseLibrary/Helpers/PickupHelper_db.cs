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
    public class PickupHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Pickup_db Add(int healthcare_no, int din, string location, string name,
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
                Pickup_db instance = new Pickup_db
                    (
                      healthcare_no,
                      din,
                      location, 
                      name
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO pick_up (HealthcareNo, DIN, Location, Name) values (@healthcare_no, @din, @location, @name)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@healthcare_no", instance.HealthcareNo},
                            { "@din", instance.DIN },
                            { "@location", instance.Location },
                            { "@name", instance.Name}

                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Pickup added successfully");
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
        public static List<Pickup_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM pick_up",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Pickup_db> instances = new List<Pickup_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Pickup_db
                            (
                                healthcare_no: (int)row["HealthcareNo"],
                                din: (int)row["DIN"], 
                                location: row["Location"].ToString(), 
                                name: row["Name"].ToString() 
                                // password: row["Password"].ToString()
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Pick_up list has been retrieved successfully.");
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
