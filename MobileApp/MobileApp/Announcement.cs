using System;
using System.Collections.Generic;
using System.Text;
using Plugin.CloudFirestore.Attributes;

namespace MobileApp
{
    public class Announcement
    {
        [MapTo("Title")]
        public string Title { get; set; }
        [MapTo("Content")]
        public string Content { get; set; }
        public string Img { get; set; }

    }
}
