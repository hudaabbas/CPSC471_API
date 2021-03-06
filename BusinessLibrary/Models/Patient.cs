using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLibrary.Models
{
    public class Patient
    {

        #region Contructors

        /// <summary>
        /// Default constructor. 
        /// Need for serialization purposes.
        /// </summary>
        public Patient()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Patient(int healthcareNo, string name, int phoneNo, int doctorId, string password)
        {
            HealthcareNo = healthcareNo;
            Name = name;
            PhoneNo = phoneNo;
            DoctorId = doctorId;
            Password = password;
        }

        /// <summary>
        /// Clone/Copy constructor.
        /// </summary>
        /// <param name="instance">The object to clone from.</param>
        public Patient(Patient instance)
            : this(instance.HealthcareNo, instance.Name, instance.PhoneNo, instance.DoctorId, instance.Password)
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
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Name of the patient.
        /// </summary>
        [JsonProperty(PropertyName = "phoneNo")]
        public int PhoneNo { get; set; }

        /// <summary>
        /// Name of the patient.
        /// </summary>
        [JsonProperty(PropertyName = "doctorId")]
        public int DoctorId { get; set; }

        /// <summary>
        /// Password of the patient.
        /// </summary>
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Overrides the .ToString method.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }

        #endregion

    }
}
