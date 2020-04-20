using System;
using System.Collections.Generic;

namespace ShareLib.SharepadData
{
    public partial class Text
    {
        public string TextId { get; set; }
        public string TextData { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime AccessTime { get; set; }
    }
}
