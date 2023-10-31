//https://github.com/Szmury/XrInteractionsRxWrapper
using UnityEngine;
using UnityEngine.XR;

namespace XRInputRXWrapper
{
    public class TouchPadChangeCheck : Vector2InputChecker
    {
        protected override InputFeatureUsage<Vector2> querryFuture { get; set; } = CommonUsages.primary2DAxis;
    }
}