using System;

namespace Modules.Core.Exceptions
{
    public class WrongPrefabConfigurationException : Exception
    {
        public WrongPrefabConfigurationException(string message)
            : base(message)
        {
        }
    }
}