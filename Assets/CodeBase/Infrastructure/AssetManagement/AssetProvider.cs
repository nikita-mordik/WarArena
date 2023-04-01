using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly DiContainer container;

        public AssetProvider(DiContainer container)
        {
            this.container = container;
        }
        
        public GameObject Instantiate(string path) => 
            container.InstantiatePrefabResource(path);

        public GameObject Instantiate(string path, Vector3 at) => 
            container.InstantiatePrefabResource(path, at, Quaternion.identity, null);
    }
}