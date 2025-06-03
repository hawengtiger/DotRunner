using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDiff : MonoBehaviour
{
    // Start is called before the first frame update
    public void SceneGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void SceneMain()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void Quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
