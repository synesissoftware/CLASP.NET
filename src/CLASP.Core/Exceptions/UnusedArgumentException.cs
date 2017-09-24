
// Created: 23rd June 2010
// Updated: 19th June 2017

namespace SynesisSoftware.SystemTools.Clasp.Exceptions
{
    /// <summary>
    ///  Exception thrown to indicate an unused flag/option.
    /// </summary>
    public class UnusedArgumentException
        : FlagOrOptionArgumentException
    {
        /// <summary>
        ///  Constructs an instance of the exception.
        /// </summary>
        /// <param name="arg">
        ///  The argument that is not used.
        /// </param>
        public UnusedArgumentException(Interfaces.IArgument arg)
            : base(arg, "unused argument", null, null)
        {
        }
    }
}
