using BepInEx;

namespace Clubass
{
	[BepInPlugin("CLUBMASTER.clubass.tabs", "clubass", "1.1.1")]
	public class Launcher : BaseUnityPlugin
	{
		public Launcher()
		{
			Binder.UnitGlad();
		}
	}
}
