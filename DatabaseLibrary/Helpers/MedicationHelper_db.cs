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
    public class MedicationHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Medication_db Add(int dIN, bool refillStatus, string expDate, int dosage, int time, int leftoverAmount,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(expDate?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a expiration date.");

                // Generate a new instance
                Medication_db instance = new Medication_db
                    (
                        dIN, //Guid.NewGuid().ToString(), //This can be ignored is PK in your DB is auto increment
                        refillStatus, expDate, dosage, time, leftoverAmount
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO medication (DIN, RefillStatus, ExpDate, Dosage, Time, LeftoverAmount) values (@1, @2, STR_TO_DATE(@3,'%d/%m/%Y %H:%i:%s'), @4, @5, @6)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@1", instance.DIN },
                            { "@2", instance.RefillStatus},
                            { "@3", instance.ExpDate},
                            { "@4", instance.Dosage },
                            { "@5", instance.Time },
                            { "@6", instance.LeftoverAmount }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Medication added successfully");
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
        public static List<Medication_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM medication",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Medication_db> instances = new List<Medication_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Medication_db
                            (
                                dIN: (int)row["DIN"],
                                refillStatus: (bool)row["RefillStatus"],
                                expDate: row["ExpDate"].ToString(),
                                dosage:(int)row["Dosage"],
                                time: (int)row["Time"],
                                leftoverAmount: (int)row["LeftoverAmount"]
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Medication list has been retrieved successfully.");
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
