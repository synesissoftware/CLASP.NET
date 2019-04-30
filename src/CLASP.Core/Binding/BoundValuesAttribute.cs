
// Created: 19th June 2017
// Updated: 30th April 2019

namespace SynesisSoftware.SystemTools.Clasp.Binding
{
    using System;

    /// <summary>
    ///  Attribute applied to a field that is loaded from a set of values
    /// </summary>
    /// <remarks>
    ///  The bound field must be of a type that inherits from
    ///  <see cref="System.Collections.Generic.ICollection{T}"/>&lt;<see cref="System.String"/>&gt;.
    ///  <para/>
    ///  Note also that, if the bound field is <c>null</c> then it will
    ///  be assigned to be a default-constructed instance of its collection
    ///  type, if concrete; if abstract then it will be assigned to be an
    ///  instance of
    ///  <see cref="System.Collections.Generic.List{T}"/>&lt;<see cref="System.String"/>&gt;
    /// </remarks>
    /// <example>
    /// </example>
    public class BoundValuesAttribute
        : Attribute
    {
        #region fields

        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance
        /// </summary>
        /// <remarks>
        ///  The <see cref="Clasp.Binding.BoundValuesAttribute.Minimum"/> value is set to 1.
        ///  The <see cref="Clasp.Binding.BoundValuesAttribute.Maximum"/> value is set to <c>int.MaxValue</c>
        /// </remarks>
        public BoundValuesAttribute()
        {
            this.Minimum    =   1;
            this.Maximum    =   int.MaxValue;
        }
        #endregion

        #region properties

        /// <summary>
        ///  The base index from which to look
        /// </summary>
        public int Base { get; set; }

        /// <summary>
        ///  The minimum number of values to obtain
        /// </summary>
        public int Minimum { get; set; }

        /// <summary>
        ///  The maximum number of values to obtain
        /// </summary>
        public int Maximum { get; set; }
        #endregion
    }
}
