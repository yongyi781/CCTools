using System.Reflection;

namespace CCTools
{
	public static class RegistryHelper
	{
		public static string GetDefaultIconPath()
		{
			var path = Assembly.GetExecutingAssembly().CodeBase;
			return string.Format(@"""{0}"",-1", path);
		}
	}
}
