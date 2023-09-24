using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay=2f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip failureSFX;
    [SerializeField] ParticleSystem successParticleSystem;
    [SerializeField] ParticleSystem failureParticleSystem;

    AudioSource componentAudioSource;
    bool isTransisioning = false;
    bool collisionEnabled = true;

    void Start()
    {
        componentAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionEnabled = !collisionEnabled;
        }
    }

    void OnCollisionEnter(Collision otherActor)
    {
        if (!isTransisioning && collisionEnabled)
        {
            switch (otherActor.gameObject.tag)
            {  
                case "Friendly":
                    Debug.Log("Friendly collision");
                    break;
                case "Finish":
                    StartSuccessSequence();
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }
        
    }

    void StopPlayer()
    {
        componentAudioSource.Stop();
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<PlayerMovement>().StopAllBoosters();
    }

    void StartSuccessSequence() 
    { 
        StopPlayer();
        isTransisioning = true;
        componentAudioSource.PlayOneShot(successSFX);
        successParticleSystem.Play();
        Invoke("LoadNextLevel", levelLoadDelay); 
    }

    void StartCrashSequence()
    {
        StopPlayer();
        isTransisioning = true;
        componentAudioSource.PlayOneShot(failureSFX);
        failureParticleSystem.Play();
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) 
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

}
