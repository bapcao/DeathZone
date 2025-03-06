using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform player; // Nhân vật
    public GameObject[] mapChunks; // Danh sách Prefab cho map chunks
    public float chunkSize = 50f; // Kích thước mỗi chunk
    public int renderDistance = 3; // Số chunks xung quanh nhân vật
    public float renderThreshold = 60f; // Khoảng cách tối đa để giữ chunk

    private Dictionary<Vector2, GameObject> activeChunks = new Dictionary<Vector2, GameObject>();
    private Queue<GameObject> chunkPool = new Queue<GameObject>(); // Object Pool
    private Vector2 currentChunkPosition;

    void Update()
    {
        Vector2 newChunkPosition = new Vector2(
            Mathf.FloorToInt(player.position.x / chunkSize),
            Mathf.FloorToInt(player.position.z / chunkSize)
        );

        if (newChunkPosition != currentChunkPosition)
        {
            currentChunkPosition = newChunkPosition;
            UpdateMapChunks();
        }
    }

    void UpdateMapChunks()
    {
        HashSet<Vector2> neededChunks = new HashSet<Vector2>();

        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int z = -renderDistance; z <= renderDistance; z++)
            {
                Vector2 chunkPosition = new Vector2(currentChunkPosition.x + x, currentChunkPosition.y + z);
                neededChunks.Add(chunkPosition);
                if (!activeChunks.ContainsKey(chunkPosition))
                {
                    SpawnChunk(chunkPosition);
                }
            }
        }

        List<Vector2> chunksToRemove = new List<Vector2>();
        foreach (var chunk in activeChunks)
        {
            float distance = Vector2.Distance(chunk.Key * chunkSize, currentChunkPosition * chunkSize);
            if (!neededChunks.Contains(chunk.Key) || distance > renderThreshold)
            {
                chunksToRemove.Add(chunk.Key);
            }
        }

        foreach (var chunkPosition in chunksToRemove)
        {
            RecycleChunk(chunkPosition);
        }
    }

    void SpawnChunk(Vector2 chunkPosition)
    {
        GameObject chunk;
        if (chunkPool.Count > 0)
        {
            chunk = chunkPool.Dequeue();
            chunk.SetActive(true);
        }
        else
        {
            chunk = Instantiate(mapChunks[Random.Range(0, mapChunks.Length)]);
        }

        Vector3 randomOffset = new Vector3(
            Random.Range(-chunkSize / 4, chunkSize / 4),
            0f,
            Random.Range(-chunkSize / 4, chunkSize / 4)
        );
        Vector3 worldPos = new Vector3(chunkPosition.x * chunkSize, 100f, chunkPosition.y * chunkSize) + randomOffset;

        if (Physics.Raycast(worldPos, Vector3.down, out RaycastHit hit, 200f))
        {
            worldPos.y = hit.point.y;
        }
        else if (Terrain.activeTerrain != null)
        {
            worldPos.y = Terrain.activeTerrain.SampleHeight(worldPos);
        }
        else
        {
            worldPos.y = 0f;
        }

        chunk.transform.position = worldPos;
        activeChunks[chunkPosition] = chunk;
    }

    void RecycleChunk(Vector2 chunkPosition)
    {
        if (activeChunks.TryGetValue(chunkPosition, out GameObject chunk))
        {
            chunk.SetActive(false);
            chunkPool.Enqueue(chunk);
            activeChunks.Remove(chunkPosition);
        }
    }
}