using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static soundManager instance;
    public AudioSource source;
    public bool prvni = true;

    private void Awake()
    {

        instance = this;

        List<soundManager> sm = FindObjectsOfType<soundManager>().ToList();
        foreach (soundManager h in sm)
        {
            if (h != instance)
            {
                Destroy(h.gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayClip(AudioClip clip)
    {
        source.Stop();
        source.clip = clip;
        source.Play();

    }
    public void stopClip()
    {
        source.Stop();
    }
   
}
