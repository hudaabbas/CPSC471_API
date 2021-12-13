using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary.Models
{
    public class Caregiver_db
    {

        #region Contructors

        /// <summary>
        /// Default constructor. 
        /// </summary>
        public Caregiver_db()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Caregiver_db(int caregiverId, int healthcareNo, string name, int phoneNo)
        {
            HealthcareNo = healthcareNo;
            Name = name;
            PhoneNo = phoneNo;
            CaregiverId = caregiverId;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Name of the caregiver.
        /// </summary>
        public int CaregiverId { get; set; }
        
        /// <summary>
        /// Id generated by our system upon creation of a new instance.
        /// </summary>
        public int HealthcareNo { get; set; }

        /// <summary>
        /// Name of the patient.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Phone no of the caregiver.
        /// </summary>
        public int PhoneNo { get; set; }



        #endregion

    }
}
