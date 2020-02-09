using System;

namespace CCTools
{
	[Flags]
	public enum DrawLevelOptions
	{
		None = 0,
		RevealHiddenAndInvisibleWalls = 1,
		RevealFakeWalls = 2,
	}
}
