using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace BatsBadmintonFixtures
{
    public class ValidatableObject<T> : ObservableObject, IValidation
    {
        public ValidatableObject()
        {
            _validations = new List<IValidationRule<T>>();
            _isValid = true;
            _errors = new List<string>();
        }

        private readonly List<IValidationRule<T>> _validations;
        public List<IValidationRule<T>> Validations => _validations;

        private bool _isValid;
        public bool IsValid
        {
            get { return _isValid; }
            set
            {
                SetProperty(ref _isValid, value);
            }
        }

        private List<string> _errors;

        public List<string> Errors
        {
            get { return _errors; }
            set
            {
                SetProperty(ref _errors, value);
            }
        }

        private T _value;
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                SetProperty(ref _value, value);
            }
        }



        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = _validations.Where(v => v.Check(Value) == false)
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }

    }
}
