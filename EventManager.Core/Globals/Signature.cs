using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Core.Globals
{
    public class Signature
    {
		public string User { get; protected set; }
		public string HostIP { get; protected set; }
		public DateTime Version { get; protected set; }

		protected Signature() { }
		public Signature (string user, string hostIP, DateTime version)
		{
			User = user;
			HostIP = hostIP;
			Version = version;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType()) return false;
			Signature s = (Signature)obj;
			return (User == s.User) && (HostIP == s.HostIP) && (Version == s.Version);
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
	
}
