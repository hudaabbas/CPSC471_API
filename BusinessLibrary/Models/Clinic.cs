using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLibrary.Models
{
    public class Clinic
    {

        #region Contructors

        /// <summary>
        /// Default constructor. 
        /// Need for serialization purposes.
        /// </summary>
        public Clinic()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Clinic(int clinicNo, string address, string name)
        {
            ClinicNo = clinicNo;
            Address = address;
            Name = name;
        }

        /// <summary>
        /// Clone/Copy constructor.
        /// </summary>
        /// <param name="instance">The object to clone from.</param>
        public Clinic(Clinic instance)
            : this(instance.ClinicNo, instance.Address, instance.Name)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Id generated by our system upon creation of a new instance.
        /// </summary>
        [JsonProperty(PropertyName = "ClinicNo")]
        public int ClinicNo { get; set; }

        /// <summary>
        /// First name of the student.
        /// </summary>
        [JsonProperty(PropertyName = "Address")]
        public string Address
        {
            get;
            set;
        }

        /// <summary>
        /// Last name of the student.
        /// </summary>
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        /*/// <summary>
        /// FUll name of the student.
        /// </summary>
        [JsonProperty(PropertyName = "password")]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }*/

        #endregion

        #region Methods

        /// <summary>
        /// Overrides the .ToString method.
        /// </summary>

        #endregion

    }
}
