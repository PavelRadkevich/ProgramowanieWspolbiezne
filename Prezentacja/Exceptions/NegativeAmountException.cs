﻿using System;

namespace Prezentacja.Exceptions
{
    [Serializable]
    public class NegativeAmountException : Exception
    {
        public NegativeAmountException() { }
        public NegativeAmountException(string message) : base(message) { }
        public NegativeAmountException(string message, Exception inner) : base(message, inner) { }
        protected NegativeAmountException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
