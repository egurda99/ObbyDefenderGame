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
    public static class VFXAPI
    {
        ///Keys
        public const int ShootFX = 38; // ParticleSystem
        public const int TakeDamageFX = 39; // ParticleSystem
        public const int ShootAmmoFX = 43; // ParticleSystem
        public const int DieFX = 48; // ParticleSystem
        public const int DestroyFX = 69; // ParticleSystem
        public const int IsFXEnded = 71; // ReactiveVariable<bool>


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParticleSystem GetShootFX(this IEntity obj) => obj.GetValue<ParticleSystem>(ShootFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetShootFX(this IEntity obj, out ParticleSystem value) => obj.TryGetValue(ShootFX, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddShootFX(this IEntity obj, ParticleSystem value) => obj.AddValue(ShootFX, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasShootFX(this IEntity obj) => obj.HasValue(ShootFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelShootFX(this IEntity obj) => obj.DelValue(ShootFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetShootFX(this IEntity obj, ParticleSystem value) => obj.SetValue(ShootFX, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParticleSystem GetTakeDamageFX(this IEntity obj) => obj.GetValue<ParticleSystem>(TakeDamageFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetTakeDamageFX(this IEntity obj, out ParticleSystem value) => obj.TryGetValue(TakeDamageFX, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTakeDamageFX(this IEntity obj, ParticleSystem value) => obj.AddValue(TakeDamageFX, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasTakeDamageFX(this IEntity obj) => obj.HasValue(TakeDamageFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTakeDamageFX(this IEntity obj) => obj.DelValue(TakeDamageFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTakeDamageFX(this IEntity obj, ParticleSystem value) => obj.SetValue(TakeDamageFX, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParticleSystem GetShootAmmoFX(this IEntity obj) => obj.GetValue<ParticleSystem>(ShootAmmoFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetShootAmmoFX(this IEntity obj, out ParticleSystem value) => obj.TryGetValue(ShootAmmoFX, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddShootAmmoFX(this IEntity obj, ParticleSystem value) => obj.AddValue(ShootAmmoFX, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasShootAmmoFX(this IEntity obj) => obj.HasValue(ShootAmmoFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelShootAmmoFX(this IEntity obj) => obj.DelValue(ShootAmmoFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetShootAmmoFX(this IEntity obj, ParticleSystem value) => obj.SetValue(ShootAmmoFX, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParticleSystem GetDieFX(this IEntity obj) => obj.GetValue<ParticleSystem>(DieFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetDieFX(this IEntity obj, out ParticleSystem value) => obj.TryGetValue(DieFX, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddDieFX(this IEntity obj, ParticleSystem value) => obj.AddValue(DieFX, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasDieFX(this IEntity obj) => obj.HasValue(DieFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelDieFX(this IEntity obj) => obj.DelValue(DieFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetDieFX(this IEntity obj, ParticleSystem value) => obj.SetValue(DieFX, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParticleSystem GetDestroyFX(this IEntity obj) => obj.GetValue<ParticleSystem>(DestroyFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetDestroyFX(this IEntity obj, out ParticleSystem value) => obj.TryGetValue(DestroyFX, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddDestroyFX(this IEntity obj, ParticleSystem value) => obj.AddValue(DestroyFX, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasDestroyFX(this IEntity obj) => obj.HasValue(DestroyFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelDestroyFX(this IEntity obj) => obj.DelValue(DestroyFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetDestroyFX(this IEntity obj, ParticleSystem value) => obj.SetValue(DestroyFX, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<bool> GetIsFXEnded(this IEntity obj) => obj.GetValue<ReactiveVariable<bool>>(IsFXEnded);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetIsFXEnded(this IEntity obj, out ReactiveVariable<bool> value) => obj.TryGetValue(IsFXEnded, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddIsFXEnded(this IEntity obj, ReactiveVariable<bool> value) => obj.AddValue(IsFXEnded, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasIsFXEnded(this IEntity obj) => obj.HasValue(IsFXEnded);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelIsFXEnded(this IEntity obj) => obj.DelValue(IsFXEnded);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIsFXEnded(this IEntity obj, ReactiveVariable<bool> value) => obj.SetValue(IsFXEnded, value);
    }
}
