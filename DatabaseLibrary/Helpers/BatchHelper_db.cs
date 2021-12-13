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
    public class BatchHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Batch_db Add(int batch_id, int mid, string exp_date, int med_din,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                // if (string.IsNullOrEmpty(name?.Trim()))
                //     throw new StatusException(HttpStatusCode.BadRequest, "Please provide a first name.");
                // if (string.IsNullOrEmpty(password?.Trim()))
                //     throw new StatusException(HttpStatusCode.BadRequest, "Please provide a last name.");

                // Generate a new instance
                Batch_db instance = new Batch_db
                    (
                        batch_id, //Guid.NewGuid().ToString(), //This can be ignored is PK in your DB is auto increment
                        mid, 
                        exp_date, med_din
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO batch (Batch_id, MID, ExpirationDate, Medication_DIN) values (@batch_id, @mid, STR_TO_DATE(@exp_Date,'%d/%m/%Y %H:%i:%s'), @med_din)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@batch_id", instance.Batch_id },
                            { "@mid", instance.MID },
                            { "@exp_Date", instance.ExpirationDate },
                            { "@med_DIN", instance.Medication_DIN}

                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Batch added successfully");
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
        public static List<Batch_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM batch",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Batch_db> instances = new List<Batch_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Batch_db
                            (
                                batch_id: (int)row["Batch_id"],
                                mid: (int) row["MID"], 
                                exp_date: row["ExpirationDate"].ToString(), 
                                med_din: (int) row["Medication_DIN"] 
                                // password: row["Password"].ToString()
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Batch list has been retrieved successfully.");
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
