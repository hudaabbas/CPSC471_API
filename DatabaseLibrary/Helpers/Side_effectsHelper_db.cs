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
    public class Side_effectsHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Side_effects_db Add(int dIN, string side_effect,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(side_effect?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a first name.");

                // Generate a new instance
                Side_effects_db instance = new Side_effects_db
                    (
                        dIN, //Guid.NewGuid().ToString(), //This can be ignored is PK in your DB is auto increment
                        side_effect
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO Side_effects (DIN, Side_effects) values (@1, @2)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@1", instance.DIN },
                            { "@2", instance.Side_effect },
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Side effects added successfully");
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
        public static List<Side_effects_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM Side_effects",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Side_effects_db> instances = new List<Side_effects_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Side_effects_db
                            (
                                dIN: (int)row["DIN"],
                                side_effect: row["Side_effects"].ToString()
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Side effects list has been retrieved successfully.");
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