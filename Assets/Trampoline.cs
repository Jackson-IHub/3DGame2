using StarterAssets;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Header("Trampoline Settings")]
    [Tooltip("How high/fast the player is launched.")]
    [SerializeField] private float bounceForce = 15f;

    [Tooltip("If true, launches along the object's Up direction (good for angled bounce pads). If false, always launches straight up.")]
    [SerializeField] private bool useLocalUp = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            Debug.Log("player player player");
            // 2. Get the Rigidbody component from the player
            CharacterController playerRb = other.gameObject.GetComponent<CharacterController>();

            ThirdPersonController controller = other.gameObject.GetComponent<ThirdPersonController>();
            if (playerRb != null)
            {
                ApplyBounce(playerRb);
                controller.Gravity = -4f;
            }
        }
    }

    private void ApplyBounce(CharacterController rb)
    {
        // 3. Reset vertical velocity for consistent jump height
        // This prevents the "dampening" effect if falling from a great height
        Vector3 velocity = rb.velocity;
        velocity.y = 0; 

        // 4. Determine direction
        Vector3 bounceDirection = useLocalUp ? transform.up : Vector3.up;

        // 5. Apply the force
        // ForceMode.Impulse is best for instant explosion/jump effects
        rb.Move(bounceDirection * bounceForce);
        
    }
}