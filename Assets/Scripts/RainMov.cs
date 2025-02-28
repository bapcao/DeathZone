using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainMov : MonoBehaviour
{
    public Vector3 minBounds;
    public Vector3 maxBounds;
    public float speed = 2f;
    public GameObject rainArea;
    public AudioSource rainSound; // Add this line

    void Start()
    {
        if (rainArea == null)
        {
            Debug.LogError("RainArea GameObject not assigned in Inspector!");
            enabled = false;
            return;
        }
        if (rainSound == null)
        {
            Debug.LogError("Rain sound AudioSource not assigned in Inspector!");
            enabled = false;
            return;
        }
        SetRandomTargetPosition();
    }

    void Update()
    {
        rainArea.transform.position = Vector3.MoveTowards(rainArea.transform.position, targetPosition, speed * Time.deltaTime);
        if (rainArea.transform.position == targetPosition)
        {
            SetRandomTargetPosition();
        }

        // Audio Management
        UpdateRainSound();
    }

    void SetRandomTargetPosition()
    {
        targetPosition = new Vector3(
            Random.Range(minBounds.x, maxBounds.x),
            Random.Range(minBounds.y, maxBounds.y),
            Random.Range(minBounds.z, maxBounds.z)
        );
    }

    void UpdateRainSound()
    {
        // Get the distance between the listener (camera) and the rain area.
        float distance = Vector3.Distance(Camera.main.transform.position, rainArea.transform.position);

        // Adjust volume based on distance.  Experiment with these values.
        float maxDistance = 50f; // Adjust this to control the range of audibility
        float volume = Mathf.Clamp01(1 - (distance / maxDistance));
        rainSound.volume = volume;
    }

    private Vector3 targetPosition;
}