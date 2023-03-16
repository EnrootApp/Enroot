using System.Threading.Tasks;
using ErrorOr;

namespace Enroot.Domain.Common.Errors;

public static partial class Errors
{
    public static class Assignment
    {
        private const string _code = "Assignment";
        public static Error NotFound =>
            Error.NotFound(_code, "NotFound");
        public static Error NotOnReview =>
            Error.Conflict(_code, "NotOnReview");
        public static Error HasStarted =>
            Error.Conflict(_code, "HasStarted");
        public static Error HasCompleted =>
           Error.Conflict(_code, "HasCompleted");
    }
}
