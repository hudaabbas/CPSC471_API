using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary.Models
{
    public class Clinic_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Clinic_db()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Clinic_db(int clinicNo, string address, string name)
        {
            ClinicNo = clinicNo;
            Address = address;
            Name = name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Id generated by our system upon creation of a new instance.
        /// </summary>
        public int ClinicNo { get; set; }

        /// <summary>
        /// First name used of the student.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Last name used of the student.
        /// </summary>
        public string Address { get; set; }

        #endregion

    }
}
