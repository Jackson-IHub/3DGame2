using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("The position you want the platform to move TO (relative to where it starts).")]
    [SerializeField] private Vector3 moveOffset = new Vector3(0, 0, 10);
    
    [Tooltip("How fast the platform moves.")]
    [SerializeField] private float speed = 2.0f;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private void Start()
    {
        // 1. Capture the starting position
        startPosition = transform.position;
        
        // 2. Calculate the end position based on the offset
        endPosition = startPosition + moveOffset;
    }

    private void Update()
    {
        // 3. Calculate the interpolation value (0 to 1 and back to 0)
        // Mathf.PingPong moves the value back and forth continuously
        float time = Mathf.PingPong(Time.time * speed, 1.0f);
        
        // 4. Move the platform smoothly between start and end
        transform.position = Vector3.Lerp(startPosition, endPosition, time);
    }

    // --- PLAYER STICKINESS LOGIC ---
    // This ensures the player moves WITH the platform

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Make the player a child of the platform
            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Remove the player from the platform so they move freely again
            other.transform.SetParent(null);
        }
    }

   

    // --- VISUALIZATION TOOL ---
    // This draws a line in the Editor so you can see the path
    private void OnDrawGizmos()
    {
        if (Application.isPlaying) return; // Only draw preview when not playing

        Vector3 start = transform.position;
        Vector3 end = start + moveOffset;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(start, end);
        Gizmos.DrawSphere(start, 0.1f);
        Gizmos.DrawSphere(end, 0.1f);
    }
}