using System;

using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private LayerMask damageableLayer = default;

    private Action<EnemyController> onDespawn = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CheckLayerInMask(damageableLayer, collision.gameObject.layer))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.HitPlayer();
            }

            onDespawn?.Invoke(this);
        }
    }

    public void Init(Action<EnemyController> onDespawn)
    {
        this.onDespawn = onDespawn;
    }

    public void Get()
    {
        gameObject.SetActive(true);
    }

    public void Release()
    {
        gameObject.SetActive(false);
    }

    private bool CheckLayerInMask(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}
