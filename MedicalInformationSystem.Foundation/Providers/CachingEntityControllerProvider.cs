using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MedicalInformationSystem.Foundation.Providers
{
    [UsedImplicitly]
    public class CachingEntityControllerProvider<TEntity, TController> : IEntityControllerProvider<TEntity, TController>
    {
        private readonly IEntityControllerFactory<TEntity, TController> _entityControllerFactory;

        private readonly ConcurrentDictionary<TEntity, TController> _entityControllersCache;

        public CachingEntityControllerProvider(
            IEntityControllerFactory<TEntity, TController> entityControllerFactory,
            IEqualityComparer<TEntity> entityEqualityComparer,
            IAuthenticationService authenticationService)
        {
            _entityControllerFactory = entityControllerFactory;

            _entityControllersCache = new ConcurrentDictionary<TEntity, TController>(entityEqualityComparer);

            authenticationService.SignedOut += AuthenticationServiceOnSignedOut;
        }

        public TController GetControllerFor(TEntity entity)
        {
            TController controller;
            if (!_entityControllersCache.TryGetValue(entity, out controller))
            {
                controller = _entityControllerFactory.CreateFrom(entity);
                if (!_entityControllersCache.TryAdd(entity, controller))
                {
                    return _entityControllersCache[entity];
                }
            }

            return controller;
        }

        public TEntity GetEntityOf(TController controller)
            => _entityControllersCache.Single(pair => pair.Value.Equals(controller)).Key;

        private void AuthenticationServiceOnSignedOut(object sender, System.EventArgs e)
        {
            ClearCache();
        }

        private void ClearCache()
        {
            _entityControllersCache.Clear();
        }
    }
}
