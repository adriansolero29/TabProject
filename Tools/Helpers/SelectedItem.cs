using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    public class SelectedItem
    {
        public static bool IsItemValid(Guid? itemId)
        {
            if (itemId == null)
                return false;
            else
                return true;

        }
    }
}
