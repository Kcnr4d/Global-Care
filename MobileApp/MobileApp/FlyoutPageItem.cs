using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp
{
    class FlyoutPageItem
    {
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Type TargetPage { get; set; }
        public bool IsLogOut { get; set; }
        public bool IsChat { get; set; }
    }
}
