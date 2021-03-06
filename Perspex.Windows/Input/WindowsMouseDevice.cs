﻿// -----------------------------------------------------------------------
// <copyright file="MouseDevice.cs" company="Steven Kirk">
// Copyright 2014 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Perspex.Windows.Input
{
    using System;
    using Perspex.Input;
    using Perspex.Windows.Interop;

    public class WindowsMouseDevice : MouseDevice
    {
        private static WindowsMouseDevice instance = new WindowsMouseDevice();

        public static WindowsMouseDevice Instance
        {
            get { return instance; }
        }

        public Window CurrentWindow
        {
            get;
            set;
        }

        public new Point Position
        {
            get { return base.Position; }
            internal set { base.Position = value; }
        }

        protected override Point GetClientPosition()
        {
            UnmanagedMethods.POINT p;
            UnmanagedMethods.GetCursorPos(out p);
            UnmanagedMethods.ScreenToClient(this.CurrentWindow.Handle, ref p);
            return new Point(p.X, p.Y);
        }
    }
}

