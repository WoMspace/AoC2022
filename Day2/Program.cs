string fileName = args[0];
List<string> lines = new();
lines.AddRange(File.ReadAllLines(fileName).ToList());

int score = 0;
foreach(string line in lines)
{
	char opponentChoice = line[0]; // A in 'A Y'
	char outcome    = line[2]; // Y in 'A Y'
// 	char yourChoice     = line[2]; // Y in 'A Y'
	char yourChoice = CalculateMove(opponentChoice, outcome);
	score += CalculateScore(opponentChoice, yourChoice);
	Console.WriteLine($"{GetName(opponentChoice)} vs {GetName(yourChoice)}: You {OutcomeName(Outcome(opponentChoice, yourChoice))}");
}
Console.WriteLine($"Your score was {score}");

int CalculateScore(char opponentChoice, char yourChoice)
{
	int yourChoiceScore = yourChoice switch
	{
		'X' => 1,
		'A' => 1,
		'Y' => 2,
		'B' => 2,
		'Z' => 3,
		'C' => 3
	};
	int outcomeScore = Outcome(opponentChoice, yourChoice) switch
	{
		0 => 0, // loss
		1 => 3, // draw
		2 => 6  // win
	};
	return yourChoiceScore + outcomeScore;
}

int Outcome(char opponentChoice, char yourChoice)
{
	// 0 for lose, 1 for draw, 2 for win
	int[,] matrix = new[,]
	{  // A  B  C
		{ 1, 0, 2 }, // X
		{ 2, 1, 0 }, // Y
		{ 0, 2, 1 }  // Z
	};
	int opponentChoiceIndex = opponentChoice switch
	{
		'A' => 0,
		'B' => 1,
		'C' => 2
	};
	int yourChoiceIndex = yourChoice switch
	{
		'A' => 0,
		'X' => 0,
		'B' => 1,
		'Y' => 1,
		'C' => 2,
		'Z' => 2
	};

	return matrix[yourChoiceIndex, opponentChoiceIndex];
}

string GetName(char choice)
{
	return choice switch
	{
		'A' => "Rock",
		'X' => "Rock",
		'B' => "Paper",
		'Y' => "Paper",
		'C' => "Scissors",
		'Z' => "Scissors",
	};
}

string OutcomeName(int outcome)
{
	return outcome switch
	{
		0 => "Lose",
		1 => "Draw",
		2 => "Win"
	};
}

char CalculateMove(char opponentChoice, char outcome)
{
	int outcomeIndex = outcome switch
	{
		'X' => 0, // Lose
		'Y' => 1, // Draw
		'Z' => 2, // Win
	};

	int opponentIndex = opponentChoice switch
	{
		'A' => 0,
		'B' => 1,
		'C' => 2
	};
	
	char[,] matrix = new[,]
	{  // A    B    C => opponent Rock, Paper, Scissors
		{'C', 'A', 'B'}, // X => Lose
		{'A', 'B', 'C'}, // Y => Draw
		{'B', 'C', 'A'}  // Z => Win
	};

	return matrix[outcomeIndex, opponentIndex];
}