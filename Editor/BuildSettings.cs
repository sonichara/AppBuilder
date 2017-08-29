using UnityEditor;

namespace AppBuilder {
	public class BuildSettings {
		public static void UpdateSettings() {
			PlayerSettings.companyName = "Packtpub";
			PlayerSettings.productName = "Run And Jump";
			PlayerSettings.bundleVersion = "1.0";

			PlayerSettings.applicationIdentifier = "com.packtpu.runandjump";
		}
	}
}
