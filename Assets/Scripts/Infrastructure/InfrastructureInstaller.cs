using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class InfrastructureInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _uiInventoryCellPrefab;
        public override void InstallBindings()
        {
            InstalSaves();
            InstallFactories();
        }
        private void InstalSaves()
        {

        }
        private void InstallFactories()
        {
            Container.Bind<Factories.UIInventoryCellFactory>().AsSingle().WithArguments(Container, _uiInventoryCellPrefab);
        }
    }
}
