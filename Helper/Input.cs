using System.Diagnostics;

namespace Helper;

public static class Input
{
	static string GetFileName(string[] args)
	{
		if(args.Length == 0)
		{
			Console.WriteLine("Error: Missing required arguments.");
			ShowHelp();
			Environment.Exit(1);
		}
		else if(args.Contains("--help"))
		{
			ShowHelp();
			Environment.Exit(0);
		}
		else if(args.Length == 1)
		{
			if(File.Exists(args[0]))
			{
				return args[0];
			}
			else
			{
				Console.WriteLine("Error: File does not exist.");
				ShowHelp();
				Environment.Exit(1);
			}
		}
		else if(args.Length > 1) {
		    Console.WriteLine("Error: Too many arguments.");
		    ShowHelp();
		    Environment.Exit(1);
		}
		else
		{
			throw new UnreachableException();
		}

		throw new UnreachableException();
	}

	static void ShowHelp()
	{
		string msg = """
Usage: program <data_file>

data_file		The file containing the data from AoC from this puzzle. Can be the test data or the unique data.
""";
		Console.WriteLine(msg);
	}
}