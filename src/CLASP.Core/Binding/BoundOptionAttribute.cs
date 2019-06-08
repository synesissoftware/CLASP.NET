
// Created: 18th June 2017
// Updated: 29th May 2019

namespace Clasp.Binding
{
    using System;

    /// <summary>
    ///  Attribute applied to fields to receive a value from a command-line
    ///  option
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class BoundOptionAttribute
        : BoundFieldAttribute
    {
        #region fields

        private bool                    m_allowEmpty;
        private bool                    m_requirePositive;
        private bool                    m_requireWhole;
        private object                  m_defaultValue;
        private BoundNumberConstraints  m_numberConstraints;
        private NumberTruncate          m_truncationOperation;
        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance of the attribute that matches the given
        ///  option name
        /// </summary>
        /// <param name="optionName">
        ///  The option name
        /// </param>
        public BoundOptionAttribute(string optionName)
            : base(optionName)
        {
        }
        #endregion

        #region properties

        /// <summary>
        ///  Determines whether empty values are allowed
        /// </summary>
        public bool AllowEmpty
        {
            get
            {
                return m_allowEmpty;
            }
            set
            {
                m_allowEmpty = value;
            }
        }

        /// <summary>
        ///  Determines whether negative values are allowed, when parsing
        ///  numeric values; ignored otherwise
        /// </summary>
        /// <remarks>
        ///  Ignored if <see cref="NumberConstraints"/> is not <c>None</c>
        /// </remarks>
        public bool AllowNegative
        {
            get
            {
                if(BoundNumberConstraints.None != m_numberConstraints)
                {
                    if(0 != (BoundNumberConstraints.MustBePositive & m_numberConstraints))
                    {
                        return false;
                    }

                    if(0 != (BoundNumberConstraints.MustBeNonNegative & m_numberConstraints))
                    {
                        return false;
                    }

                    return true;
                }

                return !m_requirePositive;
            }
            set
            {
                m_requirePositive = !value;
            }
        }

        /// <summary>
        ///  Determines whether fractional values are allowed, when parsing
        ///  numeric values; ignored otherwise
        /// </summary>
        /// <remarks>
        ///  Ignored if <see cref="NumberConstraints"/> is not <c>None</c>
        /// </remarks>
        public bool AllowFraction
        {
            get
            {
                if(BoundNumberConstraints.None != m_numberConstraints)
                {
                    if(0 != (BoundNumberConstraints.MustBeIntegral & m_numberConstraints))
                    {
                        return false;
                    }

                    return true;
                }

                if(NumberTruncate.None != m_truncationOperation)
                {
                    return false;
                }

                return !m_requireWhole;
            }
            set
            {
                m_requireWhole = !value;
            }
        }

        /// <summary>
        ///  Specifies numeric constraint (if not <c>None</c>)
        /// </summary>
        public BoundNumberConstraints NumberConstraints
        {
            get
            {
                return m_numberConstraints;
            }
            set
            {
                m_numberConstraints = value;
            }
        }

        /// <summary>
        ///  .
        /// </summary>
        public NumberTruncate NumberTruncate
        {
            get
            {
                return m_truncationOperation;
            }
            set
            {
                m_truncationOperation = value;
            }
        }

        /// <summary>
        ///  [INTERNAL]
        /// </summary>
        internal BoundNumberConstraints EffectiveBoundNumberConstraints
        {
            get
            {
                if(BoundNumberConstraints.None != m_numberConstraints)
                {
                    return m_numberConstraints;
                }

                if(m_requirePositive)
                {
                    return BoundNumberConstraints.MustBePositive;
                }

                return BoundNumberConstraints.None;
            }
        }


        /// <summary>
        ///  The default value to be used
        /// </summary>
        public object DefaultValue
        {
            get
            {
                return m_defaultValue;
            }
            set
            {
                m_defaultValue = value;
            }
        }
        #endregion
    }
}
