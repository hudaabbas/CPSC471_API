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
    public class PharmacyHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Pharmacy_db Add(string name,  string location, int clinic_no,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(name?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide pharmacy name.");
                if (string.IsNullOrEmpty(location?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide location name.");

                // Generate a new instance
                Pharmacy_db instance = new Pharmacy_db
                    (
                        name, //Guid.NewGuid().ToString(), //This can be ignored is PK in your DB is auto increment
                        location, 
                        clinic_no
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO pharmacy (Name, Location, ClinicNo) values (@name, @location, @clinic_no)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@name", instance.Name},
                            { "@location", instance.Location },
                            { "@clinic_no", instance.ClinicNo }

                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Pharmacy added successfully");
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
        public static List<Pharmacy_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM pharmacy",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Pharmacy_db> instances = new List<Pharmacy_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Pharmacy_db
                            (
                                name: row["Name"].ToString(),
                                location: row["Location"].ToString(), 
                                clinic_no: (int)row["ClinicNo"] 
                     
                                // password: row["Password"].ToString()
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Pharmacy list has been retrieved successfully.");
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
