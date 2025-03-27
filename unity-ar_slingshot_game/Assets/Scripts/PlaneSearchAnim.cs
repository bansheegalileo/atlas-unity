using UnityEngine;

public class PlaneSearchAnim : MonoBehaviour
{
    // called b4 1st frame update
    public AudioSource radar;

    // called once a frame
    public void PlaySound()
    {
        radar.Play();
    }
}
