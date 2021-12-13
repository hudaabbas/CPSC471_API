﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary.Models
{
    public class SuppliedTo_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SuppliedTo_db()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public SuppliedTo_db(int batch_id, int mid, string location, string name)
        {
            Batch_id= batch_id;
            MID= mid;
            Location= location;
            Name=name;
            // Password = password;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Id generated by our system upon creation of a new instance.
        /// </summary>
        public int Batch_id{ get; set; }

        /// <summary>
        /// First name used of the student.
        /// </summary>
        public int MID{ get; set; }

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
