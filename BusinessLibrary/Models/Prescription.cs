using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLibrary.Models
{
    public class Prescription
    {

        #region Contructors

        /// <summary>
        /// Default constructor. 
        /// Need for serialization purposes.
        /// </summary>
        public Prescription()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Prescription(int healthcareNo, int medDIN, int doctorId, string pharmLocation, string pharmName)
        {
            HealthcareNo = healthcareNo;
            MedDIN = medDIN;
            DoctorId = doctorId;
            PharmLocation = pharmLocation ;
            PharmName = pharmName;
        }

        /// <summary>
        /// Clone/Copy constructor.
        /// </summary>
        /// <param name="instance">The object to clone from.</param>
        public Prescription(Prescription instance)
            : this(instance.HealthcareNo, instance.MedDIN, instance.DoctorId, instance.PharmLocation, instance.PharmName)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Id generated by our system upon creation of a new instance.
        /// </summary>
        [JsonProperty(PropertyName = "healthcareNo")]
        public int HealthcareNo { get; set; }

        /// <summary>
        /// Name of the patient.
        /// </summary>
        [JsonProperty(PropertyName = "medDIN")]
        public int MedDIN { get; set; }


        /// <summary>
        /// Name of the patient.
        /// </summary>
        [JsonProperty(PropertyName = "doctorId")]
        public int DoctorId { get; set; }

        /// <summary>
        /// Password of the patient.
        /// </summary>
        [JsonProperty(PropertyName = "pharmacyName")]
        public string PharmName{ get; set; }

        /// <summary>
        /// Password of the patient.
        /// </summary>
        [JsonProperty(PropertyName = "pharmacyLocation")]
        public string PharmLocation { get; set; }

        #endregion
    }
}
