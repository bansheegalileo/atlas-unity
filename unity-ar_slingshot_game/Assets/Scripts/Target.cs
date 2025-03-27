using UnityEngine;

public class Target : MonoBehaviour
{
    public AudioSource damgeSound;
    public int ID;
    public int points = 10;
    int health = 100;

    public delegate void TargetDestroyedEventHandler (int id, int points);
    public event TargetDestroyedEventHandler OnTargetDestroy;
    void Start()
    {
        damgeSound = GameObject.Find("SFX").transform.Find("Die").GetComponent<AudioSource>();
        damgeSound.playOnAwake = false;
    }
    public void ReceiveDamage(int damage, Vector3 shootOrigin)
    {
        health -= damage;
        if (health <= 0)
        {
            OnTargetDestroy?.Invoke(ID, Points());
            OnTargetDestroy = null;
            damgeSound.Play();
            Destroy(gameObject);
        }
    }

    int Points()
    {
        return points;
    }

}
