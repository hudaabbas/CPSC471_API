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
    public class CaregiverHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Caregiver_db Add(int caregiverId, int healthcareNo, string name, int phoneNo, 
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(name?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a name.");
                //if (string.IsNullOrEmpty(caregiverId?.Trim()))
                //    throw new StatusException(HttpStatusCode.BadRequest, "Please provide an id.");

                // Generate a new instance
                Caregiver_db instance = new Caregiver_db
                    (
                        caregiverId, healthcareNo, name, phoneNo
                    );

                // Add to database
                Console.WriteLine(instance.CaregiverId.ToString());
                Console.WriteLine(instance.HealthcareNo.ToString());
                Console.WriteLine(instance.PhoneNo.ToString());
                Console.WriteLine( instance.Name );
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO Caregiver (Caregiver_id, Healthcare_No, PhoneNo, Name) values (@1, @2, @3, @4)",
                        parameters: new Dictionary<string, object>()
                        {
                                                
                            { "@1", instance.CaregiverId },
                            { "@2", instance.HealthcareNo },
                            { "@3", instance.PhoneNo },
                            { "@4", instance.Name }
                            
                            
                        },
                        message: out string message
                    );
                Console.WriteLine( rowsAffected);
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Caregiver added successfully");
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
        public static List<Caregiver_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM caregiver",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Caregiver_db> instances = new List<Caregiver_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Caregiver_db
                            (
                                caregiverId: (int)row["Caregiver_id"],
                                healthcareNo: (int)row["Healthcare_No"],
                                name: row["Name"].ToString(), 
                                phoneNo: (int)row["PhoneNo"]
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Caregiver list has been retrieved successfully.");
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
