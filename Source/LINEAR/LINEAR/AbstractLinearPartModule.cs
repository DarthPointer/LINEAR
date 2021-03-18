using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LINEAR
{
    public class AbstractLinearPartModule : PartModule
    {
    }

    public class LinearResourceConverter : AbstractLinearPartModule, ISerializationCallbackReceiver
    {
        #region Fields

        [KSPField(isPersistant = true, guiActive = true, guiActiveEditor = true, guiName = "Process throttling", groupName = "LinearResourceConverter", groupDisplayName = "LINEAR Resource Converter"),
            UI_FloatRange(maxValue = 1, minValue = 0, stepIncrement = 0.001f)]
        private float throttle = 1;

        [KSPField(isPersistant = true, guiActive = false)]
        private bool processEnabled;

        [KSPField(isPersistant = true, guiActive = false)]
        public bool controllable;

        private Dictionary<string, double> resourceRates = new Dictionary<string, double>();

        private static Dictionary<string, double> transferredResourceRatesDict;

        private bool inEditor;

        #endregion

        #region ConfigNode Reloading

        public override void OnLoad(ConfigNode node)
        {
            if (node.HasNode("ResourceRates"))
            {
                foreach (ConfigNode.Value val in node.GetNode("ResourceRates").values)
                {
                    if (double.TryParse(val.value, out double rate))
                    {
                        resourceRates[val.name] = rate;
                    }

                    else
                    {
                        Lib.LogError("LinearResourceConverter found misformatted resource rate value in ResourceRates subnode");
                    }
                }
            }
            else
            {
                Lib.LogWarning("LinearResourceConverter had no ResourceRates subnode in its config");
            }

            base.OnLoad(node);
        }

        #endregion

        #region Logics

        public override void OnStart(StartState state)
        {
            inEditor = state == StartState.Editor;
            UpdateToggleButton();

            base.OnStart(state);
        }

        [KSPEvent(guiActive = true, guiActiveEditor = true, guiName = "Toggle Process", groupName = "LinearResourceConverter", groupDisplayName = "LINEAR Resource Converter")]
        public void ToggleLinearConverter()
        {
            processEnabled = !processEnabled;

            UpdateToggleButton();
        }

        public void FixedUpdate()
        {
            if (processEnabled && !Lib.WeAreRailWarping() && !inEditor)
            {
                //if (lastUpdateTime == -1)
                //{
                //    lastUpdateTime = Planetarium.GetUniversalTime();
                //    return;
                //}

                double mult = throttle * TimeWarp.fixedDeltaTime;

                foreach (KeyValuePair<string, double> resourceRate in resourceRates)
                {
                    part.RequestResource(resourceRate.Key, -resourceRate.Value * mult);
                }
            }

            //if (Lib.WeAreRailWarping())
            //{
            //    lastUpdateTime = -1;
            //}
            //else
            //{
            //    lastUpdateTime = Planetarium.GetUniversalTime();
            //}
        }

        #endregion

        #region ISerializationCallbackReciever

        public void OnBeforeSerialize()
        {
            transferredResourceRatesDict = resourceRates;
        }

        public void OnAfterDeserialize()
        {
            resourceRates = transferredResourceRatesDict;
        }

        #endregion

        #region Internal Methods

        private void UpdateToggleButton()
        {
            if (processEnabled)
            {
                Events["ToggleLinearConverter"].guiName = "Disable Process";
            }
            else
            {
                Events["ToggleLinearConverter"].guiName = "Enable Process";
            }
        }

        #endregion
    }
}
