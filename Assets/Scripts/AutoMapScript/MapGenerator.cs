using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform player; // Vị trí của nhân vật
    public GameObject[] mapChunks; // Danh sách các Prefab cho map chunks
    public float chunkSize = 50f; // Kích thước mỗi chunk
    public int renderDistance = 3; // Số chunks giữ lại xung quanh nhân vật

    private Dictionary<Vector2, GameObject> activeChunks = new Dictionary<Vector2, GameObject>();
    private Vector2 currentChunkPosition;

    void Update()
    {
        // Tính vị trí chunk hiện tại của nhân vật
        Vector2 newChunkPosition = new Vector2(
            Mathf.FloorToInt(player.position.x / chunkSize),
            Mathf.FloorToInt(player.position.z / chunkSize)
        );

        // Nếu nhân vật di chuyển sang chunk mới, cập nhật map
        if (newChunkPosition != currentChunkPosition)
        {
            currentChunkPosition = newChunkPosition;
            UpdateMapChunks();
        }
    }

    void UpdateMapChunks()
    {
        // Tạo các chunk mới xung quanh nhân vật
        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int z = -renderDistance; z <= renderDistance; z++)
            {
                Vector2 chunkPosition = new Vector2(currentChunkPosition.x + x, currentChunkPosition.y + z);

                if (!activeChunks.ContainsKey(chunkPosition))
                {
                    SpawnChunk(chunkPosition);
                }
            }
        }

        // Hủy các chunk xa ngoài renderDistance
        List<Vector2> chunksToRemove = new List<Vector2>();
        foreach (var chunk in activeChunks)
        {
            float distance = Vector2.Distance(chunk.Key, currentChunkPosition);
            if (distance > renderDistance)
            {
                chunksToRemove.Add(chunk.Key);
            }
        }

        foreach (var chunkPosition in chunksToRemove)
        {
            Destroy(activeChunks[chunkPosition]);
            activeChunks.Remove(chunkPosition);
        }
    }

    void SpawnChunk(Vector2 chunkPosition)
    {
        GameObject chunkPrefab = mapChunks[Random.Range(0, mapChunks.Length)];
        Vector3 worldPos = new Vector3(chunkPosition.x * chunkSize, 100f, chunkPosition.y * chunkSize); // Spawn từ trên cao

        RaycastHit hit;
        if (Physics.Raycast(worldPos, Vector3.down, out hit, 200f))
        {
            worldPos.y = hit.point.y; // Cập nhật độ cao theo va chạm với mặt đất
        }
        else
        {
            worldPos.y = Terrain.activeTerrain.SampleHeight(worldPos); // Dự phòng nếu Raycast thất bại
        }

        GameObject newChunk = Instantiate(chunkPrefab, worldPos, Quaternion.identity);
        activeChunks.Add(chunkPosition, newChunk);
    }
}
