using AoC2023;

class Day4 : AdventDays
{
    public void DayTask()
    {
        int totalValue = 0;
        StreamReader reader = new(@"..\..\..\input\Day4.txt");
        string line = reader.ReadLine()!;
        while (line != null) {
            /* The prefix "Card XY:" is of no interest. */
            string[] lineParts = line.Split(':')[1].Split(' ');
            
            List<string> winningNumbers = [];
            bool fillWinningNumbers =  true;
            int lineCounter = -1;

            foreach (string linePart in lineParts) {
                if (string.IsNullOrEmpty(linePart)) {
                    /* This can happen if a number has only one digit. In this case it is
                       aligned with two spaces */
                    continue;
                }
                if (fillWinningNumbers) {
                    if (string.Equals(linePart, "|")) {
                        fillWinningNumbers = false;
                        continue;
                    }
                    winningNumbers.Add(linePart);
                } else if (winningNumbers.Contains(linePart)) {
                    lineCounter++;
                }
            }
            int lineValue = 0;
            if (lineCounter > -1) {
                lineValue = (int)Math.Pow(2, lineCounter);
            }
            Console.WriteLine($"Line value: {lineValue}");
            totalValue += lineValue;
            line = reader.ReadLine()!;
        }

    Console.WriteLine($"Total value: {totalValue}");
    }
    
}