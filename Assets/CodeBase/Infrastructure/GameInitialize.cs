using CodeBase.Infrastructure.Factory;
using CodeBase.Interface;
using CodeBase.Pool;
using CodeBase.UI.Elements;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class GameInitialize : MonoBehaviour
    {
        private IGameFactory gameFactory;
        private IObjectPoolService poolService;
        
        [Inject]
        private void Construct(IGameFactory gameFactory, IObjectPoolService objectPoolService)
        {
            this.gameFactory = gameFactory;
            poolService = objectPoolService;
        }
        
        private void Start()
        {
            OnLoad();
        }
        
        private void OnLoad()
        {
            InitGameWorld();
            poolService.InitializePool();
        }

        private void InitGameWorld()
        {
            GameObject hero = InitializeHero();
            InitializeHUD(hero);
        }

        private GameObject InitializeHero()
        {
            GameObject hero = gameFactory.CreateHero(GetSpawnPosition());
            return hero;
        }

        private void InitializeHUD(GameObject hero)
        {
            GameObject hud = gameFactory.CreateHud();
            hud.GetComponentInChildren<ActorUI>()
                .Construct(hero.GetComponent<IHealth>(), hero.GetComponent<IEnergy>());
        }

        private static Vector3 GetSpawnPosition() => 
            GameObject.FindWithTag("Spawner").transform.position;
    }
}