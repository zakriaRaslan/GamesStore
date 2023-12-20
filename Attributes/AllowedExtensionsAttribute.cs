﻿namespace GamesMvc.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string _allowedExtensions;

        public AllowedExtensionsAttribute(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var filePath = Path.GetExtension(file.FileName);
                var isAllowed = _allowedExtensions.Split(",").Contains(filePath, StringComparer.OrdinalIgnoreCase);

                if (!isAllowed)
                {
                    return new ValidationResult($"Only {_allowedExtensions} Is Allowed");
                }
            }
            return ValidationResult.Success;

        }

    }
}
