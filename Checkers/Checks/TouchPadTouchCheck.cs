//https://github.com/Szmury/XrInteractionsRxWrapper
using UnityEngine.XR;

namespace XRInputRXWrapper
{
    public class TouchPadTouchCheck : BoolInteractionChecker
    {
        protected override InputFeatureUsage<bool> querryFuture { get; set; } = CommonUsages.primary2DAxisTouch;
    }
}