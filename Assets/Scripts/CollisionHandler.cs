using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // params for tuning
    [Header("Time of delay:")]
    [SerializeField] float delayAfterSuccess = 2f;
    [SerializeField] float delayAfterCrash = 2.5f;

    // special effects
    [Header("Special Effects:")]
    [SerializeField] AudioClip successSound;
    [SerializeField] [Range(0, 1)] float successSoundVolume = 1f;
    [SerializeField] ParticleSystem successParticleSystem;
    [SerializeField] AudioClip crashSound;
    [SerializeField] [Range(0, 1)] float crashSoundVolume = 1f;
    [SerializeField] ParticleSystem crashParticleSystem;

    // states
    bool isTransitioning = false;
    bool collisions = true;

    // cashe
    Movement movement;
    AudioSource myAudioSource;

    void Start()
    {
        movement = GetComponent<Movement>();
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ForceReload();
        ForceNextScene(); // [Debug]
        DisableCollisions(); // [Debug]
    }

    void ForceReload() // Push space to instantly reload level after crash or R to reload anytime 
    {
        if (isTransitioning & Input.GetKeyDown(KeyCode.Space))
        {
            ReloadScene();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
    } 

    void ForceNextScene() // [Debug] Push N to load next scene anytime 
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            LoadNextScene();
        }
    } 

    private void DisableCollisions() // [Debug] Push C to disable/enable collisions 
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisions = !collisions;
            Debug.Log("Collisions active: " + collisions);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || !collisions) return;

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

    void StartSuccessSequence()
    {
        Debug.Log("Player finished level");
        isTransitioning = true;
        movement.enabled = false; // to not allow player to move

        //special effects:
        movement.PlayingSFX(successSound, successSoundVolume, false);
        successParticleSystem.Play();

        //scene menagement
        Invoke(nameof(LoadNextScene), delayAfterSuccess);
    }

    void StartCrashSequence()
    {
        Debug.Log("Player crashed");
        isTransitioning = true;
        movement.enabled = false; // to not allow player to move

        //special effects:
        movement.PlayingSFX(crashSound, crashSoundVolume, false);
        crashParticleSystem.Play();

        //scene menagement
        Invoke(nameof(ReloadScene), delayAfterCrash);
    }

    void LoadNextScene()
    {
        var numberOfScenes = SceneManager.sceneCountInBuildSettings; 
        var nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene >= numberOfScenes) // if there is no more scenes, go back to first
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }        
    }

    void ReloadScene()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
