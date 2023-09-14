using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTasks.Tasks
{
    public class StringParsing
    {
        public async Task<List<(string value, bool valid)>> IsEmailValid(List<string> values)
        {
            List<(string value, bool valid)> result = new List<(string value, bool valid)>();

            foreach (var value in values)
            {
                bool isValid = true;

                if (string.IsNullOrWhiteSpace(value) || value.Length > 200)
                {
                    isValid = false;
                }
                else if (value.Contains('!') || value.Contains('$') || value.Contains('£') ||
                         value.Contains('^') || value.Contains('&') || value.Contains('*') ||
                         value.Contains('(') || value.Contains(')') || value.Contains('='))
                {
                    isValid = false;
                }
                else if (!value.Contains('@') || value.Any(char.IsUpper))
                {
                    isValid = false;
                }
                else if (!value.Contains('.') || value.IndexOf('.') == 0)
                {
                    isValid = false;
                }
                else if (value.Contains(' '))
                {
                    isValid = false;
                }

                result.Add((value, isValid));
            }

            return result;
        }
    }
}
