using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace TestTask.Api.Models.Errors
{
    public abstract class BaseApiException:Exception
    {
        public HttpStatusCode HttpCode { get; protected set; }

        public BaseApiException(HttpStatusCode httpCode, string message) : this(httpCode, message, null)
        {

        }

        public BaseApiException(HttpStatusCode httpCode, string message, Exception innerException) : base(message, innerException)
        {
            HttpCode = httpCode;
        }
    }

    public sealed class ValidationException : BaseApiException
    {
        public FieldErrorDto[] ValidationErrors { get; private set; } = new FieldErrorDto[] { };

        private string _validationSummary;

        public string ValidationSummary
        {
            get
            {
                return _validationSummary ?? string.Empty;
            }
            set
            {
                _validationSummary = value;
            }
        }

        public ValidationException(string field, string message, string summary = null) : base(HttpStatusCode.BadRequest, "errors in request.")
        {
            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            ValidationErrors = new[] { new FieldErrorDto { Field = field, ErrorsMessages = new[] { message } } };
            ValidationSummary = summary;
        }

        public ValidationException(IEnumerable<FieldErrorDto> errors, string summary = null) : base(HttpStatusCode.BadRequest, "errors in request.")
        {
            ValidationErrors = errors.ToArray() ?? throw new ArgumentNullException(nameof(errors));
            ValidationSummary = summary;
        }

        public ValidationException(string summary) : base(HttpStatusCode.BadRequest, "errors in request.")
        {
            ValidationSummary = summary ?? throw new ArgumentNullException(nameof(summary));
        }
    }

    public class NotFoundException : BaseApiException
    {
        public NotFoundException() : base(HttpStatusCode.NotFound, "Requested object not found.")
        {

        }
        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message)
        {

        }
    }
}
