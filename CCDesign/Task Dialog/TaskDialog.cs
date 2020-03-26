using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CCTools.CCDesign
{
    sealed class TaskDialog
    {
        #region Fields

        private readonly NativeMethods.TASKDIALOGCONFIG _taskConfig = new NativeMethods.TASKDIALOGCONFIG();

        #endregion

        #region Constructor

        public TaskDialog()
        {
            _taskConfig.cbSize = Marshal.SizeOf(typeof(NativeMethods.TASKDIALOGCONFIG));
        }

        #endregion

        #region Properties

        public Collection<TaskDialogButton> Buttons { get; } = new Collection<TaskDialogButton>();

        public TaskDialogStandardButtons CommonButtons
        {
            set { _taskConfig.dwCommonButtons = value; }
        }

        public string Content
        {
            set { _taskConfig.pszContent = value; }
        }

        public string ExpandedInformation
        {
            set { _taskConfig.pszExpandedInformation = value; }
        }

        public bool ExpandFooterArea
        {
            set { _taskConfig.dwFlags = value ? _taskConfig.dwFlags | NativeMethods.TASKDIALOG_FLAGS.TDF_EXPAND_FOOTER_AREA : _taskConfig.dwFlags & ~NativeMethods.TASKDIALOG_FLAGS.TDF_EXPAND_FOOTER_AREA; }
        }

        public string MainInstruction
        {
            set { _taskConfig.pszMainInstruction = value; }
        }

        public TaskDialogIcon MainIcon
        {
            set { _taskConfig.mainIcon = new IntPtr((int)value); }
        }

        public string WindowTitle
        {
            set { _taskConfig.pszWindowTitle = value; }
        }

        #endregion

        #region Methods

        public DialogResult ShowDialog()
        {
            _taskConfig.hwndParent = NativeMethods.GetActiveWindow();

            var count = Buttons.Count;

            if (count > 0)
            {
                _taskConfig.cButtons = count;
                unsafe
                {
                    var pButtons = stackalloc NativeMethods.TASKDIALOG_BUTTON[count];
                    for (int i = 0; i < count; i++)
                    {
                        var button = Buttons[i];
                        pButtons[i].nButtonID = button.Id;
                        fixed (char* pszButtonText = button.Text)
                            pButtons[i].pszButtonText = pszButtonText;
                    }
                    _taskConfig.pButtons = pButtons;
                }
            }

            return NativeMethods.TaskDialogIndirect(_taskConfig, out DialogResult nButton, IntPtr.Zero, IntPtr.Zero) == 0 ? nButton : DialogResult.None;
        }

        #endregion
    }
}
