using System;
using System.Collections.Generic;
using System.Text;

namespace ChatUI.Helpers
{
    class Paginator
    {
        public const int TOTAL_NUM_ITEMS = 52;
        public const int ITEMS_PER_PAGE = 10;
        public const int ITEMS_REMAINING = TOTAL_NUM_ITEMS % ITEMS_PER_PAGE;
        public const int LAST_PAGE = TOTAL_NUM_ITEMS / ITEMS_PER_PAGE;
    
    }
}
