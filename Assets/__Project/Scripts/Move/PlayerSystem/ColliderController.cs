using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    private PolygonCollider2D collider2D;
    [SerializeField]
    private DamageController damageController;
    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<PolygonCollider2D>();
        damageController = GetComponentInParent<DamageController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && damageController.isDamage)
        {
            damageController.RegisterDamage();
        }
    }
}
