using UnityEngine;

public class BallMotionEffect : MonoBehaviour
{
    private Rigidbody2D ballRb2d; // Reference to the Rigidbody2D of the Ball
    private Vector2 initialDirection = new Vector2(1, 1).normalized; // Normalized initial direction of the sprite

    void Start ()
    {
        ballRb2d = transform.parent.GetComponent<Rigidbody2D>();

        if (ballRb2d == null)
        {
            Debug.LogError("No Rigidbody2D component found on the parent GameObject.");
            enabled = false;
            return;
        }

        SetVisibility(false);
    }

    void Update ()
    {
        SetVisibility(ballRb2d.velocity != Vector2.zero);

        if (ballRb2d.velocity != Vector2.zero)
        {
            float angle = Vector2.SignedAngle(initialDirection, ballRb2d.velocity);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void SetVisibility ( bool isVisible )
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = isVisible;
    }
}
