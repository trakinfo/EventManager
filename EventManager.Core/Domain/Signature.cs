using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Core.Domain
{
    class Signature
    {
		public string Owner { get; set; }
		public string User { get; set; }
		public string HostIP { get; set; }
		public DateTime Version { get; set; }
    }
}
