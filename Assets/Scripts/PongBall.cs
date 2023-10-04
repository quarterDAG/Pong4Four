using UnityEngine;

namespace Mirror.Examples.Pong
{
    public class PongBall : NetworkBehaviour
    {
        public float speed = 5.0f; // Speed of the ball

        private Rigidbody2D rb2d;

        [SyncVar]
        private Vector2 initialDirection;


        public override void OnStartServer ()
        {
            rb2d = GetComponent<Rigidbody2D>();
            CountdownTimer.OnCountdownFinished += LaunchBall;
            ResetBall();
        }

        public override void OnStartClient ()
        {
            rb2d = GetComponent<Rigidbody2D>();
            CountdownTimer.OnCountdownFinished += LaunchBall;
        }


        void OnDestroy ()
        {
            CountdownTimer.OnCountdownFinished -= LaunchBall; // Unsubscribe to avoid memory leaks
        }

        [Server]
        void LaunchBall ()
        {
            // Generate a random angle in radians between 0 and 360 degrees.
            float angle = Random.Range(0, 360) * Mathf.Deg2Rad;

            // Convert angle to a direction vector
            initialDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            RpcLaunchBall(initialDirection);
        }

        [ClientRpc]
        void RpcLaunchBall ( Vector2 direction )
        {
            rb2d.velocity = direction.normalized * speed;
        }

        [Server]
        public void ResetBall ()
        {
            rb2d.velocity = Vector2.zero;
            transform.position = Vector2.zero;

            // Inform all clients about the reset
            RpcResetBall();

            CountdownTimer timer = FindObjectOfType<CountdownTimer>();

            if (timer != null)
                timer.RpcStartCountdown(); // Start countdown using the RPC

        }

        [ClientRpc]
        public void RpcResetBall ()
        {
            if (!isServer) // Since the server already did its reset in the Cmd method.
            {
                transform.position = Vector2.zero;
                rb2d.velocity = Vector2.zero;
            }
        }

        private void OnTriggerEnter2D ( Collider2D collision )
        {
            if (collision.gameObject.CompareTag("Goal"))
            {
                Debug.Log(collision);
                ResetBall();
            }
        }
    }

}