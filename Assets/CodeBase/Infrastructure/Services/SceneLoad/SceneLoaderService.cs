using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Services.SceneLoad
{
    public class SceneLoaderService : ISceneLoaderService
    {
        private readonly ICoroutineRunner coroutineRunner;

        public SceneLoaderService(ICoroutineRunner coroutineRunner)
        {
            this.coroutineRunner = coroutineRunner;
        }
        
        public void LoadScene(string sceneName, Action onSceneLoad = null)
        {
            coroutineRunner.StartCoroutine(LoadSceneRoutine(sceneName, onSceneLoad));
        }

        private IEnumerator LoadSceneRoutine(string sceneName, Action onSceneLoad)
        {
            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);

            while (!loadSceneAsync.isDone)
                yield return null;
            
            onSceneLoad?.Invoke();
        }
    }
}