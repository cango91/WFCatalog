using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowCatalog.Domain.Exceptions
{
    class FileNameException : Exception
    {
        public FileNameException() { }
        public FileNameException(string msg ) : base(msg) { }
        public FileNameException(string msg, Exception ex) : base(msg, ex) { }
    }
}
