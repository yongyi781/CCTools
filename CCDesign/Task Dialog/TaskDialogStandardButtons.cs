using System;

namespace CCTools.CCDesign
{
	[Flags]
	enum TaskDialogStandardButtons
	{
		None,
		OK = 1,
		Yes = 2,
		No = 4,
		Cancel = 8,
		Retry = 0x10,
		Close = 0x20
	}
}
