using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace DomainLayer.ValidationAttributes
{
    public class MinCollectionSizeAttribute :  ValidationAttribute
    {
        private readonly int _minSize;

        public MinCollectionSizeAttribute(int minSize, string errorMessage = null) 
            : base(errorMessage)
        {
            _minSize = minSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not ICollection collection)
            {
                return new ValidationResult($"The {validationContext.DisplayName} field is not a collection.");
            }

            if (collection.Count < _minSize)
            {
                var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }

        public override string FormatErrorMessage(string name)
        {
            if (string.IsNullOrEmpty(ErrorMessage))
            {
                return $"The {name} field must contain at least {_minSize} items.";
            }

            return string.Format(ErrorMessage, name, _minSize);
        }
    }
}
