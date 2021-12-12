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
    public class PatientHelper
    {

        #region Converters

        /// <summary>
        /// Converts database models to a business logic object.
        /// </summary>
        public static BusinessLibrary.Models.Patient Convert(Patient_db instance)
        {
            if (instance == null)
                return null;
            return new BusinessLibrary.Models.Patient (instance.HealthcareNo, instance.Name, instance.PhoneNo, instance.DoctorId, instance.Password);
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
            int healthcareNo = (data.ContainsKey("healthcareNo")) ? data.GetValue("healthcareNo").Value<int>() : 0;
            string name = (data.ContainsKey("name")) ? data.GetValue("name").Value<string>() : null;
            int phoneNo = (data.ContainsKey("phoneNo")) ? (int)data.GetValue("phoneNo").Value<Int64>() : 0;
            int doctorId = (data.ContainsKey("doctorId")) ? data.GetValue("doctorId").Value<int>() : 0;
            string password = (data.ContainsKey("password")) ? data.GetValue("password").Value<string>() : null;

            // Add instance to database
            var dbInstance = DatabaseLibrary.Helpers.PatientHelper_db.Add(healthcareNo, name, phoneNo, doctorId, password,
                context, out StatusResponse statusResponse);

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while adding a new patient.";

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
        /// Gets list of patients.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage GetCollection(
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Get instances from database
            var dbInstances = DatabaseLibrary.Helpers.PatientHelper_db.GetCollection(
                context, out StatusResponse statusResponse);

            // Convert to business logic objects
            var instances = dbInstances?.Select(x => Convert(x)).ToList();

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the patients";

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
