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
    }
}
