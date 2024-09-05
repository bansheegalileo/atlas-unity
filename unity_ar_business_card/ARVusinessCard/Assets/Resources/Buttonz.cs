using UnityEngine;

public class OpenURLButton : MonoBehaviour
{
    // URL to open
    public string url;

    // Method to open the URL
    public void OpenURL()
    {
        Application.OpenURL(url);
    }
}
