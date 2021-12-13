﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary.Models
{
    public class Pickup_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Pickup_db()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Pickup_db(int healthcare_no, int din, string location,string name)
        {
            HealthcareNo= healthcare_no;
            DIN= din;
            Location= location;
            Name= name;
            // Password = password;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Id generated by our system upon creation of a new instance.
        /// </summary>
        public int HealthcareNo{ get; set; }

        /// <summary>
        /// First name used of the student.
        /// </summary>
        public int DIN { get; set; }

        /// <summary>
        /// First name used of the student.
        /// </summary>
        public string Location { get; set; }


        /// <summary>
        /// First name used of the student.
        /// </summary>
        public string Name { get; set; }
        // /// <summary>
        // /// Last name used of the student.
        // /// </summary>
        // public string Password { get; set; }

        #endregion

    }
}
