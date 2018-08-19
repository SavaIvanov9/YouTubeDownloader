namespace YD.Common.Exceptions
{
    public class CommandNotImplementedException : CustomException
    {
        public CommandNotImplementedException(string commandName) : base($@"Command ""{commandName}"" is not implemented!")
        {
        }
    }
}
