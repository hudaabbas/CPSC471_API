using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLibrary.Models
{
    public class TimeAvailable
    {

        #region Contructors

        /// <summary>
        /// Default constructor. 
        /// Need for serialization purposes.
        /// </summary>
        public TimeAvailable()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public TimeAvailable(int doctorId, DateTime time)
        {
            DoctorId = doctorId;
            Time = time;
        }

        /// <summary>
        /// Clone/Copy constructor.
        /// </summary>
        /// <param name="instance">The object to clone from.</param>
        public TimeAvailable(TimeAvailable instance)
            : this(instance.DoctorId, instance.Time)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Name of the patient.
        /// </summary>
        [JsonProperty(PropertyName = "doctorId")]
        public int DoctorId { get; set; }

        /// <summary>
        /// Password of the patient.
        /// </summary>
        [JsonProperty(PropertyName = "time")]
        public DateTime Time { get; set; }

        #endregion

    }
}
