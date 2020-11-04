using System;
using System.Security.Cryptography.X509Certificates;


namespace CodingSolution
{
    [Flags]
    public enum Capabilities
    {
        None = 0,
        All = ~0,

        Spin = 1,
        Raise = 2,
        Lower = 4,
        Flatten = 8,
        Expand = 16,
        Drop = 32,
    }
}

