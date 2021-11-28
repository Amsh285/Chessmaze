using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessmaze
{
    public static class Assert
    {
        public static void NotNull<T>(T value, string name) where T : class
        {
            That(value != null, $"{name} cannot be null.");
        }

        public static void That(bool condition, string message)
        {
            if (!condition)
                throw new AssertionException(message);
        }
    }
}
