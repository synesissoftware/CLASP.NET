
// Created: 22nd June 2010
// Updated: 5th April 2019

namespace Clasp
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
    ///  Delegate describing the program entry point as a function taking an
    ///  instance of <typeparamref name="T"/> and an instance of
    ///  <see cref="Clasp.Arguments"/>, and returning <c>int</c>
    /// </summary>
    /// <typeparam name="T">
    ///  The type of the bound structure (or class)
    /// </typeparam>
    /// <param name="boundArgs">
    ///  The instance of <typeparamref name="T"/> bound to the command-line
    ///  arguments (as represented by <paramref name="args"/>)
    /// </param>
    /// <param name="args">
    ///  The instance of <see cref="Clasp.Arguments"/> obtained from parsing
    ///  the command-line
    /// </param>
    /// <returns>
    ///  The return value to be returned by the enclosing <c>Main()</c>
    ///  entry point.
    /// </returns>
    /// <example>
    ///  T.B.C. For now, look at the example <b>Example.BoundValues.cat</b>
    ///  in the distribution
    /// </example>
    public delegate int ToolMainWithBoundArguments<T>(T boundArgs, Arguments args) where T : new();
}
