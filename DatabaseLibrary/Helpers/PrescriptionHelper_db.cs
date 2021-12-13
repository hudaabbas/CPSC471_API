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
    public class PrescriptionHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Prescription_db Add(int healthcareNo, int medDIN, int doctorId, string pharmLocation, string pharmName,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(pharmLocation?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a pharmacy location.");
                if (string.IsNullOrEmpty(pharmName?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a pharmacy name.");

                // Generate a new instance
                Prescription_db instance = new Prescription_db
                    (
                        healthcareNo, medDIN, doctorId, pharmLocation, pharmName
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO prescription (Healthcare_No, Medication_DIN, MedID, Pharm_location, Pharm_name) values (@healthcareNo, @din, @doctorId, @location, @name)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@healthcareNo", instance.HealthcareNo },
                            { "@din", instance.MedDIN },
                            { "@doctorId", instance.DoctorId },
                            { "@location", instance.PharmLocation },
                            {"@name", instance.PharmName}
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Prescription added successfully");
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
        public static List<Prescription_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM prescription",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Prescription_db> instances = new List<Prescription_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Prescription_db
                            (
                                healthcareNo: (int)row["Healthcare_No"],
                                medDIN: (int)row["Medication_DIN"],
                                doctorId: (int)row["MedID"],
                                pharmLocation: row["Pharm_location"].ToString(),
                                pharmName: row["Pharm_name"].ToString()
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Prescription list has been retrieved successfully.");
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
