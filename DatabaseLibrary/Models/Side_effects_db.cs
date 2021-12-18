using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary.Models
{
    public class Side_effects_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Side_effects_db()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Side_effects_db(int dIN, string side_effect)
        {
            DIN = dIN;
            Side_effect = side_effect;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Id generated by our system upon creation of a new instance.
        /// </summary>
        public int DIN { get; set; }

        /// <summary>
        /// Name used of the doctor.
        /// </summary>
        public string Side_effect { get; set; }

        // /// <summary>
        // /// Password used of the doctor.
        // /// </summary>
        // public string Password { get; set; }

        #endregion

    }
}