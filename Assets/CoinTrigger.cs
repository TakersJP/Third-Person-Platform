using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    private int coinCount = 0;

    private void OnTriggerEnter(Collider triggeredObject)
    {
        if (triggeredObject.CompareTag("Coin"))
        {
            Collider coinCollider = triggeredObject.GetComponent<Collider>();
            if (coinCollider != null)
            {
                coinCollider.enabled = false;
            }

            coinCount++;
            Debug.Log("Coins Collected: " + coinCount);
            Destroy(triggeredObject.gameObject); 
        }
    }
}
