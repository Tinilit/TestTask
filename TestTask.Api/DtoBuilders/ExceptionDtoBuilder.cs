using System;
using System.Linq;
using TestTask.Api.Models.Errors;

namespace TestTask.Api.DtoBuilders
{
    public static class ExceptionDtoBuilder
    {
        public static ErrorDto Map(Exception ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException(nameof(ex));
            }
            ErrorDto dto = new ErrorDto();

            switch (ex)
            {
                case ValidationException validation:
                    dto = new ValidationErrorDto()
                    {
                        ValidationSummary = validation.ValidationSummary,
                        Message = validation.Message,
                        ValidationErrors = validation.ValidationErrors.Select(x => new FieldErrorDto(FixFieldName(x.Field), x.ErrorsMessages)).ToArray()
                    };
                    break;
                case NotFoundException notfound:
                    dto = new ErrorDto
                    {
                        Code = ReadableErrorCodes.NotFound,
                        Message = notfound.Message
                    };
                    break;
                default:
                    {
                        dto = new ErrorDto()
                        {
                            Code = ReadableErrorCodes.ServerFatal,
                            Message = ex.Message
                        };
                    }
                    break;
            }
            return dto;
        }

        private static string FixFieldName(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return s;

            if (s.Contains("."))
                return string.Join(".", s.Split('.').Select(i => char.ToLower(i[0]) + i.Substring(1)));

            return char.ToLower(s[0]) + s.Substring(1);
        }
    }
}
