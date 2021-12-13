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
    public class ClinicHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Clinic_db Add(int clinicNo, string address, string name,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(address?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide an address.");
                if (string.IsNullOrEmpty(name?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a name.");
                
                Console.WriteLine(clinicNo);
                Console.WriteLine(address);
                Console.WriteLine(name);
                // Generate a new instance
                Clinic_db instance = new Clinic_db
                    (
                        clinicNo, //Guid.NewGuid().ToString(), //This can be ignored is PK in your DB is auto increment
                        address, name
                    );

                // Add to database
                Console.WriteLine(instance.ClinicNo);
                Console.WriteLine(instance.Address);
                Console.WriteLine(instance.Name);
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO clinic (ClinicNo, Address, Name) values (@1, @2, @3)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@1", instance.ClinicNo},
                            { "@2", instance.Address },
                            { "@3", instance.Name}
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Clinic added successfully");
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
        public static List<Clinic_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM clinic",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Clinic_db> instances = new List<Clinic_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Clinic_db
                            (
                                clinicNo: (int)row["ClinicNo"],
                                address: row["Address"].ToString(),
                                name: row["Name"].ToString()
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Clinic list has been retrieved successfully.");
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
