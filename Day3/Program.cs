using System.Text;

string   fileName  = args[0];
string[] rucksacks = File.ReadAllLines(fileName);

// Part 1
int      total     = 0;
foreach(string rucksack in rucksacks)
{
	int    compartmentSize = rucksack.Length / 2;
	string compartment1    = rucksack[0..compartmentSize];
	string compartment2    = rucksack[compartmentSize..];
	if(compartment1.Length != compartment2.Length) { throw new Exception("Rucksack size not even!"); }

	char duplicate = GetDuplicateItem(compartment1, compartment2);
	total += GetPriority(duplicate);
}
Console.WriteLine($"Total priorities: {total}");

// Part 2
int badgeTotal = 0;
for(int i = 0; i < rucksacks.Length; i += 3)
{
	string rucksack1 = rucksacks[i];
	string rucksack2 = rucksacks[i + 1];
	string rucksack3 = rucksacks[i + 2];

	char groupBadge = GetTruplicateItem(rucksack1, rucksack2, rucksack3);
	badgeTotal += GetPriority(groupBadge);
}
Console.WriteLine($"Total group priorities: {badgeTotal}");

int GetPriority(char item)
{
	if(char.IsUpper(item))
	{
		return Encoding.ASCII.GetBytes(new [] {item})[0] - 38;
	}
	else
	{
		return Encoding.ASCII.GetBytes(new[] { item })[0] - 96;
	}

	// Not case sensitive
	// return Encoding.ASCII.GetBytes(new []{item})[0] & 31;
}

char GetDuplicateItem(string compartment1, string compartment2)
{
	foreach(char item in compartment1)
	{
		if(compartment2.Contains(item))
		{
			return item;
		}
	}

	throw new Exception("Faulty input. No duplicate items found.");
}

char GetTruplicateItem(string rucksack1, string rucksack2, string rucksack3)
{
	foreach(char item in rucksack1)
	{
		if(rucksack2.Contains(item))
		{
			if(rucksack3.Contains(item))
			{
				return item;
			}
		}
	}

	throw new Exception("Faulty input. No duplicate items found.");
}