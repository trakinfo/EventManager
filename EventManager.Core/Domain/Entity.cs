using EventManager.Core.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Core.Domain
{
    public abstract class Entity
    {
		//private ISet<Signature> _modifier = new HashSet<Signature>();
		public long Id { get; protected set; }
		public Signature Creator { get; protected set; }
		public ISet<Signature> ModifierList { get; set; }

	}
}
