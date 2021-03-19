using KSP.UI.Screens;
using UnityEngine;

namespace LINEAR
{
    static class Lib
    {
        public static void Log(string message)
        {
            Debug.Log("[LINEAR]" + message);
        }

        public static void LogWarning(string message)
        {
            Debug.LogWarning("[LINEAR]" + message);
        }

        public static void LogError(string message)
        {
            Debug.LogError("[LINEAR]" + message);
        }

        public static bool WeAreRailWarping()
        {
            return TimeWarp.WarpMode == TimeWarp.Modes.HIGH && TimeWarp.CurrentRate != 1;
        }

        #region Toolbar Button

        private static ApplicationLauncherButton linearWindowToggleToolbarButton = null;

        public static void CreateLinearWindowToggleToolarButton()
        {
            Texture2D texture = new Texture2D(64, 64, TextureFormat.ARGB32, false);             // Hardcode = bad. Sry.
            ImageConversion.LoadImage(texture, System.IO.File.ReadAllBytes("GameData/LINEAR/Resources/y=kx+b.png"));

            linearWindowToggleToolbarButton = ApplicationLauncher.Instance.AddModApplication(null, null, null, null, null, null,
                    ApplicationLauncher.AppScenes.FLIGHT, texture);

            linearWindowToggleToolbarButton.onLeftClick = () => LINEARWindow.show = !LINEARWindow.show;
        }

        public static void TryRemoveLinearWindowToggleToolabrButton()
        {
            if (linearWindowToggleToolbarButton != null)
            {
                ApplicationLauncher.Instance.RemoveModApplication(linearWindowToggleToolbarButton);
            }
        }

        #endregion
    }
}
