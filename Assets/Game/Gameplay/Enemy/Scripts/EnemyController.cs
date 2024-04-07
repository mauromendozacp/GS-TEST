using System;

using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private LayerMask damageableLayer = default;

    private Action<EnemyController> onDespawn = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CheckLayerInMask(damageableLayer, collision.gameObject.layer))
        {
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
