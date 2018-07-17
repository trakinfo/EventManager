using EventManager.Core.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Core.Domain
{
    public abstract class Entity
    {
		public ulong Id { get; protected set; }
		public Signature Creator { get; protected set; }
	}
}
