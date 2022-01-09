using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    GameObject koma;
    GameObject musicPlayer;

    //void Awake()
    //{
    //    SetupSingleton();
    //}

    //private void SetupSingleton()
    //{
    //    if (FindObjectsOfType(GetType()).Length > 1)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        koma = GameObject.Find("Koma");
        musicPlayer = GameObject.Find("Music Player");
    }

    public void LoadMenuScene()
    {
        if (koma) { Destroy(koma); }
        musicPlayer.GetComponent<MusicPlayer>().StopMusic();
        SceneManager.LoadScene("Menu");
    }

    public void LoadMakeScene()
    {
        if (koma) { Destroy(koma); }
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            musicPlayer.GetComponent<MusicPlayer>().PlayMusic(0);
        }
        SceneManager.LoadScene("Make");
    }

    public void LoadCheckScene()
    {
        SceneManager.LoadScene("Check");
    }

    public void LoadPlayScene()
    {
        musicPlayer.GetComponent<MusicPlayer>().PlayMusic(1);
        SceneManager.LoadScene("Play");
    }

}
