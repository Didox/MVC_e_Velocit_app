using System;

namespace Didox.Business
{
    public class TradeVisionError : Exception
    {
        public TradeVisionError(string message)
        {
            this._message = message;
        }

        private string _message;

        public override string Message
        {
            get { return this._message; }
        }
    }

    public class TradeVisionError404 : Exception
    {
        public TradeVisionError404(string message)
        {
            this._message = message;
        }

        private string _message;

        public override string Message
        {
            get { return this._message; }
        }
    }

    public class TradeVisionError403 : Exception
    {
        public TradeVisionError403(string message)
        {
            this._message = message;
        }

        private string _message;

        public override string Message
        {
            get { return this._message; }
        }
    }

    public class TradeVisionValidationError : Exception
    {
        public TradeVisionValidationError(string message)
        {
            this._message = message;
        }

        private string _message;

        public override string Message
        {
            get { return this._message; }
        }
    }

    public class FormatError
    {
        public static string FormatMessageForJAlert(Exception err, bool showTrace)
        {
            string erroMessage = err.Message;
            return fMessageErro(erroMessage, showTrace, err.StackTrace);
        }

        private static string fMessageErro(string erroMessage, bool showTrace, string stackTrace)
        {
            if (showTrace) erroMessage += ", " + stackTrace;
            erroMessage = erroMessage.Replace("\n", " ");
            erroMessage = erroMessage.Replace("'", "");
            erroMessage = erroMessage.Replace("\r", " ");
            return erroMessage;
        }

        public static string FormatMessageForJAlert(Exception err)
        {
            return FormatMessageForJAlert(err, false);
        }

        public static string FormatMessageForJAlert(string err)
        {
            return fMessageErro(err, false, "");
        }
    }
}
