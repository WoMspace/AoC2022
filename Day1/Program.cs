if(args.Length < 1)
{
	Console.WriteLine("Missing required argument\nUsage: day1 <input_file_name>");
	
	return;
}
string fileName = Path.GetFullPath(args[0]);
string contents = File.ReadAllText(fileName);

List<string> elves = contents.Split("\n\n").ToList();

List<int> elfTotals = new();
foreach(string elf in elves)
{
	List<string> items = elf.Split("\n").ToList();
	int          total = 0;
	foreach(var item in items)
	{
		total += int.Parse(item);
	}
	elfTotals.Add(total);
}

elfTotals.Sort();
elfTotals.Reverse();

Console.WriteLine($"Most calories: {elfTotals[0]}");
Console.WriteLine($"Top three elves combined: {elfTotals[0] + elfTotals[1] + elfTotals[2]}");
