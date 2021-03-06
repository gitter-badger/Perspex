﻿// -----------------------------------------------------------------------
// <copyright file="DataTemplate.cs" company="Steven Kirk">
// Copyright 2014 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Perspex.Controls
{
    using System;
    using System.Reflection;

    public class DataTemplate
    {
        public static readonly DataTemplate Default =
            new DataTemplate(typeof(object), o => new TextBlock { Text = o.ToString() });

        public DataTemplate(Func<object, Control> build)
            : this(o => true, build)
        {
        }

        public DataTemplate(Type type, Func<object, Control> build)
            : this(o => IsInstance(o, type), build)
        {
        }

        public DataTemplate(Func<object, bool> match, Func<object, Control> build)
        {
            Contract.Requires<ArgumentNullException>(match != null);
            Contract.Requires<ArgumentNullException>(build != null);

            this.Match = match;
            this.Build = build;
        }

        public static bool IsInstance(object o, Type t)
        {
            return t.GetTypeInfo().IsAssignableFrom(o.GetType().GetTypeInfo());
        }

        public Func<object, bool> Match { get; private set; }

        public Func<object, Control> Build { get; private set; }
    }

    public class DataTemplate<T> : DataTemplate
    {
        public DataTemplate(Func<T, Control> build)
            : base(typeof(T), Cast(build))
        {
        }

        public DataTemplate(Func<T, bool> match, Func<T, Control> build)
            : base(CastMatch(match), Cast(build))
        {
        }

        private static Func<object, bool> CastMatch(Func<T, bool> f)
        {
            return o => (o is T) ? f((T)o) : false;
        }

        private static Func<object, TResult> Cast<TResult>(Func<T, TResult> f)
        {
            return o => f((T)o);
        }
    }
}
