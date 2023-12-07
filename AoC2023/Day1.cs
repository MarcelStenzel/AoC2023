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

                    int firstNum, firstIndex, lastNum, lastIndex;
                    FindWrittenNums(line, out firstNum, out firstIndex, out lastNum, out lastIndex);

                    int frontLoopLength = firstIndex != -1 ? firstIndex : line.Length;
                    for (int i = 0; i < frontLoopLength; i++) {
                        int firstDigit = (int)char.GetNumericValue(line[i]);
                        if (firstDigit >= 0 && firstDigit <= 9) {
                           firstNum = firstDigit;
                           firstIndex = i;
                           break;
                        }
                    }

                    int backLoopLength = lastIndex != -1 ? lastIndex : 0;
                    for (int i = line.Length; i > backLoopLength; i--) {
                        int lastDigit = (int)char.GetNumericValue(line[i - 1]);
                        if (lastDigit >= 0 && lastDigit <= 9) {
                            lastNum = lastDigit;
                            lastIndex = i;
                            break;
                        }
                    }

                    number = 10 * firstNum + lastNum;
                    Console.WriteLine("Number = " + number);
                    sum += number;
                    line = reader.ReadLine()!;
                }
                Console.WriteLine("Sum: " + sum);
                reader.Close();
            }
            catch(Exception e) {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        enum WrittenNums {
            one = 1, two, three, four, five, six, seven, eight, nine
        }

        private void FindWrittenNums(string line, out int firstNum, out int firstIndex, out int lastNum, out int lastIndex)
        {
            firstNum = -1;
            firstIndex = -1;
            lastNum = -1;
            lastIndex = -1;

            foreach(string writtenNum in Enum.GetNames(typeof(WrittenNums))) {
                int frontIndex = line.IndexOf(writtenNum);
                if (frontIndex > -1) {
                    if (frontIndex < firstIndex || firstIndex == -1) {
                        firstIndex = frontIndex;
                        firstNum = (int)Enum.Parse(typeof(WrittenNums), writtenNum);
                    }
                }

                int endIndex = line.LastIndexOf(writtenNum);
                if (endIndex > -1) {
                    if (endIndex > lastIndex || lastIndex == -1) {
                        lastIndex = endIndex;
                        lastNum = (int)Enum.Parse(typeof(WrittenNums), writtenNum);
                    }
                }
            }
        }
    }
}