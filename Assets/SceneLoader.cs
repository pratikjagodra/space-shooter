using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class SceneLoader : MonoBehaviour
{

    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Submit"))
        {
            LoadFirstScene();
        }
    }

    void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }
}
