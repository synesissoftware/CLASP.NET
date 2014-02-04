
// Created: 
// Updated: 3rd February 2014

namespace SynesisSoftware.SystemTools.Clasp.Util
{
	using SynesisSoftware.SystemTools.Clasp.Exceptions;
	using SynesisSoftware.SystemTools.Clasp.Internal;

	using System;
	//using System.Collections.Generic;
	using System.Diagnostics;
	using System.IO;
	//using System.Linq;
	//using System.Text;

	public static class ClaspUtil
	{
		public static bool ParseBool(string s)
		{
			Debug.Assert(null != s);

			switch(s.ToLower())
			{
				case "yes":
				case "true":
					return true;
				case "no":
				case "false":
					return false;
				default:
					return bool.Parse(s);
			}
		}

		public static bool TryParseBool(string s, out bool value)
		{
			Debug.Assert(null != s);

			switch(s)
			{
				case "yes":
				case "Yes":
					value = true;
					return true;
				case "no":
				case "No":
					value = false;
					return true;
				default:
					return bool.TryParse(s, out value);
			}
		}

		public static void ShowUsageAndQuit(Arguments args, int exitCode)
		{
			TextWriter o = Console.Out;

			if(null != args.Aliases)
			{
				foreach(Alias alias in args.Aliases)
				{
					switch(alias.Type)
					{
						case ArgumentType.Flag:
							if(!String.IsNullOrEmpty(alias.GivenName))
							{
								o.WriteLine("  {0}", alias.GivenName);
							}
							if(!String.IsNullOrEmpty(alias.ResolvedName))
							{
								o.WriteLine("  {0}", alias.ResolvedName);
							}
							o.WriteLine("    {0}", alias.Description);
							o.WriteLine();
							break;
						case ArgumentType.Option:
							if(!String.IsNullOrEmpty(alias.GivenName))
							{
								o.WriteLine("  {0}", alias.GivenName);
							}
							if(!String.IsNullOrEmpty(alias.ResolvedName))
							{
								o.WriteLine("  {0}", alias.ResolvedName);
							}
							o.WriteLine("    {0}", alias.Description);
							o.WriteLine();
							break;
					}
				}
			}

			Environment.Exit(exitCode);
		}

		#region Flags and Options Operations
		public static bool FlagSpecified(Arguments args, string flagName)
		{
			foreach(Argument arg in args.Flags)
			{
				if(flagName == arg.ResolvedName)
				{
					arg.Used = true;
					return true;
				}
			}
			return false;
		}

		public static short CheckOption(Arguments args, string optionName, short defaultValue)
		{
			return SearchOptionOrDefault(args, optionName, defaultValue, (arg) => short.Parse(arg.Value));
		}
		public static int CheckOption(Arguments args, string optionName, int defaultValue)
		{
			return SearchOptionOrDefault(args, optionName, defaultValue, (arg) => int.Parse(arg.Value));
		}
		public static long CheckOption(Arguments args, string optionName, long defaultValue)
		{
			return SearchOptionOrDefault(args, optionName, defaultValue, (arg) => long.Parse(arg.Value));
		}
		public static bool CheckOption(Arguments args, string optionName, bool defaultValue)
		{
			return SearchOptionOrDefault(args, optionName, defaultValue, (arg) => ParseBool(arg.Value));
		}
		public static string CheckOption(Arguments args, string optionName, string defaultValue)
		{
			return SearchOptionOrDefault(args, optionName, defaultValue, (arg) => arg.Value);
		}


		//public static short RequireOption(Arguments args, string optionName)
		//{
		//    return SearchOption(args, optionName, (arg) => short.Parse(arg.Value));
		//}
		//public static int RequireOption(Arguments args, string optionName)
		//{
		//    return SearchOption(args, optionName, (arg) => int.Parse(arg.Value));
		//}
		//public static long RequireOption(Arguments args, string optionName)
		//{
		//    return SearchOption(args, optionName, (arg) => long.Parse(arg.Value));
		//}
		//public static string RequireOption(Arguments args, string optionName)
		//{
		//    return SearchOption(args, optionName, (arg) => arg.Value);
		//}

		public static void RequireOption(Arguments args, string optionName, out short value)
		{
			value = SearchOption<short>(args, optionName, (arg) => short.Parse(arg.Value));
		}
		public static void RequireOption(Arguments args, string optionName, out int value)
		{
			value = SearchOption<int>(args, optionName, (arg) => int.Parse(arg.Value));
		}
		public static void RequireOption(Arguments args, string optionName, out long value)
		{
			value = SearchOption<long>(args, optionName, (arg) => long.Parse(arg.Value));
		}
		public static void RequireOption(Arguments args, string optionName, out string value)
		{
			value = SearchOption<string>(args, optionName, (arg) => arg.Value);
		}
		#endregion

		#region Implementation
		delegate T Translate<T>(Argument arg);

		private static T SearchOption<T>(Arguments args, string optionName, Translate<T> f)
		{
			foreach(Argument arg in args.Options)
			{
				if(optionName == arg.ResolvedName)
				{
					arg.Used = true;

					return f(arg);
				}
			}

			throw new MissingOptionException("option not specified", optionName);
		}
		private static T SearchOptionOrDefault<T>(Arguments args, string optionName, T defaultValue, Translate<T> f)
		{
			foreach(Argument arg in args.Options)
			{
				if(optionName == arg.ResolvedName)
				{
					arg.Used = true;

					return f(arg);
				}
			}

			return defaultValue;
		}

		public static void VerifyAllOptionsUsed(Arguments args)
		{
			foreach(Argument arg in args.FlagsAndOptions)
			{
				if(!arg.Used)
				{
					throw new UnusedArgumentException("unused argument", arg.ResolvedName);
				}
			}
		}
		#endregion
	}
}
