using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class InfrastructureInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _uiInventoryCellPrefab;
        public override void InstallBindings()
        {
            InstallFactories();
        }
        private void InstallFactories()
        {
            Container.Bind<Factories.UIInventoryCellFactory>().AsSingle().WithArguments(Container, _uiInventoryCellPrefab);
        }
    }
}
