using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void sceneChanger(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
