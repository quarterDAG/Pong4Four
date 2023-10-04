using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using System.Collections;
using Mirror;

namespace Mirror.Examples.Pong
{
    public class CountdownTimer : NetworkBehaviour
    {
        public TextMeshProUGUI countdownText; // Reference to TextMeshProUGUI component for showing countdown
        public float countdownTime = 3f;

        public delegate void CountdownFinished ();
        public static event CountdownFinished OnCountdownFinished; // Event invoked when countdown finishes

        [ClientRpc]
        public void RpcStartCountdown ()
        {
            StartCountdown();
        }

        public void StartCountdown ()
        {
            StartCoroutine(Countdown());
        }

        private IEnumerator Countdown ()
        {
            float countdown = countdownTime;
            while (countdown > 0)
            {
                if (countdownText != null)
                    countdownText.text = countdown.ToString("0");
                yield return new WaitForSeconds(1f);
                countdown--;
            }
            if (countdownText != null)
                countdownText.text = "";

            OnCountdownFinished?.Invoke(); // Notify listeners that the countdown has finished
        }
    }
}