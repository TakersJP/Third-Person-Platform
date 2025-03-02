using UnityEngine;

public class CoinRotater : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f; // Speed of rotation

    void Update()
    {
        transform.localRotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
        Debug.Log("Coin Rotating");
    }
}