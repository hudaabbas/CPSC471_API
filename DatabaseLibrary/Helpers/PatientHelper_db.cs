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
    public class PatientHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Patient_db Add(int healthcareNo, string name, int phoneNo, int doctorId, string password,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(name?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a name.");
                if (string.IsNullOrEmpty(password?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a password.");

                // Generate a new instance
                Patient_db instance = new Patient_db
                    (
                        healthcareNo, name, phoneNo, doctorId, password
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO patient (HealthcareNo, Name, PhoneNo, MedID, Password) values (@healthcareNo, @name, @phoneNo, @doctorId, @password)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@healthcareNo", instance.HealthcareNo },
                            { "@name", instance.Name },
                            { "@phoneNo", instance.PhoneNo },
                            { "@doctorId", instance.DoctorId },
                            { "@password", instance.Password }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Patient added successfully");
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
        public static List<Patient_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM patient",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Patient_db> instances = new List<Patient_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Patient_db
                            (
                                healthcareNo: (int)row["HealthcareNo"],
                                name: row["Name"].ToString(), 
                                phoneNo: (int)row["PhoneNo"],
                                doctorId: (int)row["MedID"],
                                password: row["Password"].ToString()
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Patient list has been retrieved successfully.");
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
