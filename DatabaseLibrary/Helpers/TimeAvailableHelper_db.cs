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
    public class TimeAvailableHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static TimeAvailable_db Add(int doctorId, DateTime time,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
               
                // Generate a new instance
                TimeAvailable_db instance = new TimeAvailable_db
                    (
                        doctorId, time
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO Time_available (Time_available, MedID) values (@time, @doctorId)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@time", instance.Time },                    
                            { "@doctorId", instance.DoctorId },

                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Time available added successfully");
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
        public static List<TimeAvailable_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM Time_available",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<TimeAvailable_db> instances = new List<TimeAvailable_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new TimeAvailable_db
                            ( 
                                doctorId: (int)row["MedID"],
                                time: (DateTime)row["Time_available"]
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Time available has been retrieved successfully.");
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
