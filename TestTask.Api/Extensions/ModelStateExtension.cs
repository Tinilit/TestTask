using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using TestTask.Api.Models.Errors;
using System.Text.Json;

namespace TestTask.Api.Extensions
{
    public static class ModelStateExtension
    {
        /// <summary>
        /// Raises a special validation exception based on  modelState. This ex will be processed with a special pipeline and converted to a dto.
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="summary"></param>
        /// <returns></returns>
        public static void ThrowValidationExceptionIfNotValid(this ModelStateDictionary modelState, string summary = null)
        {
            if (modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }
            if (!modelState.IsValid)
            {
                var errors = modelState.Where(e => e.Value.Errors.Any())
                      .Select(x => new FieldErrorDto(x.Key, BuildErrorMessages(x).Distinct().ToArray())).ToArray();
                throw new ValidationException(errors, summary);
            }
        }

        private static IEnumerable<string> BuildErrorMessages(KeyValuePair<string, ModelStateEntry> arg)
        {
            foreach (var error in arg.Value.Errors)
            {
                if (error.Exception is JsonException je)
                {
                    yield return "Wrong format.";
                }
                else if (error.Exception is Exception JsonSerializationException)
                {
                    yield return "Field " + arg.Key + " has wrong format.";
                }
                else
                {
                    yield return error.ErrorMessage;
                }
            }
        }
    }
}
