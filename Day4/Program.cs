// using Helper.Input;

string   fileName = "";
string[] file     = File.ReadAllLines(fileName);

// Part 1
int count = 0;
foreach(string line in file)
{
	var pair = ParsePair(line);
	if(pair.Elf1.Start.Value <= pair.Elf2.Start.Value && pair.Elf1.End.Value >= pair.Elf2.End.Value)
	{
		count++;
	}
	else if(pair.Elf2.Start.Value <= pair.Elf1.Start.Value && pair.Elf2.End.Value >= pair.Elf1.End.Value)
	{
		count++;
	}
}
Console.WriteLine($"{count} pairs entirely overlap.");

// Part 2
count = 0;
foreach(string line in file)
{
	var pair = ParsePair(line);
	if(pair.Elf1.End.Value >= pair.Elf2.Start.Value && pair.Elf1.Start.Value < pair.Elf2.Start.Value)
	{
		count++;
	}
	else if(pair.Elf2.End.Value >= pair.Elf1.Start.Value && pair.Elf2.Start.Value < pair.Elf1.Start.Value)
	{
		count++;
	}
}
Console.WriteLine($"{count} pairs overlap at least slightly.");

Range ParseRange(string elf)
{
	// Parse '2-4' into a range
	string start      = elf.Split('-')[0];
	string end        = elf.Split('-')[1];
	int    startIndex = int.Parse(start);
	int    endIndex   = int.Parse(end);
	return new Range(startIndex, endIndex);
}

(Range Elf1, Range Elf2) ParsePair(string pair)
{
	Range Elf1 = ParseRange(pair.Split(',')[0]);
	Range Elf2 = ParseRange(pair.Split(',')[1]);
	return (Elf1, Elf2);
}