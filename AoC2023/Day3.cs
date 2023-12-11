using AoC2023;
using System;

class Day3 : AdventDays
{
    string? beforeLine;
    string? currentLine;
    string? nextLine;

    public void DayTask()
    {
        StreamReader reader = new(@"..\..\..\input\Day3.txt");
        currentLine = reader.ReadLine()!;
        nextLine = reader.ReadLine()!;
        int sum = 0;

        do {
            for (int i = 0; i < currentLine.Length; i++) {
                int numLen = 0;
                int number = currentLine[i] >= '0' && currentLine[i] <= '9' ? CheckNum(i, out numLen) : -1;
                if (number > 0) {
                    Console.WriteLine($"number: {number}");
                    sum += number;
                }
                if (numLen > 0) {
                    i += numLen - 1;
                }
            }

            beforeLine = currentLine;
            currentLine = nextLine;
            nextLine = reader.ReadLine()!;
        } while (currentLine != null);
        Console.WriteLine(sum);
    }

    private int CheckNum(int index, out int numLen) {
        if (string.IsNullOrEmpty(currentLine)) {
            throw new InvalidOperationException();
        }

        bool validNum = false;
        int num = (int)char.GetNumericValue(currentLine[index]);
        numLen = 1;

        /* Check column before first digit */
        if (index != 0) {
            validNum = CheckColumn(index - 1);
        }

        int iterator = 0; 
        bool isDigit = true;
        int digit;

        do {
            if (!validNum) {
                /* Check above the digit */
                if (!string.IsNullOrEmpty(beforeLine) && IsSymbol(beforeLine[index + iterator])) {
                    validNum = true;
                }

                /* Check below the digit */
                if (!string.IsNullOrEmpty(nextLine) && IsSymbol(nextLine[index + iterator])) {
                    validNum = true;
                }
            }

            iterator++;
            if (index  + iterator < currentLine.Length && (digit = (int)char.GetNumericValue(currentLine[index + iterator])) >= 0) {
                num *= 10;
                num += digit;
                numLen++;
            } else {
                isDigit = false;
            }

        } while (isDigit);

        /* Check column after last digit */
        if (!validNum && index + iterator < currentLine.Length) {
            validNum = CheckColumn(index + iterator);
        }

        return validNum ? num : -1;
    }

    private bool CheckColumn(int index) {
        if (!string.IsNullOrEmpty(beforeLine) && IsSymbol(beforeLine[index])) return true;
        if (!string.IsNullOrEmpty(currentLine) && IsSymbol(currentLine[index])) return true;
        if (!string.IsNullOrEmpty(nextLine) && IsSymbol(nextLine[index])) return true;
        return false;
    }

    private static bool IsSymbol(char c) {
        return !(c >= '0' && c <= '9') && c != '.';
    }
}