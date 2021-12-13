using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary.Models
{
    public class TimeAvailable_db
    {

        #region Contructors

        /// <summary>
        /// Default constructor. 
        /// Need for serialization purposes.
        /// </summary>
        public TimeAvailable_db()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public TimeAvailable_db(int doctorId, DateTime time)
        {
            DoctorId = doctorId;
            Time = time;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Name of the patient.
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        /// Password of the patient.
        /// </summary>
        public DateTime Time { get; set; }

        #endregion


    }
}