//https://github.com/Szmury/XrInteractionsRxWrapper
using UnityEngine.XR;

namespace XRInputRXWrapper
{
    public abstract class BoolInteractionChecker : BaseInputCheck<bool>
    {
        protected override void HandleCheckingLogic(InputDevice device)
        {
            device.TryGetFeatureValue(querryFuture, out DictDeviceStatus[device].currentValue);
            if (HelperCompareBool(DictDeviceStatus,device))
            {
                if (DictDeviceStatus[device].currentValue)
                {
                    Started(device);
                }
                else
                {
                    Ended(device);
                }
            }

            SaveTheState(DictDeviceStatus, device);
        }

    }
}