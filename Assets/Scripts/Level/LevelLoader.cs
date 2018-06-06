using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private static bool IsLoading = false;

	public void Load(LevelData levelToLoad)
    {
        if(IsLoading)
        {
            Debug.LogWarning(string.Format("Failed to load {0}; already loading a level!", levelToLoad.DisplayName));
            return;
        }
        StartCoroutine(DoLevelLoad(levelToLoad));
	}

    IEnumerator DoLevelLoad(LevelData levelToLoad)
    {
        IsLoading = true;

        Debug.Log("Starting Level Load : " + levelToLoad.SceneFile.SceneName);

        DontDestroyOnLoad(gameObject);

        Scene oldScene = SceneManager.GetActiveScene();

        SceneTransitioner transitioner = SceneTransitioner.GetTransition(SceneTransitionType.Fade);
        transitioner.Run(SceneTransitionDirection.Out, 1f, false);
        yield return new WaitForSecondsRealtime(1.1f);

        //First load the loading screen
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(SceneNames.LoadingScreen, LoadSceneMode.Single);
        while(!loadOp.isDone)
        {
            yield return null;
        }

        Fun_MonoBehaviourInitializer.Initialized = false;

        transitioner.Run(SceneTransitionDirection.In, 1f, false);
        yield return new WaitForSecondsRealtime(1.1f);

        Time.timeScale = 0f;

        //Then load the new level
        loadOp = SceneManager.LoadSceneAsync(levelToLoad.SceneFile.SceneName, LoadSceneMode.Additive);
        Scene sceneToLoad = SceneManager.GetSceneByName(levelToLoad.SceneFile.SceneName);
        loadOp.allowSceneActivation = false;
        while (!loadOp.isDone && !sceneToLoad.isLoaded)
        {
            if (loadOp.progress >= .9f)
            {
                loadOp.allowSceneActivation = true;
            }

            yield return null;
        }

        Debug.Log("Scene " + levelToLoad.SceneFile.SceneName + " Loaded; Setting Active");
        SceneManager.SetActiveScene(sceneToLoad);

        //now do our fun monobehaviour queueing!!
        Fun_MonoBehaviourInitializer.Run();

        while(!Fun_MonoBehaviourInitializer.Initialized)
        {
            yield return null;
        }

        yield return null;

        transitioner.Run(SceneTransitionDirection.Out, 1f, false);
        yield return new WaitForSecondsRealtime(1.1f);

        //unload the loading screen
        AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(SceneNames.LoadingScreen);
        while (!unloadOp.isDone)
        {
            yield return null;
        }

        Time.timeScale = 1f;

        Debug.Log("Loading Screen Unloaded");

        transitioner.Run(SceneTransitionDirection.In, 1f, true);
        yield return new WaitForSecondsRealtime(1.1f);

        IsLoading = false;

        Debug.Log("Successfully Loaded Level : " + levelToLoad.SceneFile.SceneName);

        Destroy(gameObject);
    }
}
