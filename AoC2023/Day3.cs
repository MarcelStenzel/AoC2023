using AoC2023;
using System;
using System.Globalization;

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
        long gearsum = 0;

        do {
            for (int i = 0; i < currentLine.Length; i++) {
                if (currentLine[i] == '*') {
                    /* Calculate gear ratio for part 2 */
                    long ratio = CalculateGearRatio(i);
                    Console.WriteLine(ratio);
                    if (ratio > 0) {
                        gearsum += ratio;
                    }
                    continue;
                }
                int numLen = 0;
                int number = IsDigit(currentLine[i]) ? CheckNum(i, out numLen) : -1;
                if (number > 0) {
                    //Console.WriteLine($"number: {number}");
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
        Console.WriteLine($"Sum of valid numbers: {sum}");
        Console.WriteLine($"Sum of gear ratios: {gearsum}");
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

    private static bool IsDigit(char c) {
        return (c >= '0' && c <= '9');
    }

    private static bool IsSymbol(char c) {
        return !IsDigit(c) && c != '.';
    }

    private long CalculateGearRatio(int index) {
        if (string.IsNullOrEmpty(currentLine)) {
            throw new InvalidOperationException();
        }
        const int NUMBERS_NEEDED = 2;
        long ratio = 1;
        int numbersFound = 0;

        /* Check above */
        if (!string.IsNullOrEmpty(beforeLine)) {
            if (IsDigit(beforeLine[index])) {
                ratio *= BuildNumber(ref beforeLine, index);
                numbersFound++;
            } else {
            /* Diagoanlly left and right only needs to be checked if directly above was no number. If directly
               above was a number there cannot be a seperate number diagonally */
                if (index > 0 && IsDigit(beforeLine[index - 1])) {
                    ratio *= BuildNumber(ref beforeLine, index - 1);
                    numbersFound++;
                }
                if (index < beforeLine.Length - 1 && IsDigit(beforeLine[index + 1])) {
                    ratio *= BuildNumber(ref beforeLine, index + 1);
                    numbersFound++;
                }
            }    
        }

        /* Check left */
        if (index > 0 && IsDigit(currentLine[index - 1])) {
            if (numbersFound < NUMBERS_NEEDED) {
                ratio *= BuildNumber(ref currentLine, index - 1);
                numbersFound++;
            } else {
                return -1;
            }
        }

        /* Check right */
        if (index < currentLine.Length - 1 && IsDigit(currentLine[index + 1])) {
            if (numbersFound < NUMBERS_NEEDED) {
                ratio *= BuildNumber(ref currentLine, index + 1);
                numbersFound++;
            } else {
                return -1;
            }
        }

        /* Check below */
        if (!string.IsNullOrEmpty(nextLine)) {
            if (IsDigit(nextLine[index])) {
                if (numbersFound < NUMBERS_NEEDED) {
                    ratio *= BuildNumber(ref nextLine, index);
                    numbersFound++;
                } else {
                    return -1;
                }
                
            } else {
            /* Diagoanlly left and right only needs to be checked if directly above was no number. If directly
               above was a number there cannot be a seperate number diagonally */
                if (index > 0 && IsDigit(nextLine[index - 1])) {
                    if (numbersFound < NUMBERS_NEEDED) {
                        ratio *= BuildNumber(ref nextLine, index - 1);
                        numbersFound++;
                    } else {
                        return -1;
                    }
                }
                if (index < nextLine.Length - 1 && IsDigit(nextLine[index + 1])) {
                    if (numbersFound < NUMBERS_NEEDED) {
                        ratio *= BuildNumber(ref nextLine, index + 1);
                    } else {
                        return -1;
                    }
                }
            }    
        }

        if (numbersFound == NUMBERS_NEEDED) {
            return ratio;
        }
        return -2;
    }

    private int BuildNumber(ref string line, int index) {
        int number = 0;
        int steps = 0;
        
        /* Find most significant digit */
        while (index + steps - 1 >= 0 && IsDigit(line[index + steps - 1])) {
            steps--;
        }

        while (index + steps < line.Length && IsDigit(line[index + steps])) {
            number *= 10;
            number += (int)char.GetNumericValue(line[index + steps]);
            steps++;
        }

        return number;
    }
}