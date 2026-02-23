using System;

namespace FlexibleInventorySystem.Exceptions
{
    /// <summary>
    /// TODO: Implement custom exception for inventory-specific errors
    /// </summary>
    public class InventoryException : Exception
    {
        // TODO: Add these constructors:
        public string ErrorCode { get; } = string.Empty;
        public InventoryException() { }
        // - Default constructor
        // - Constructor with message
        public InventoryException(string message) : base(message) { }
        // - Constructor with message and inner exception
        public InventoryException(string message, Exception innerException) : base(message, innerException) { }
        // - Constructor with message and error code
        
        public InventoryException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        // TODO: Add ErrorCode property

        // TODO: Override Message property to include error code
        public override string Message => ErrorCode==null ? base.Message : $"Error Code: {ErrorCode} - {base.Message}";
    }
}
