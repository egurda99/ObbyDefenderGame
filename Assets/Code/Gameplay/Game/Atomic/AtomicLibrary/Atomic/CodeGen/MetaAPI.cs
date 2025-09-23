/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;
using System.Runtime.CompilerServices;
using Atomic.Elements;
using ObbyDefender.Weapons;
using MyCodeBase.Utils;

namespace Atomic.Entities
{
    public static class MetaAPI
    {
        ///Keys
        public const int PointsValue = 73; // ReactiveVariable<int>
        public const int CollectedEvent = 75; // IEvent
        public const int CollectedPoints = 76; // ReactiveVariable<int>


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<int> GetPointsValue(this IEntity obj) => obj.GetValue<ReactiveVariable<int>>(PointsValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetPointsValue(this IEntity obj, out ReactiveVariable<int> value) => obj.TryGetValue(PointsValue, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddPointsValue(this IEntity obj, ReactiveVariable<int> value) => obj.AddValue(PointsValue, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasPointsValue(this IEntity obj) => obj.HasValue(PointsValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelPointsValue(this IEntity obj) => obj.DelValue(PointsValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPointsValue(this IEntity obj, ReactiveVariable<int> value) => obj.SetValue(PointsValue, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEvent GetCollectedEvent(this IEntity obj) => obj.GetValue<IEvent>(CollectedEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCollectedEvent(this IEntity obj, out IEvent value) => obj.TryGetValue(CollectedEvent, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCollectedEvent(this IEntity obj, IEvent value) => obj.AddValue(CollectedEvent, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCollectedEvent(this IEntity obj) => obj.HasValue(CollectedEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCollectedEvent(this IEntity obj) => obj.DelValue(CollectedEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCollectedEvent(this IEntity obj, IEvent value) => obj.SetValue(CollectedEvent, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<int> GetCollectedPoints(this IEntity obj) => obj.GetValue<ReactiveVariable<int>>(CollectedPoints);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCollectedPoints(this IEntity obj, out ReactiveVariable<int> value) => obj.TryGetValue(CollectedPoints, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCollectedPoints(this IEntity obj, ReactiveVariable<int> value) => obj.AddValue(CollectedPoints, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCollectedPoints(this IEntity obj) => obj.HasValue(CollectedPoints);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCollectedPoints(this IEntity obj) => obj.DelValue(CollectedPoints);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCollectedPoints(this IEntity obj, ReactiveVariable<int> value) => obj.SetValue(CollectedPoints, value);
    }
}
