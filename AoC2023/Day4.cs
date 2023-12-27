using AoC2023;

class Day4 : AdventDays
{
    Dictionary<int, int>? cards;
    public void DayTask()
    {
        cards = [];
        int totalValue = 0;
        StreamReader reader = new(@"..\..\..\input\Day4.txt");
        string line = reader.ReadLine()!;
        while (line != null) {
            string[] lineParts = line.Split(' ');
            int cardNumberIndex = 1;
            for (int i = 1; i < lineParts.Length; i++) {
                if (!string.IsNullOrEmpty(lineParts[i])) {
                    cardNumberIndex = i;
                    break;
                }
            }
            int currentCard = int.Parse(lineParts[cardNumberIndex].Split(':')[0]);
            lineParts = lineParts[(cardNumberIndex + 1)..];
            
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
                Part2Fill(lineCounter + 1, currentCard);
            } else {
                Part2Fill(0, currentCard);
            }
            totalValue += lineValue;
            line = reader.ReadLine()!;
        }

        Console.WriteLine($"Total value for Part 1: {totalValue}");
        Part2Eval();
    }

    private void Part2Fill(int matchingNumbers, int currentCard) {
        if (cards == null) {
            throw new InvalidOperationException();
        }
        if (!cards.TryAdd(currentCard, 1)) {
            cards[currentCard]++;
        }

        for (int i = currentCard + 1; i <= currentCard + matchingNumbers; i++) {
            if (cards.ContainsKey(i)) {
                cards[i] += cards[currentCard];
            } else {
                cards.Add(i, cards[currentCard]);
            }
        }
    }

    private void Part2Eval() {
        if (cards == null) {
            throw new InvalidOperationException();
        }
        int sum = 0;
        foreach(var kv in cards) {
            sum += kv.Value;
        }
        Console.WriteLine("Total cards for part 2: " + sum);
    }
    
}