using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace CCTools.CCDesign
{
	static class RegistryHelper
	{
		const string ProgID = "Yongyi.CCDesign.2.0";

		public static bool IsAssociated()
		{
			return IsAssociatedMain() || IsAssociatedFileExts();
		}

		static bool IsAssociatedMain()
		{
			using (var cclKey = OpenMainCclKey(false))
			{
				if (cclKey == null)
					return false;
				var value = cclKey.GetValue(null);
				if (value == null)
					return false;
				return string.Equals(value.ToString(), ProgID, StringComparison.InvariantCultureIgnoreCase);
			}
		}

		static bool IsAssociatedFileExts()
		{
			using (var userChoiceKey = OpenFileExtsCclKey(false))
			{
				if (userChoiceKey == null)
					return false;
				var value = userChoiceKey.GetValue(null);
				if (value == null)
					return false;
				return string.Equals(value.ToString(), ProgID, StringComparison.InvariantCultureIgnoreCase);
			}
		}

		public static bool IsAssociatedWithAnotherProgram()
		{
			using (var cclKey = OpenMainCclKey(false))
			{
				if (cclKey == null)
					return false;
				var value = cclKey.GetValue(null);
				return value != null && !string.Equals(value.ToString(), ProgID, StringComparison.InvariantCultureIgnoreCase);
			}
		}

		public static void Associate()
		{
			CreateFileAssociation(true);
			if (IsAssociatedWithAnotherProgram())
				AssociateFileExts();
			else
				AssociateMain();
		}

		static void AssociateFileExts()
		{
			using (var userChoiceKey = OpenFileExtsCclKey(true))
			{
			}
		}

		static void AssociateMain()
		{
			throw new NotImplementedException();
		}

		public static void Unassociate()
		{
			UnassociateFileExts();
			UnassociateMain();
		}

		static void UnassociateMain()
		{
			throw new NotImplementedException();
		}

		static void UnassociateFileExts()
		{
		}

		static RegistryKey OpenMainCclKey(bool writable)
		{
			return Registry.CurrentUser.OpenSubKey(@"Software\Classes\.ccl", writable);
		}

		static RegistryKey OpenFileExtsCclKey(bool writable)
		{
			return Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.ccl\UserChoice", writable);
		}

		static RegistryKey OpenProgIDKey(bool writable)
		{
			return Registry.CurrentUser.OpenSubKey(@"Software\Classes\" + ProgID, writable);
		}

		static void CreateFileAssociation(bool returnIfAlreadyExists)
		{
			var progIDKey = OpenProgIDKey(true);
			if (progIDKey != null && returnIfAlreadyExists)
				return;
			if (progIDKey == null)
				progIDKey = Registry.CurrentUser.CreateSubKey(@"Software\Classes\" + ProgID);
			using (progIDKey)
			{
				var defaultIconKey = progIDKey.OpenSubKey("DefaultIcon", true);
				if (defaultIconKey == null)
					defaultIconKey = progIDKey.CreateSubKey("DefaultIcon");
				using (defaultIconKey)
					defaultIconKey.SetValue(string.Empty, GetDefaultIconPath());

				var openCommandKey = progIDKey.OpenSubKey(@"shell\open\command", true);
				if (openCommandKey == null)
					openCommandKey = progIDKey.CreateSubKey(@"shell\open\command");
				using (openCommandKey)
					openCommandKey.SetValue(string.Empty, string.Format("{0} \"%1\"", Application.ExecutablePath));
			}
		}

		static string GetDefaultIconPath()
		{
			return CCTools.RegistryHelper.GetDefaultIconPath();
		}
	}
}
