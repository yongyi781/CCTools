using System.Windows.Forms;

namespace CCTools.CCDesign
{
    sealed class TaskDialogButton
    {
        #region Constructors

        public TaskDialogButton() { }
        public TaskDialogButton(DialogResult id, string text)
        {
            Id = id;
            Text = text;
        }

        #endregion

        #region Properties

        public DialogResult Id { get; }
        public string Text { get; }

        #endregion
    }
}
