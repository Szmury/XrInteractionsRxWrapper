//https://github.com/Szmury/XrInteractionsRxWrapper
using UnityEngine.XR;

namespace XRInputRXWrapper
{
    public class FloatInputCheck : BaseInputCheck<float>
    {
        protected override InputFeatureUsage<float> querryFuture { get; set; }
        
        protected override void HandleCheckingLogic(InputDevice device)
        {
            device.TryGetFeatureValue(querryFuture, out DictDeviceStatus[device].currentValue);
            //Rearange Depth 
            DictDeviceStatus[device].currentValue = (DictDeviceStatus[device].currentValue - 0.2f)/0.8f;
            if (HelperCompareFloat(DictDeviceStatus, device))
            {
                ContinuouslyChanging(device,DictDeviceStatus[device].currentValue);  
                SaveTheState(DictDeviceStatus, device);
            }
        }
    }
}