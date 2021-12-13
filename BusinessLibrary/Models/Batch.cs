﻿using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLibrary.Models
{
    public class Batch
    {

        #region Contructors

        /// <summary>
        /// Default constructor. 
        /// Need for serialization purposes.
        /// </summary>
        public Batch()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Batch(int b_id,int mid, string exp_date, int med_din)
        {
            Batch_id= b_id;
            MID = mid;
            ExpirationDate= exp_date;
            Medication_DIN= med_din;
        }

        /// <summary>
        /// Clone/Copy constructor.
        /// </summary>
        /// <param name="instance">The object to clone from.</param>
        public Batch(Batch instance)
            : this(instance.Batch_id, instance.MID, instance.ExpirationDate, instance.Medication_DIN)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Id generated by our system upon creation of a new instance.
        /// </summary>
        [JsonProperty(PropertyName = "b_id")]
        public int Batch_id { get; set; }

        /// <summary>
        /// First name of the student.
        /// </summary>
        [JsonProperty(PropertyName = "mid")]
        public int MID 
        { get;
           /* {
                return string.Format("{0}", Name);
            } */
            set; 
        }

        /// <summary>
        /// Last name of the student.
        /// </summary>
        [JsonProperty(PropertyName = "exp_date")]
        public string ExpirationDate{ get; set; }

         /// <summary>
        /// Last name of the student.
        /// </summary>
        [JsonProperty(PropertyName = "med_din")]
        public int Medication_DIN{ get; set; }
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
        // public override string ToString()
        // {
        //     return Name;
        // }

        #endregion

    }
}