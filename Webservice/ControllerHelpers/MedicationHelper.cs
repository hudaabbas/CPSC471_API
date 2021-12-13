using BusinessLibrary.Models;
using DatabaseLibrary.Core;
using DatabaseLibrary.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Webservice.ControllerHelpers
{
    public class MedicationHelper
    {

        #region Converters

        /// <summary>
        /// Converts database models to a business logic object.
        /// </summary>
        public static BusinessLibrary.Models.Medication Convert(Medication_db instance)
        {
            if (instance == null)
                return null;
            return new BusinessLibrary.Models.Medication(instance.DIN, instance.RefillStatus, instance.ExpDate, instance.Dosage, instance.Time, instance.LeftoverAmount);
        }

        #endregion

        /// <summary>
        /// Signs up a student.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Add(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            int dIN = (data.ContainsKey("dIN")) ? data.GetValue("dIN").Value<int>() : 0;
            bool refillStatus = (data.ContainsKey("refillStatus")) ? data.GetValue("refillStatus").Value<bool>() : false;
            string expDate = (data.ContainsKey("expDate")) ? data.GetValue("expDate").Value<string>() : null;
            int dosage= (data.ContainsKey("dosage")) ? data.GetValue("dosage").Value<int>() : 0;
            int time = (data.ContainsKey("time")) ? data.GetValue("time").Value<int>() : 0;
            int leftoverAmount = (data.ContainsKey("leftoverAmount")) ? data.GetValue("leftoverAmount").Value<int>() : 0;

            // Add instance to database
            var dbInstance = DatabaseLibrary.Helpers.MedicationHelper_db.Add(dIN, refillStatus, expDate,
            dosage, time,leftoverAmount,  context, out StatusResponse statusResponse);

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while adding a new student.";

            // Return response
            var response = new ResponseMessage
                (
                    dbInstance != null,
                    statusResponse.Message,
                    Convert(dbInstance)
                );
            statusCode = statusResponse.StatusCode;
            return response;
        }

        /// <summary>
        /// Gets list of students.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage GetCollection(
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Get instances from database
            var dbInstances = DatabaseLibrary.Helpers.MedicationHelper_db.GetCollection(
                context, out StatusResponse statusResponse);

            // Convert to business logic objects
            var instances = dbInstances?.Select(x => Convert(x)).ToList();

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the medications";

            // Return response
            var response = new ResponseMessage
                (
                    instances != null,
                    statusResponse.Message,
                    instances
                );
            statusCode = statusResponse.StatusCode;
            return response;
        }

    }
}
