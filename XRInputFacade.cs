//https://github.com/Szmury/XrInteractionsRxWrapper
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.XR;

namespace XRInputRXWrapper
{
    public class XRInputFacade : MonoBehaviour
    {
        private TouchPadTouchCheck touchPadTouchCheck = new();
        private TriggerClickCheck triggerClickCheck = new();
        private GripCheck gripCheck = new();
        private TochPadClickCheck tochPadClickCheck = new();
        private TouchPadChangeCheck touchPadChangeCheck = new();
        private TriggerCheck triggerCheck = new();

        private List<InputDevice> controllers = new();
        private List<IUpdatable> InputsToUpdate = new();

        public IObservable<Unit> TriggerLeftClickStarted => triggerClickCheck.OnStartedLeft;
        public IObservable<Unit> TriggerLeftClickEnded => triggerClickCheck.OnFinishedLeft;
        public IObservable<Unit> TriggerRightClickStarted => triggerClickCheck.OnStartedRight;
        public IObservable<Unit> TriggerRightClickEnded => triggerClickCheck.OnFinishedRight;

        public IObservable<Unit> GripLeftClickStart => gripCheck.OnStartedLeft;
        public IObservable<Unit> GripLeftClickEnded => gripCheck.OnFinishedLeft;
        public IObservable<Unit> GripRightClickStart => gripCheck.OnStartedRight;
        public IObservable<Unit> GripRightClickEnded => gripCheck.OnFinishedRight;

        public IObservable<Unit> TouchPadClickLeftStart => tochPadClickCheck.OnStartedLeft;
        public IObservable<Unit> TouchPadClickLeftEnded => tochPadClickCheck.OnFinishedLeft;
        public IObservable<Unit> TouchPadClickRightStart => tochPadClickCheck.OnStartedRight;
        public IObservable<Unit> TouchPadClickRightEnded => tochPadClickCheck.OnFinishedRight;

        private Subject<Vector2> touchPadLeftClickPosChange = new Subject<Vector2>();
        public IObservable<Vector2> TouchPadLeftClickPosChange => touchPadLeftClickPosChange;
        private Subject<Vector2> touchPadRightClickPosChange = new Subject<Vector2>();
        public IObservable<Vector2> TouchPadRightClickPosChange => touchPadRightClickPosChange;


        private Subject<float> triggerLeftChange = new Subject<float>();
        public IObservable<float> TriggerLeftChange => triggerLeftChange;
        private Subject<float> triggerRightChange = new Subject<float>();
        public IObservable<float> TriggerRightChange => triggerRightChange;


        private IDisposable touchPadChangeListenLeft = Disposable.Empty;
        private IDisposable touchPadChangeListenRight = Disposable.Empty;
        private IDisposable triggerChangeListenLeft = Disposable.Empty;
        private IDisposable triggerChangeListenRight = Disposable.Empty;

        public void Initalize()
        {

            InputsToUpdate.Add(touchPadChangeCheck);
            InputsToUpdate.Add(triggerClickCheck);
            InputsToUpdate.Add(gripCheck);
            InputsToUpdate.Add(tochPadClickCheck);
            InputsToUpdate.Add(touchPadChangeCheck);
            InputsToUpdate.Add(triggerCheck);

            AddSubscriptionsTrackChange(TouchPadClickLeftStart, TouchPadClickLeftEnded,
                touchPadChangeCheck.OnChangeLeft, touchPadLeftClickPosChange, touchPadChangeListenLeft);
            AddSubscriptionsTrackChange(TouchPadClickRightStart, TouchPadClickRightEnded,
                touchPadChangeCheck.OnChangeRight, touchPadRightClickPosChange, touchPadChangeListenRight);

            AddSubscriptionsTrackChange(TriggerLeftClickStarted, TriggerLeftClickEnded,
                triggerCheck.OnChangeLeft.Select(vec => vec.x)
                , triggerLeftChange, triggerChangeListenLeft);
            AddSubscriptionsTrackChange(TriggerRightClickStarted, TriggerRightClickEnded,
                triggerCheck.OnChangeRight.Select(vec => vec.x)
                , triggerRightChange, triggerChangeListenRight);
        }

        private void AddSubscriptionsTrackChange<T>(IObservable<Unit> start, IObservable<Unit> finish,
            IObservable<T> change, Subject<T> propagator, IDisposable clickDisposable)
        {
            start.Subscribe(_ => { clickDisposable = change.Subscribe(propagator.OnNext); });
            finish.Subscribe(_ => clickDisposable.Dispose());
        }

        private void Update()
        {
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller, controllers);

            foreach (IUpdatable input in InputsToUpdate)
            {
                foreach (var device in controllers)
                {
                    input.Update(device);
                }
            }
        }
    }

}