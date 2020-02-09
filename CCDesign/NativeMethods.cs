using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;

namespace CCTools.CCDesign
{
	static class NativeMethods
	{
		#region Constants

		public const int
			BM_SETIMAGE = 0x00F7,
			BS_BITMAP = 0x00000080,

			CF_BITMAP = 2,

			GMEM_MOVEABLE = 0x0002,

			SRCCOPY = 0x00cc0020,

			WM_MOUSEHWHEEL = 0x020E,
			WM_DRAWCLIPBOARD = 0x0308,
			WM_CHANGECBCHAIN = 0x030D;

		public static readonly bool IsAdministrator = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
		public static readonly bool IsVista = Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6;
		public static readonly bool IsExactlyVista = Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major == 6;

		#endregion

		#region Delegates

		public delegate void TIMERPROC(IntPtr hwnd, int uMsg, IntPtr idEvent, int dwTime);

		#endregion

		#region DLL Imports

		[DllImport("comctl32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int TaskDialogIndirect(NativeMethods.TASKDIALOGCONFIG pTaskConfig, out DialogResult pnButton, IntPtr pnRadioButton, IntPtr pfVerificationFlagChecked);

		[DllImport("gdi32.dll", ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

		[DllImport("gdi32.dll", ExactSpelling = true)]
		public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int width, int height);

		[DllImport("gdi32.dll", ExactSpelling = true)]
		public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

		[DllImport("gdi32.dll", ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteDC(IntPtr hDC);

		[DllImport("gdi32.dll", ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteObject(IntPtr hObject);

		[DllImport("gdi32.dll", ExactSpelling = true)]
		public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

		[DllImport("kernel32.dll", ExactSpelling = true)]
		public static extern IntPtr GlobalAlloc(int uFlags, IntPtr dwBytes);

		[DllImport("kernel32.dll", ExactSpelling = true)]
		public static extern unsafe void* GlobalLock(IntPtr hMem);

		[DllImport("kernel32.dll", ExactSpelling = true)]
		public static extern IntPtr GlobalSize(IntPtr hMem);

		[DllImport("kernel32.dll", ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GlobalUnlock(IntPtr hMem);

		[DllImport("kernel32.dll", ExactSpelling = true)]
		public static extern unsafe int RtlMoveMemory(void* Destination, [In] byte[] Source, IntPtr Length);

		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		public static extern IntPtr ShellExecute(HandleRef hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

		[DllImport("user32.dll", ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CloseClipboard();

		[DllImport("user32.dll", ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool EmptyClipboard();

		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern IntPtr GetActiveWindow();

		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern IntPtr GetClipboardData(int uFormat);

		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern IntPtr GetDC(IntPtr hWnd);

		[DllImport("user32.dll", ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsClipboardFormatAvailable(int format);

		[DllImport("user32.dll", ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool OpenClipboard(HandleRef hWndNewOwner);

		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern int RegisterClipboardFormat(string lpszFormat);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern IntPtr SetClipboardData(int uFormat, IntPtr hMem);

		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern IntPtr SetTimer(IntPtr hWnd, IntPtr nIDEvent, int uElapse, TIMERPROC lpTimerFunc);

		[DllImport("uxtheme.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int SetWindowTheme(HandleRef hWnd, string pszSubAppName, string pszSubIdList);

		#endregion

		#region Helpers

		public static IntPtr GetCompatibleBitmap(IWin32Window owner, Bitmap bitmap)
		{
			var hbm = bitmap.GetHbitmap();
			var hDC = GetDC(owner.Handle);
			var srcDC = CreateCompatibleDC(hDC);
			var destDC = CreateCompatibleDC(hDC);
			var compatibleBitmap = CreateCompatibleBitmap(hDC, bitmap.Width, bitmap.Height);
			var hOldSrc = SelectObject(srcDC, hbm);
			var hOldDest = SelectObject(destDC, compatibleBitmap);

			BitBlt(destDC, 0, 0, bitmap.Width, bitmap.Height, srcDC, 0, 0, SRCCOPY);

			SelectObject(destDC, hOldDest);
			SelectObject(srcDC, hOldSrc);
			DeleteDC(destDC);
			DeleteDC(srcDC);
			ReleaseDC(owner.Handle, hDC);
			DeleteObject(hbm);

			return compatibleBitmap;
		}

		public static string GetWindowsPath()
		{
			return @"C:\Apps\otvdm-v0.7.0\WINDOWS";
		}

		#endregion

		#region Structs

		#region CHIPEDIT_MAPSECT

		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		public struct CHIPEDIT_MAPSECT
		{
			public int width;
			public int height;
			public int bytesToEndOfData;
			public int reserved1;
			public int reserved2;
			public short marker;
		}

		#endregion

		#region TASKDIALOGCONFIG

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
		public class TASKDIALOGCONFIG
		{
			public int cbSize;
			public IntPtr hwndParent;
			public IntPtr hInstance;
			public TASKDIALOG_FLAGS dwFlags;
			public TaskDialogStandardButtons dwCommonButtons;
			public string pszWindowTitle;
			public IntPtr mainIcon;
			public string pszMainInstruction;
			public string pszContent;
			public int cButtons;
			public unsafe TASKDIALOG_BUTTON* pButtons;
			public int nDefaultButton;
			public int cRadioButtons;
			public unsafe TASKDIALOG_BUTTON* pRadioButtons;
			public int nDefaultRadioButton;
			public string pszVerificationText;
			public string pszExpandedInformation;
			public string pszExpandedControlText;
			public string pszCollapsedControlText;
			public IntPtr footerIcon;
			public string pszFooter;
			public IntPtr pfCallback;
			public IntPtr lpCallbackData;
			public int cxWidth;
		}

		#endregion

		#region TASKDIALOG_BUTTON

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
		public struct TASKDIALOG_BUTTON
		{
			public DialogResult nButtonID;
			public unsafe char* pszButtonText;
		}

		#endregion

		#endregion

		#region Enums

		#region TASKDIALOG_FLAGS

		[Flags]
		public enum TASKDIALOG_FLAGS
		{
			TDF_ENABLE_HYPERLINKS = 0x0001,
			TDF_USE_HICON_MAIN = 0x0002,
			TDF_USE_HICON_FOOTER = 0x0004,
			TDF_ALLOW_DIALOG_CANCELLATION = 0x0008,
			TDF_USE_COMMAND_LINKS = 0x0010,
			TDF_USE_COMMAND_LINKS_NO_ICON = 0x0020,
			TDF_EXPAND_FOOTER_AREA = 0x0040,
			TDF_EXPANDED_BY_DEFAULT = 0x0080,
			TDF_VERIFICATION_FLAG_CHECKED = 0x0100,
			TDF_SHOW_PROGRESS_BAR = 0x0200,
			TDF_SHOW_MARQUEE_PROGRESS_BAR = 0x0400,
			TDF_CALLBACK_TIMER = 0x0800,
			TDF_POSITION_RELATIVE_TO_WINDOW = 0x1000,
			TDF_RTL_LAYOUT = 0x2000,
			TDF_NO_DEFAULT_RADIO_BUTTON = 0x4000,
			TDF_CAN_BE_MINIMIZED = 0x8000
		}

		#endregion

		#endregion
	}
}
