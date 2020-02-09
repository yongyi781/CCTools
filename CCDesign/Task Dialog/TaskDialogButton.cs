using System.Windows.Forms;

namespace CCTools.CCDesign
{
	sealed class TaskDialogButton
	{
		#region Constructors

		public TaskDialogButton() { }
		public TaskDialogButton(DialogResult id, string text)
		{
			this.id = id;
			this.text = text;
		}

		#endregion

		#region Properties

		private DialogResult id;
		public DialogResult Id
		{
			get { return id; }
		}

		private string text;
		public string Text
		{
			get { return text; }
		}

		#endregion
	}
}
