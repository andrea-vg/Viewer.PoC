using System;

namespace Viewer.PoC.Core.Exceptions
{
    public class ShapeFileReadException(string message) : Exception(message)
    {
    }
}