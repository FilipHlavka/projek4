using UnityEngine;
using static Unity.VisualScripting.Member;

public class Firework : MonoBehaviour
{
    [SerializeField]
    AudioClip clip;
    [SerializeField]
    AudioSource source; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("End",4.8f);
        PlayClip(clip);
    }
    public void End()
    {
        stopClip();
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
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
