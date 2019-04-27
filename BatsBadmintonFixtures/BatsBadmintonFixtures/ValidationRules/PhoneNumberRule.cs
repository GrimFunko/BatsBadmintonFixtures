using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BatsBadmintonFixtures.ValidationRules
{
    public class PhoneNumberRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
                return true;

            var str = value as string;

            // Allows empty or null because number is not required
            if (String.IsNullOrEmpty(str))
                return true;

            Regex regex = new Regex(@"^\d{5} ?\d{6}$");
            Match match = regex.Match(str);

            return match.Success;

        }
    }
}
