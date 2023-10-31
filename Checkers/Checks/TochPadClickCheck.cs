//https://github.com/Szmury/XrInteractionsRxWrapper
using UnityEngine.XR;

namespace XRInputRXWrapper
{
    public class TochPadClickCheck : BoolInteractionChecker
    {
        protected override InputFeatureUsage<bool> querryFuture { get; set; } = CommonUsages.primary2DAxisClick;
    }
}