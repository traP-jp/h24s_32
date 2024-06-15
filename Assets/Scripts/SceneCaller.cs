using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneCaller : MonoBehaviour
{
    [SerializeField] bool callTestScene = false;
    [SerializeField] string testSceneName;
    void Start()
    {
        if (callTestScene)
        {
            SceneManager.LoadScene(testSceneName, LoadSceneMode.Additive);
        }
    }
    public void LoadStageScene(int stageNumber)
    {
        SceneManager.LoadScene("Scene_" + stageNumber.ToString("D"), LoadSceneMode.Additive);
    }
    public void UnloadStageScene(int stageNumber)
    {
        SceneManager.UnloadSceneAsync("Scene_" + stageNumber.ToString("D"));
    }
    public void LoadOtherScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
    public void UnLoadOtherScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
