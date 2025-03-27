using UnityEngine;

public class Ammo : MonoBehaviour
{    
    float duration = 3;
    public bool shot;
    public Vector3 throwOrigin;
    public delegate void HitEventHandler();
    public event HitEventHandler OnAmmoHit;
    public delegate void HitTargetEventHandler(int id);
    public event HitTargetEventHandler OnAmmoHitTarget;

    void Start()
    {

    }

    // called once a frame
    void Update()
    {
        if (shot)
        {
            transform.Rotate(360 *Time.deltaTime , 0, 0);
            duration -= Time.deltaTime;
            if (duration <= 0)
            {
                AmmoHit();
                shot = false;
            }
        }
    }

    void AmmoHit()
    {
        OnAmmoHit?.Invoke();
        OnAmmoHit = null;
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!shot)
            return;
        if (other.GetComponent<Target>() != null)
        {
            OnAmmoHitTarget?.Invoke(other.GetComponent<Target>().ID);
            other.GetComponent<Target>().ReceiveDamage(100, throwOrigin);            
        }
        AmmoHit();
    }
}

