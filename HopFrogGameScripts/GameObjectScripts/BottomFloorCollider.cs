using UnityEngine;

public class BottomFloorCollider : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            DeathManager.Instance.HandleDeath();
        }
    }
}
