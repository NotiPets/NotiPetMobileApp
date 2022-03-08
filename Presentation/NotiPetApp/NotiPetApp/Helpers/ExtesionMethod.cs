using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Results;

namespace NotiPetApp.Helpers
{

        public static class ValidationFailureExtensions
        {
            // This is an example that gives you all messages,
            // no matter if they are warnings or errors.
            // Provide your own implementation that fits your need.
            public static string GetMessageForProperty(this IList<ValidationFailure> errors, string propertyName)
            {
                return errors
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .Aggregate(new StringBuilder(), (builder, s) => builder.AppendLine(s), builder => builder.ToString());
            }
            public static bool HasMessageForProperty(this IList<ValidationFailure> errors, string propertyName)
            {
                return errors
                    .Any(e => e.PropertyName == propertyName);

            }
        }
    
}