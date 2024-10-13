using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    public class Generator
    {
        public static string GuidString()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
