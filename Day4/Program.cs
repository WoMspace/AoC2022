// using Helper.Input;

string   fileName = args[0];
string[] file     = File.ReadAllLines(fileName);

// Part 1
int count = 0;
foreach(string line in file)
{
	var pair = ParsePair(line);
	if((pair.Elf1.Start.Value <= pair.Elf2.Start.Value && pair.Elf1.End.Value >= pair.Elf2.End.Value) 
	   || (pair.Elf2.Start.Value <= pair.Elf1.Start.Value && pair.Elf2.End.Value >= pair.Elf1.End.Value))
	{
		count++;
	}
}
Console.WriteLine($"{count} pairs entirely overlap.");

// Part 2
count = 0;
foreach(string line in file)
{
	var (elf1, elf2) = ParsePair(line);
	// if((elf1.Start.Value <= elf2.Start.Value && elf1.End.Value >= elf2.End.Value)
	//    || (elf2.Start.Value <= elf1.Start.Value && elf2.End.Value >= elf1.End.Value)
	//    || (elf1.Start.Value >= elf2.End.Value && elf1.Start.Value <= elf2.Start.Value)
	//    || (elf1.End.Value <= elf2.End.Value && elf1.End.Value >= elf2.Start.Value))
	// {
	// 	count++;
	// }

	for(int i = elf1.Start.Value; i <= elf1.End.Value; i++)
	{
		for(int j = elf2.Start.Value; j <= elf2.End.Value; j++)
		{
			if(i == j)
			{
				count++;
				goto escape;				
			}
		}
	}
	escape: ;
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