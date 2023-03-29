using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource m_Source;
    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }


        else
            Destroy(this.gameObject);

    }
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        m_Source= GetComponent<AudioSource>();
        m_Source.Play();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayAnimeowIntro()
    {
        m_Source.Play();
    }
    public void StopAnimeowIntro()
    {
        m_Source.Stop();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "MapScene")
            AudioManager.Instance?.PlayAnimeowIntro();
    }
}
