using UnityEngine;
using System.Collections.Generic;

public class GoldItem : MonoBehaviour
{
    public int goldAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GoldManager.Instance.AddGold(goldAmount);
            Destroy(gameObject);
        }
    }
}
