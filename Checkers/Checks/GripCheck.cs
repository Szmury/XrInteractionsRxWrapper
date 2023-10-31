//https://github.com/Szmury/XrInteractionsRxWrapper
using UnityEngine.XR;

namespace XRInputRXWrapper
{
    public class GripCheck : BoolInteractionChecker
    {
        protected override InputFeatureUsage<bool> querryFuture { get; set; } = CommonUsages.gripButton;
    }
}