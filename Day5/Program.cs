string sourceFile = args[0];
string file = File.ReadAllText(sourceFile);

string initialState = file.Split("\n\n")[0];
string instructions = file.Split("\n\n")[1];

class Piles
{
	public Stack<char>[] piles;

	public void Move(int quantity, int source, int destination)
	{
		for(int i = 0; i < quantity; i++)
		{
			char transit = piles[source].Pop();
			piles[destination].Push(transit);
		}
	}

	public Piles(string initialstate)
	{
		string[] lines = initialstate.Split('\n');
		
	}
}