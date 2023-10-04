using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.Examples.Pong
{
    public class Goal : MonoBehaviour
    {
        public int playerNumber; // Set this in the Inspector to represent which player this goal belongs to.
        private Collider2D goalCollider;

        private void Awake ()
        {
            goalCollider = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D ( Collider2D collision )
        {
            if (collision.CompareTag("Ball"))
            {
                ScoreManager.Instance.PlayerHit(playerNumber, goalCollider);
                collision.GetComponent<PongBall>().ResetBall(); // Reset ball position and velocity
            }
        }
    }
}