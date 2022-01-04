using System;

namespace Common.Exceptions
{
    public class DataBaseException : Exception
    {
        #region Fields

        private readonly string _message;

        #endregion //Fields

        #region Properies

        public override string Message => _message;


        #endregion //Properties

        public DataBaseException(string message)
        {
            _message = message;
        }

    }
}
