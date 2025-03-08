using UnityEngine;
using UnityEngine.UI;

public class AmmoSupply : MonoBehaviour
{
    public GameObject ammoPrefab; // Prefab c?a h?p ??n
    public Transform spawnPoint;  // N?i xu?t hi?n h?p ??n (có th? là v? trí NPC)
    public GameObject supplyButton; // Nút UI ?? b?m

    private bool playerNearby = false;

    private void Start()
    {
        supplyButton.SetActive(false); // ?n nút ban ??u
    }

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E)) // Nh?n 'E' ?? nh?n ??n
        {
            SpawnAmmo();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            supplyButton.SetActive(true); // Hi?n nút
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            supplyButton.SetActive(false); // ?n nút khi r?i xa
        }
    }

    public void SpawnAmmo()
    {
        if (ammoPrefab != null && spawnPoint != null)
        {
            Instantiate(ammoPrefab, spawnPoint.position, Quaternion.identity);
        }
        supplyButton.SetActive(false); // ?n nút sau khi b?m
    }
}
