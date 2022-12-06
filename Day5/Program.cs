using System.Text.RegularExpressions;

string sourceFile = args[0];
string file = File.ReadAllText(sourceFile);

string initialState = file.Split("\n\n")[0];
string instructions = file.Split("\n\n")[1];

Piles piles = new(initialState);
piles.Draw();

foreach(string instruction in instructions.Split('\n'))
{
	if(string.IsNullOrEmpty(instruction)) {continue;}
	// Get the "move X" number
	string pattern    = @"(?<=move )\d+";
	Regex  regex      = new(pattern);
	int    moveNumber = int.Parse(regex.Match(instruction).Value);
	pattern = @"(?<=from )\d+";
	regex   = new(pattern);
	int    sourcePile = int.Parse(regex.Match(instruction).Value) - 1;
	pattern    = @"(?<=to )\d+";
	regex   = new(pattern);
	int    destinationPile = int.Parse(regex.Match(instruction).Value) - 1;
	piles.Move(moveNumber, sourcePile, destinationPile);
}

Console.WriteLine("Top crate on each pile: ");
foreach(var stack in piles.piles)
{
	Console.Write(stack.Peek());
}

class Piles
{
	public Stack<char>[] piles;

	// CrateMover 9000
	// public void Move(int quantity, int source, int destination)
	// {
	// 	if(piles[source].Count < quantity) { throw new IndexOutOfRangeException(); }
	// 	for(int i = 0; i < quantity; i++)
	// 	{
	// 		char transit = piles[source].Pop();
	// 		Console.WriteLine($"Moving crate {transit} from stack {source} to {destination}");
	// 		piles[destination].Push(transit);
	// 	}
	// }
	
	// CrateMover 9001
	public void Move(int quantity, int source, int destination)
	{
		Console.WriteLine($"Moving {quantity} crates from {source} to {destination}");
		Stack<char> craneArm = new();
		Console.Write("Grabbing crates: ");
		for(int i = 0; i < quantity; i++)
		{
			char transit = piles[source].Pop();
			craneArm.Push(transit);
			Console.Write($"[{transit}]");
		}
		Console.WriteLine();

		Console.Write("Placing crates: ");
		for(int i = 0; i < quantity; i++)
		{
			char transit = craneArm.Pop();
			piles[destination].Push(transit);
			Console.Write($"[{transit}]");
		}
		Console.WriteLine();
	}

	public Piles(string initialState)
	{
		string[] lines    = initialState.Split('\n');
		string   lastLine = lines.Last();
		lines = lines[0..^1];

		string pattern    = @"\d+\s\Z";
		Regex  regex      = new(pattern);
		string lastNumber = regex.Match(lastLine).Value.TrimEnd();
		int    count      = int.Parse(lastNumber);
		piles = new Stack<char>[count];
		for(int i = 0; i < count; i++)
		{
			piles[i] = new Stack<char>();
		}

		foreach(string line in lines.Reverse())
		{
			for(int i = 0; i < count; i++)
			{
				char crate = line[i * 4 + 1];
				if(Char.IsAsciiLetter(crate))
				{
					Console.WriteLine($"Found crate [{crate}] in pile");
					piles[i].Push(crate);
				}
			}
		}
	}

	public void Draw()
	{
		int maxLength = 0;
		foreach(var stack in piles)
		{
			if(stack.Count > maxLength) { maxLength = stack.Count; }
		}
		Console.WriteLine($"Longest stack is {maxLength} items long");
	}
}