using System;

namespace MercuryLogger
{
    public interface IExceptionHandler
    {
        void Hand(Exception e);
        IExceptionHandler Component { get; set; }
    }
}