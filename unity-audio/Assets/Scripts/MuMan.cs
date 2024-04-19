using UnityEngine;
using UnityEngine.Audio;
public class MuMan : MonoBehaviour
{
    public AudioSource BGMusic;
    public AudioSource DUBMusic;

    private void Start()
    {
        BGMusic = GetComponentInChildren<AudioSource>();
        if (BGMusic == null)
        {
            BGMusic = GetComponent<AudioSource>();
        }
    }

    public void StartBGM()
    {
        if (BGMusic != null)
        {
            BGMusic.Play();
        }
                else
        {
            Debug.LogWarning("Oopsie");
        }
    }

    public void StopBGM()
    {
        if (BGMusic != null)
        {
            BGMusic.Stop();
        }
    }
    public void Winsong()
    {
        if (DUBMusic != null)
        {
            DUBMusic.Play();
        }
        else
        {
            Debug.LogWarning("Oopsie");
        }
    }
}
