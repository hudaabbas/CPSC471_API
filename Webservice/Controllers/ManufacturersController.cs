using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BusinessLibrary.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Webservice.ContextHelpers;
using Webservice.ControllerHelpers;

namespace Webservice.Controllers
{
    [Route("api/manufacturers")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {

        #region Initialization

        /// <summary>
        /// Reference to the hosting environment instance added in the Startup.cs.
        /// </summary>
        private readonly IHostingEnvironment HostingEnvironment;

        /// <summary>
        /// Reference to the app settings helper instance added in the Startup.cs.
        /// </summary>
        private readonly AppSettingsHelper AppSettings;

        /// <summary>
        /// Reference to the database context helper instance added in the Startup.cs.
        /// </summary>
        private readonly DatabaseContextHelper Database;

        /// <summary>
        /// Constructor called by the service provider.
        /// Using injection to get the arguments.
        /// </summary>
        public ManufacturersController(IHostingEnvironment hostingEnvironment, AppSettingsHelper appSettings,
            DatabaseContextHelper database)
        {
            HostingEnvironment = hostingEnvironment;
            AppSettings = appSettings;
            Database = database;
        }

        #endregion


        // Gets collection.
        [HttpGet]
        [Route("GetManufacturers")]
        public ResponseMessage GetManufacturers()
        {
            var response = ManufacturerHelper.GetCollection(
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }

        // Adds a new instance.
        [HttpPost]
        [Route("AddManufacturer")]
        public ResponseMessage AddManufacturer([FromBody] JObject data)
        {

            var response = ManufacturerHelper.Add(data,
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }

    }
}