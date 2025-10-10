using Abstraction.SharedModel;
using UnityEngine;
using Zenject;

namespace Abstraction
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        public override void InstallBindings()
        {

            Container.Bind<GameDataController>().AsSingle().NonLazy();
            Container.Bind<InputController>().AsSingle().NonLazy();

            Container.Bind<PlayerSM>().AsSingle().NonLazy();
        }
    } 
}