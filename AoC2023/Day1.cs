namespace AoC2023 {
    class Day1 : AdventDays
    {
        public void DayTask()
        {
            int sum = 0;

            try {
                StreamReader reader = new(@"..\..\..\input\Day1.txt");
                string line = reader.ReadLine()!;
                while (line != null) {
                    int number = 0;
                    bool firstDigitFound = false;
                    bool lastDigitFound = false;
                    for (int i = 0; i < line.Length; i++) {
                        if (!firstDigitFound) {
                            int firstDigit = (int)char.GetNumericValue(line[i]);
                            if (firstDigit >= 0 && firstDigit <= 9) {
                                number += 10 * firstDigit;
                                firstDigitFound = true;
                            }
                        }
                        if (!lastDigitFound) {
                            int lastDigit = (int)char.GetNumericValue(line[line.Length - i - 1]);
                            if (lastDigit >= 0 && lastDigit <= 9) {
                                number += lastDigit;
                                lastDigitFound = true;
                            }
                        }
                        if (lastDigitFound && firstDigitFound) {
                            Console.WriteLine("Number = " + number);
                            sum += number;
                            break;
                        }
                    }
                    line = reader.ReadLine()!;
                }
                Console.WriteLine("Sum: " + sum);
                reader.Close();
            }
            catch(Exception e) {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}