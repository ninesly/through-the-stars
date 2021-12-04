using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Header("Time of delay:")]
    [SerializeField] float afterSuccess = 2f;
    [SerializeField] float afterCrash = 2.5f;
    [SerializeField] AudioClip successSound;
    [SerializeField] [Range(0, 1)] float successSoundVolume = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] [Range(0, 1)] float crashSoundVolume = 1f;

    bool isTransitioning = false;

    Movement movement;
    AudioSource myAudioSource;

    private void Start()
    {
        movement = GetComponent<Movement>();
        myAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ForceReload();
    }

    private void ForceReload()
    {
        if (isTransitioning & Input.GetKeyDown(KeyCode.Space))
        {
            ReloadScene();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) return;

        switch (other.gameObject.tag)
        {
            case "Friendly":                
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();               
                break;
        }
    }

    private void StartSuccessSequence()
    {

            Debug.Log("Player finished level");
            isTransitioning = true;
            movement.enabled = false;
            movement.PlayingSFX(successSound, successSoundVolume, false);
            Invoke(nameof(LoadNextScene), afterSuccess);

    }

    void StartCrashSequence()
    {
            Debug.Log("Player crashed");
            isTransitioning = true;
            movement.enabled = false;
            movement.PlayingSFX(crashSound, crashSoundVolume, false);
            Invoke(nameof(ReloadScene), afterCrash);

    }



    private void LoadNextScene()
    {
        var numberOfScenes = SceneManager.sceneCountInBuildSettings; 
        var nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene >= numberOfScenes)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }        
    }



    private void ReloadScene()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
