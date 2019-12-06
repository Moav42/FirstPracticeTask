using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace FirstPracticeTask
{
    [Serializable]
    public class MatrixException : ApplicationException
    {
        public MatrixException() { }
        public MatrixException(string message) : base(message) { }
        public MatrixException(string message, Exception inner) : base(message, inner) { }
        protected MatrixException( SerializationInfo info, StreamingContext context) : base(info, context) { }

        // Custom members for our exception.
        public DateTime ErrorTimeStamp { get; set; }
        public string CauseOfError { get; set; }
        public MatrixException(string message, string cause, DateTime time) : base(message)
        {
            CauseOfError = cause;
            ErrorTimeStamp = time;           
        }
    }
    public class MatrixAdditionException : MatrixException
    {
        public MatrixAdditionException(string message) : base(message) { }
    }
    public class MatrixSubtractionException : MatrixException
    {
        public MatrixSubtractionException(string message) : base(message) { }
    }
    public class MatrixMultiplyingException : MatrixException
    {
        public MatrixMultiplyingException(string message) : base(message) { }
    }


}
