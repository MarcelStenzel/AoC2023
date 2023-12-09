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

        do {
            for (int i = 0; i < currentLine.Length; i++) {
                int numLen = 0;
                int number = currentLine[i] >= '0' && currentLine[i] <= '9' ? CheckNum(i, out numLen) : -1;  //(int)char.GetNumericValue(currentLine[i]);
                if (number < 0) {
                    if (numLen > 0) {
                        i += numLen - 1;
                    }
                    continue;
                }
                // if (number >= 0) {
                //     currentNum *= 10;
                //     currentNum += digit;

                //     //Check if it a number that counts


                // } else {
                //     if (currentNum > 0) {
                //         //Last digit was the last of the current number
                //     }
                //     Console.WriteLine(currentNum);
                //     currentNum = 0;
                // }
            }

            beforeLine = currentLine;
            currentLine = nextLine;
            nextLine = reader.ReadLine()!;
        } while (nextLine != null);
    }

    private int CheckNum(int index, out int numLen) {
        if (string.IsNullOrEmpty(currentLine)) {
            throw new InvalidOperationException();
        }

        bool validNum = false;
        bool firstDigit = true;
        int sum = (int)char.GetNumericValue(currentLine[index]);
        numLen = 1;

        if (index != 0) {
            validNum = IsSymbol(currentLine[index - 1]);
        }




        return validNum ? sum : -1;
    }

    private static bool IsSymbol(char c) {
        return !(c >= '0' && c <= '9') && c != '.';
    }
}