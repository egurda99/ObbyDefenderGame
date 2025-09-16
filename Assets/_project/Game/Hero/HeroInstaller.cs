using Atomic.Elements;
using Atomic.Entities;
using Elementary;
using ObbyDefender.Scripts;
using ObbyDefender.Weapons;
using UnityEngine;
using Zenject;

namespace ObbyDefender
{
    public sealed class HeroInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private ColliderDetectionOverlapSphere _enemiesSensor;
        [SerializeField] private ColliderDetectionOverlapSphere _coinsSensor;


        [SerializeField] private HeroGunsView _heroGunsView;

        [SerializeField] private MoveToDirectionMechanic _moveToDirectionMechanic;

        [SerializeField] private RotateToMoveDirectionWhenNoTargetMechanic _rotateToMoveDirectionMechanic;

        [SerializeField] private RotateToTargetMechanic _rotateToTargetMechanic;

        [SerializeField] private AutoShootToTargetMechanic _autoShootToTargetMechanic;


        [SerializeField] private LifeMechanic _lifeMechanic;

        [SerializeField] private ShootReloadMechanic _shootReloadMechanic;
        [SerializeField] private StunMechanic _stunMechanic;

        [SerializeField] private CheckTargetAliveMechanic _checkTargetAliveMechanic;

        private BulletFactory _bulletFactory;

        private NearestAliveTargetObserver _nearestAliveTargetObserver;
        private CoinsObserver _coinsObserver;
        private IEntity _entity;


        [Inject]
        public void Construct(BulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        public override void Install(IEntity entity)
        {
            entity.AddPlayerTag();
            entity.AddHeroTeamTag();
            entity.AddCollectedPoints(new ReactiveVariable<int>());


            _entity = entity;

            _moveToDirectionMechanic.Install(entity);
            _rotateToMoveDirectionMechanic.Install(entity);
            _autoShootToTargetMechanic.Install(entity);
            _autoShootToTargetMechanic.Init(_bulletFactory);
            _rotateToTargetMechanic.Install(entity);

            _lifeMechanic.Install(entity);
            _checkTargetAliveMechanic.Install(entity);

            _shootReloadMechanic.Install(entity);
            _stunMechanic.Install(entity);

            _nearestAliveTargetObserver = new NearestAliveTargetObserver(_enemiesSensor, GetComponent<SceneEntity>());
            _coinsObserver = new CoinsObserver(_coinsSensor, GetComponent<SceneEntity>());


            entity.AddWeaponType(WeaponType.None);

            entity.AddHeroGunsView(_heroGunsView);


            entity.GetCanMove().Append(() => !entity.GetIsDead().Value);
            entity.GetCanRotate().Append(() => !entity.GetIsDead().Value);
            entity.GetCanMove().Append(() => !entity.GetIsStunned().Value);

            entity.GetCanShoot().Append(() => !entity.GetIsDead().Value);

            entity.GetCanShoot().Append(() => !entity.GetNeedReload().Value);
            entity.GetCanShoot().Append(() => !entity.GetIsStunned().Value);
            entity.GetCanShoot().Append(() => entity.GetIsTargetAlive().Value);
        }

        public void ConfigurePlayer(HeroData heroData)
        {
            _entity.GetMoveSpeed().Value = heroData.Speed;
            _entity.GetHitPoints().Value = heroData.Health;
        }

        private void OnDestroy()
        {
            _nearestAliveTargetObserver.Dispose();
            _coinsObserver.Dispose();
        }
    }
}
