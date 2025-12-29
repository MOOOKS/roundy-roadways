using UnityEngine;
using UnityEngine.SceneManagement;

public class playButton : MonoBehaviour
{
    public AudioClip musicSong;
    private void Start()
    {
        soundManager.instance.playSound(musicSong, transform, 0.7f, 1f, true);
    }
    public void playGame()
    {       
        StartCoroutine(music.instance.transition(SceneManager.GetActiveScene().buildIndex+1));
    }
}
