using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LINEAR
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    class LINEARWindow : MonoBehaviour
    {
        public static bool show = false;

        private static LINEARWindow instance;
        private static KSP.IO.PluginConfiguration config = null;
        private static Rect windowRect = new Rect();

        #region Lifecycle

        private void Start()
        {
            instance = this;

            if (config == null)
            {
                config = KSP.IO.PluginConfiguration.CreateForType<LINEARWindow>();
            }

            config.load();

            windowRect = config.GetValue("windowRect", new Rect(500, 500, 300, 0));
            config["windowRect"] = windowRect;
        }

        private void OnDestroy()
        {
            instance = null;
        }

        #endregion

        private void OnGUI()
        {
            if (show) Draw();
        }

        public static void Draw()
        {
            windowRect = GUILayout.Window(0, windowRect, DoWindow, "LINEAR", GUILayout.Width(100));
        }

        private static void DoWindow(int id)
        {
            if (GUI.Button(new Rect(windowRect.width - 20, 5, 20, 20), "x"))
                show = false;
        }
    }
}
