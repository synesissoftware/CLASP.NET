
// Created: 22nd June 2010
// Updated: 4th April 2019

namespace SynesisSoftware.SystemTools.Clasp
{
    /// <summary>
    ///  Delegate describing the program entry point as a function
    ///  taking an array of strings and returning <c>int</c>.
    /// </summary>
    /// <param name="args">
    ///  The arguments passed to the program's <c>Main()</c> entry point.
    /// </param>
    /// <returns>
    ///  The return value to be returned by the enclosing <c>Main()</c>
    ///  entry point.
    /// </returns>
    /// <remarks>
    ///  Due to the limitations of .NET's overload resolution with respect
    ///  to delegates that differ only by return types, CLASP.NET supports
    ///  only two program-main signatures:
    ///  (i) a method taking an array of <c>string</c> and returning
    ///   <c>int</c>; and
    ///  (ii) a method taking an array of <c>string</c> and having a
    ///   return type of <c>void</c>.
    ///  The first is considered the "proper" form.
    /// </remarks>
    public delegate int ToolMain(Arguments args);

    /// <summary>
    ///  Delegate describing the program entry point as a function
    ///  having no parameters and returning <c>void</c>.
    /// </summary>
    /// <param name="args">
    ///  The arguments passed to the program's <c>Main()</c> entry point.
    /// </param>
    /// <remarks>
    ///  Due to the limitations of .NET's overload resolution with respect
    ///  to delegates that differ only by return types, CLASP.NET supports
    ///  only two program-main signatures:
    ///  (i) a method taking an array of <c>string</c> and returning
    ///   <c>int</c>; and
    ///  (ii) a method taking an array of <c>string</c> and having a
    ///   return type of <c>void</c>.
    ///  The first is considered the "proper" form.
    /// </remarks>
    public delegate void ToolMainVA(Arguments args);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="boundArgs"></param>
    /// <param name="args"></param>
    /// <returns>
    /// </returns>
    /// <example>
    /// </example>
    public delegate int ToolMainWithBoundArguments<T>(T boundArgs, Arguments args) where T : new();
}
