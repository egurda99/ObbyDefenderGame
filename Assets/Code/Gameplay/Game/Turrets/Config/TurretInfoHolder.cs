using System;
using UnityEngine;

namespace ObbyDefender
{
    [Serializable]
    public sealed class TurretInfoHolder
    {
        [SerializeField] private Transform _position;
        [SerializeField] private int _priceToBuild;
        [SerializeField] private float _timeToBuild;

        public float TimeToBuild => _timeToBuild;

        public Transform Position => _position;

        public int PriceToBuild => _priceToBuild;
    }
}
