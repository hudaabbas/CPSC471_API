﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary.Models
{
    public class Batch_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Batch_db()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Batch_db(int batch_id, int mid, string exp_date, int med_din )
        {
            Batch_id = batch_id;
            MID = mid;
            ExpirationDate=exp_date;
            Medication_DIN= med_din;
            // Password = password;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Id generated by our system upon creation of a new instance.
        /// </summary>
        public int Batch_id { get; set; }

        /// <summary>
        /// First name used of the student.
        /// </summary>
        public int MID { get; set; }

         /// <summary>
        /// First name used of the student.
        /// </summary>
        public string ExpirationDate { get; set; }

         /// <summary>
        /// First name used of the student.
        /// </summary>
        public int Medication_DIN { get; set; }

        // /// <summary>
        // /// Last name used of the student.
        // /// </summary>
        // public string Password { get; set; }

        #endregion

    }
}
