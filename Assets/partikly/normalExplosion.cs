using UnityEngine;

public class normalExplosion : Nuke
{
    [SerializeField]
    AudioSource source;
    public override void Prehraj()
    {
        source.clip = clip;
        source.Play();
    }
}
