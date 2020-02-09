using System;

namespace CCTools
{
	public class ItemChangedEventArgs : EventArgs
	{
		public ItemChangedEventArgs(Level level) { Level = level; }

		public Level Level { get; set; }
	}
}
