using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthScript
{
    public class HearthException : Exception
    {
        protected string mMessage;
        public HearthException(string message) { 
            this.mMessage = message;
        }

        public void LogError() {
            Console.WriteLine(ErrorInfo());
        }

        protected virtual string ErrorInfo() {
            return string.Empty;
        }
    }

    public class HearthAssemblyException : HearthException {

        public HearthAssemblyException(string message) : base (message) { 
            
        }

        protected override string ErrorInfo()
        {
            string errorInfo = "Assembly Error : " + "This path : " + mMessage + " not found";
            return errorInfo;
        }
    }

    public class HearthModuleException : HearthException
    {
        public HearthModuleException(string message)
            : base(message)
        { 
            
        }

        protected override string ErrorInfo()
        {
            string errorInfo = "Module Error : " + mMessage + " is null!";
            return errorInfo;
        }
    }

    public class HearthTypeException : HearthException
    {
        public HearthTypeException(string message)
            : base(message)
        {
            
        }

        protected override string ErrorInfo()
        {
            string errorInfo = "Type Error : " + mMessage + " is not existed!";
            return errorInfo;
        }
    }

    public class HearthMethodException : HearthException
    {
        public HearthMethodException(string message)
            : base(message)
        { 
            
        }

        protected override string ErrorInfo()
        {
            string errorInfo = "Method Error : " + mMessage + " is not existed!";
            return errorInfo;
        }
    }
}
