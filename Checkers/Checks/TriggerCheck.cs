//https://github.com/Szmury/XrInteractionsRxWrapper
using UnityEngine.XR;

namespace XRInputRXWrapper
{
    public class TriggerCheck : FloatInputCheck
    {
        protected override InputFeatureUsage<float> querryFuture { get; set; } = CommonUsages.trigger;
    }
}