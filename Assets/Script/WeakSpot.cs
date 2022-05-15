using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    public GameObject obletToDestroy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(obletToDestroy);
        }
    }
}
