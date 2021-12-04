using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{


    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                
                break;
            case "Finish":
                LoadNextScene();
                Debug.Log("Player finished level");
                break;
            default:
                ReloadScene();
                Debug.Log("Player died");
                break;
        }
    }

    private static void LoadNextScene()
    {
        var numberOfScenes = SceneManager.sceneCountInBuildSettings;
        //Debug.Log("number of scenes: " + numberOfScenes);
        var nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        //Debug.Log("next scene: " + nextScene);

        if (nextScene >= numberOfScenes)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }        
    }

    private static void ReloadScene()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
