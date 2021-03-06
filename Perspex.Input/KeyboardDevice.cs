﻿// -----------------------------------------------------------------------
// <copyright file="KeyboardDevice.cs" company="Steven Kirk">
// Copyright 2014 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Perspex.Input
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using Perspex.Input.Raw;
    using Splat;

    public abstract class KeyboardDevice : IKeyboardDevice
    {
        public KeyboardDevice()
        {
            this.InputManager.RawEventReceived
                .OfType<RawKeyEventArgs>()
                .Where(x => x.Device == this)
                .Subscribe(this.ProcessRawEvent);
        }

        public IInputManager InputManager
        {
            get { return Locator.Current.GetService<IInputManager>(); }
        }

        public IFocusManager FocusManager
        {
            get { return Locator.Current.GetService<IFocusManager>(); }
        }

        public IInputElement FocusedElement
        {
            get;
            set;
        }

        public abstract ModifierKeys Modifiers { get; }

        private void ProcessRawEvent(RawKeyEventArgs e)
        {
            IInputElement element = this.FocusedElement;

            if (element != null)
            {
                switch (e.Type)
                {
                    case RawKeyEventType.KeyDown:
                        KeyEventArgs ev = new KeyEventArgs
                        {
                            RoutedEvent = InputElement.PreviewKeyDownEvent,
                            Device = this,
                            Key = e.Key,
                            Text = e.Text,
                            Source = element,
                            OriginalSource = element,
                        };

                        element.RaiseEvent(ev); ;
                        ev.RoutedEvent = InputElement.KeyDownEvent;
                        element.RaiseEvent(ev); ;
                        break;
                }
            }
        }
    }
}
