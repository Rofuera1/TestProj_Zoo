using Zenject;

namespace Core
{
    public class ZenjectBinder : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CreaturePool>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CreaturesFactory>().FromComponentInHierarchy().AsSingle();
        }
    }
}