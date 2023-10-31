//https://github.com/Szmury/XrInteractionsRxWrapper
using UnityEngine.XR;

namespace XRInputRXWrapper
{
    public class TriggerClickCheck : BoolInteractionChecker
    {
        protected override InputFeatureUsage<bool> querryFuture { get; set; } = CommonUsages.triggerButton;
    }
}