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
            /* This function shoulr return the string valur along with a bool indicating if the string is valid.
             * The is considerd valid if the following rules are met:
             * . The string is not null or empty.
             * . The string is not longer than 200 characters.
             * . The string does not contain any of the following characters: !$£^&*()=
             * . The String Contains one @.
             * . The String contains no uppercase letters.
             * . the string contains at least one letter prefixed with an "."
             * . The string contains no spaces.
            */

            return new List<(string value, bool valid)>();
        }
    }
}
