using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Api.Models.Errors
{
    /// <summary>
    /// A representation of not found error
    /// </summary>
    public sealed class NotFoundErrorDto : ErrorDto
    {
        public NotFoundErrorDto()
        {
            Code = ReadableErrorCodes.NotFound;
        }
    }
}
