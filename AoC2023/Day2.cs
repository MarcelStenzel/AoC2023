using AoC2023;

class Day2 : AdventDays
{
    const int MAXRED = 12;
    const int MAXGREEN = 13;
    const int MAXBLUE = 14;
    public void DayTask()
    {
        int sumPart1 = 0;
        int sumPart2 = 0;
        StreamReader reader = new(@"..\..\..\input\Day2.txt");
        string line = reader.ReadLine()!;
        while (line != null) {
            string[] lineParts = line.Split(' ');
            /*  
                Example line: Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
                linePart[0] contains only the string "Game" and is of no interest.
            */
            bool validGame = true;
            int minRedNeeded = 1;
            int minGreenNeeded = 1;
            int minBlueNeeded = 1;
            int gameID = int.Parse(lineParts[1].Substring(0, lineParts[1].Length - 1));

            for (int i = 2; i < lineParts.Length; i += 2) {
                int maxOfColour = 0;
                int num = int.Parse(lineParts[i]);
                string colour = lineParts[i + 1];
                if (colour[colour.Length - 1] == ',' || colour[colour.Length - 1] == ';') {
                    colour = colour.Substring(0, lineParts[i + 1].Length - 1);
                }
                switch (colour) {
                    case "red":
                        minRedNeeded = Math.Max(num, minRedNeeded);
                        maxOfColour = MAXRED;
                        break;
                    case "green":
                        minGreenNeeded = Math.Max(num, minGreenNeeded);
                        maxOfColour = MAXGREEN;
                        break;
                    case "blue":
                        minBlueNeeded = Math.Max(num, minBlueNeeded);
                        maxOfColour = MAXBLUE;
                        break;
                }
                if (validGame) {
                    validGame = num <= maxOfColour;
                }
            }
            if (validGame) {
                sumPart1 += gameID;
            }
            sumPart2 += minBlueNeeded * minGreenNeeded * minRedNeeded;
            line = reader.ReadLine()!;
        }
        Console.WriteLine($"Sum for part 1: {sumPart1}");
        Console.WriteLine($"Sum for part 2: {sumPart2}");
    }
}