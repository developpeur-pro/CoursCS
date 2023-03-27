using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TestsUnitaires
{
	internal static class Traceur
	{
		public static void TracerAppel([CallerFilePath] string file = "",
												 [CallerMemberName] string memberName = "")
		{
			Debug.WriteLine($"{Path.GetFileNameWithoutExtension(file)} {memberName}");
		}
	}
}
