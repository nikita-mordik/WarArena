using System;

namespace CodeBase.Infrastructure.Services.SceneLoad
{
    public interface ISceneLoaderService
    {
        void LoadScene(string sceneName, Action onSceneLoad = null);
    }
}