using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
