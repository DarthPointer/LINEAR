using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LINEAR
{
    [KSPScenario(ScenarioCreationOptions.AddToAllGames, new[] { GameScenes.FLIGHT, GameScenes.EDITOR, GameScenes.TRACKSTATION, GameScenes.SPACECENTER, GameScenes.LOADING, GameScenes.LOADINGBUFFER })]
    public class LINEARScenario : ScenarioModule
    {
        private List<ManagedVessel> managedVessels = new List<ManagedVessel>();

        public LINEARScenario()
        {
        }

        #region Subdata Classes

        private class ManagedVessel
        {
            Guid guid;
            double lastResourcesRevisionUTime;

            List<ManagedQuasiResource> managedResources;
            List<ManagedProcess> managedProcesses;
        }

        private abstract class ManagedQuasiResource
        {
            double maxAmount;
            double amount;
            double rate;
        }

        private class RealManagedResource : ManagedQuasiResource
        {
        }

        private class AmountReportingManagedPseudoResource : ManagedQuasiResource
        {
        }

        public class PseudoResourceAmountReporter
        {
            Action<string, double> reportingCallback;
        }

        private class ManagedProcess
        {
            private bool canBeTrottled;

            private bool enabled;
            private double throttle;

            Dictionary<string, double> nominalQuasiResourceRates;
        }

        #endregion
    }
}
