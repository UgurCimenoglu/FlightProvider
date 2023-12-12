using System;
using System.Collections.Generic;
using System.Text;

namespace FlightProject.Core.Utilities.Results
{
    public interface IResult
    {
        string Message { get; }
        bool Success { get; }
    }
}
