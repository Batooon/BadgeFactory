using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Scene _currentScene;
    [SerializeField]
    private SceneAsset _sceneToLoad;

    [SerializeField]
    private GameObject _loadingScreen;

    private List<AsyncOperation> _sceneLoadingData = new List<AsyncOperation>();

    private void Awake()
    {
        _currentScene = SceneManager.GetActiveScene();
    }

    private void Start()
    {
        LoadGame();
    }

    private void LoadGame()
    {
        SceneManager.sceneLoaded += SetActiveScene;

        _loadingScreen.SetActive(true);

        _sceneLoadingData.Add(SceneManager.LoadSceneAsync(_sceneToLoad.name, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    private void SetActiveScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.sceneLoaded -= SetActiveScene;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneToLoad.name));
        SceneManager.UnloadSceneAsync(_currentScene);
    }

    private IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < _sceneLoadingData.Count; i++)
        {
            while (!_sceneLoadingData[i].isDone)
            {
                yield return null;
            }
        }
        _loadingScreen.SetActive(false);
    }
}
