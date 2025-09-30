using System;

namespace Viewer.PoC.Core.Exceptions
{
    public class ShapeParseException : Exception
    {
        public ShapeParseException(string message) : base(message) { }
        public ShapeParseException(string message, Exception inner) : base(message, inner) { }
    }

    public class ShapeTypeNotSupportedException(string type) : Exception($"Shape type '{type}' is not supported.")
    {
    }

    public class ShapeCreationException(string message) : Exception(message)
    {
    }
}