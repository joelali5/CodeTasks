using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTasks.Tasks
{
    public class FizzBuzz
    {
        public async Task<string> GetFizzBuzz1To100()
        {
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= 100; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    result.AppendLine("FizzBuzz");
                }
                else if (i % 3 == 0)
                {
                    result.AppendLine("Fizz");
                }
                else if (i % 5 == 0)
                {
                    result.AppendLine("Buzz");
                }
                else
                {
                    result.AppendLine(i.ToString());
                }
            }

            return result.ToString();
        }

        // 3 = Fizz, 5 = Buzz, {multiple} = {word}
        public async Task<string> GetFizzBuzzWithCustom1To100(int multiple, string word)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= 100; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    result.AppendLine("FizzBuzz");
                }
                else if (i % 3 == 0)
                {
                    result.AppendLine("Fizz");
                }
                else if (i % 5 == 0)
                {
                    result.AppendLine("Buzz");
                }
                else if (i % multiple == 0)
                {
                    result.AppendLine(word);
                }
                else
                {
                    result.AppendLine(i.ToString());
                }
            }

            return result.ToString();
        }

        public async Task<string> GetFizzBuzzWithAllCustomFrom1ToN(
            List<(int multiple, string word)> words,
            int to
        )
        {
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= to; i++)
            {
                bool isMultiple = false;
                StringBuilder output = new StringBuilder();

                foreach (var (multiple, word) in words)
                {
                    if (i % multiple == 0)
                    {
                        output.Append(word);
                        isMultiple = true;
                    }
                }

                if (!isMultiple)
                {
                    output.Append(i);
                }

                result.AppendLine(output.ToString());
            }
            return result.ToString();
        }
    }
}
