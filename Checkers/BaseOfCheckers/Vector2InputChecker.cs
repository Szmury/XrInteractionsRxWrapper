//https://github.com/Szmury/XrInteractionsRxWrapper
using UnityEngine;
using UnityEngine.XR;

namespace XRInputRXWrapper
{
    public abstract class Vector2InputChecker : BaseInputCheck<Vector2>
    {
        protected override void HandleCheckingLogic(InputDevice device)
        {
            device.TryGetFeatureValue(querryFuture, out DictDeviceStatus[device].currentValue);

            if (HelperVector2(DictDeviceStatus, device))
            {
                ContinuouslyChanging(device, DictDeviceStatus[device].currentValue.normalized);
                SaveTheState(DictDeviceStatus, device);
            }
        }
    }
}