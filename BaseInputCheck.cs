//https://github.com/Szmury/XrInteractionsRxWrapper
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.XR;

namespace XRInputRXWrapper
{
    public abstract class BaseInputCheck<T> : IUpdatable
    {

        protected Subject<Unit> onStartedLeft = new();
        protected Subject<Unit> onFinishedLeft = new();
        
        protected Subject<Unit> onStartedRight = new();
        protected Subject<Unit> onFinishedRight = new();
        
        protected Subject<Vector2> continousChangeLeft = new();
        protected Subject<Vector2> continousChangeRight = new();

        protected Dictionary<InputDevice, CheckControllerKey<T>> DictDeviceStatus = new();
        
        public IObservable<Unit> OnStartedLeft => onStartedLeft;
        public IObservable<Unit> OnFinishedLeft => onFinishedLeft;
        public IObservable<Unit> OnStartedRight => onStartedRight;
        public IObservable<Unit> OnFinishedRight => onFinishedRight;

        public IObservable<Vector2> OnChangeLeft => continousChangeLeft;
        public IObservable<Vector2> OnChangeRight => continousChangeRight;
        

        protected abstract InputFeatureUsage<T> querryFuture { get;  set; }
        public string Name => querryFuture.name;
        

        public void Update(InputDevice device)
        {
            if (!DictDeviceStatus.ContainsKey(device))
            {
                DictDeviceStatus.Add(device, new CheckControllerKey<T>());
            }
            HandleCheckingLogic(device);
        }
        
        protected abstract void HandleCheckingLogic(InputDevice device);

        
        protected void Started(InputDevice device)
        {
            (IsLeft(device)? onStartedLeft: onStartedRight).OnNext(Unit.Default);
        }
        
        protected void Ended(InputDevice device)
        {
            (IsLeft(device)? onFinishedLeft: onFinishedRight).OnNext(Unit.Default);
        }

        protected void ContinuouslyChanging(InputDevice device, Vector2 value)
        {
            (IsLeft(device)? continousChangeLeft: continousChangeRight).OnNext(value);
        }
        
        protected void ContinuouslyChanging(InputDevice device, float value)
        {
            (IsLeft(device)? continousChangeLeft: continousChangeRight).OnNext(new Vector2(value,0));
        }

        private bool IsLeft(InputDevice device)
        {
            return (device.characteristics & InputDeviceCharacteristics.Left) == InputDeviceCharacteristics.Left;
        }

        protected static bool HelperCompareBool(Dictionary<InputDevice, CheckControllerKey<bool>> dict, InputDevice device)
        {
            return dict[device].currentValue != dict[device].previousValue;
        }

        protected static bool HelperCompareFloat(Dictionary<InputDevice, CheckControllerKey<float>> dict, InputDevice device)
        {
            return Math.Abs(dict[device].currentValue - dict[device].previousValue) > 0.01f;
        } 
            
        protected static bool HelperVector2(Dictionary<InputDevice, CheckControllerKey<Vector2>> dict, InputDevice device)
        {
            return Vector2.SqrMagnitude(dict[device].currentValue - dict[device].previousValue) > 0.0000001f;
        }
        
        protected static void SaveTheState(Dictionary<InputDevice, CheckControllerKey<bool>> dict, InputDevice device) 
        {
            dict[device].previousValue = dict[device].currentValue;
        }
        
        protected static void SaveTheState(Dictionary<InputDevice, CheckControllerKey<float>> dict, InputDevice device) 
        {
            dict[device].previousValue = dict[device].currentValue;
        }
        
        protected static void SaveTheState(Dictionary<InputDevice, CheckControllerKey<Vector2>> dict, InputDevice device) 
        {
            dict[device].previousValue = dict[device].currentValue;
        }
    }
}