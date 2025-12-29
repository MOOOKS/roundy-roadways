using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class music : MonoBehaviour
{
    public static music instance;
    public AudioClip musicClip;
    public AudioSource musicSource;
    public float volume;

    public GameObject transitionObj;
    public Vector3 startPos;
    public float midPos;
    public float endPos;
    public float duration;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    void Start()
    {
        if (musicClip != null)
        {
            musicSource.clip = musicClip;      
            musicSource.Play();
        }
        
    }

    public IEnumerator transition(int sceneIndex)
    {
        // Move from offscreen to center
        RectTransform rect = transitionObj.GetComponent<RectTransform>();
        rect.anchoredPosition = startPos;
        transitionObj.LeanMoveLocalX(midPos, duration).setIgnoreTimeScale(true);
        yield return new WaitForSecondsRealtime(duration);

        // Load the new scene
        SceneManager.LoadScene(sceneIndex);        
        transitionObj.LeanMoveLocalX(endPos, duration).setIgnoreTimeScale(true);
        yield return new WaitForSecondsRealtime(duration);        
    }

}
