using System;
using System.Collections.Generic;
using System.Text;

namespace BatsBadmintonFixtures.ValidationRules
{
    public class MatchValuesRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        private T _otherValue;
        public T OtherValue { get { return _otherValue; } set { _otherValue = value; } }

        public bool Check(T value)
        {
            if (_otherValue == null || value == null)
                return false;

            var str1 = value as string;
            var str2 = _otherValue as string;

            return (str1 == str2);
        }

        
        
    }
}
