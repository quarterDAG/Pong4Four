using Mirror;
using Unity.VisualScripting;
using UnityEngine;

namespace Mirror.Examples.Pong
{
    public class NetworkManagerCustomPong : NetworkManager
    {
        public Transform[] playerSpawns; // Assign the spawn positions in the inspector
        public GameObject[] playerPrefabs; // Assign your player prefabs here in the inspector

        private int connectedPlayersCount = 0;
        GameObject ball;
        GameObject countdownTimer;

        public override void OnServerConnect ( NetworkConnectionToClient conn )
        {
            Debug.Log("Adding Player, Connected Players Count: " + connectedPlayersCount);

            if (connectedPlayersCount < playerPrefabs.Length && connectedPlayersCount < playerSpawns.Length)
            {
                playerPrefab = playerPrefabs[connectedPlayersCount];
            }
            else
            {
                Debug.LogError("Cannot instantiate player, check your playerPrefabs and playerSpawns arrays. Ensure they are set properly in the inspector");
            }
        }

        public override void OnServerAddPlayer ( NetworkConnectionToClient conn )
        {
            Debug.Log("Adding Player, Connected Players Count: " + connectedPlayersCount);

            if (connectedPlayersCount < playerPrefabs.Length && connectedPlayersCount < playerSpawns.Length)
            {
                Transform start = playerSpawns[connectedPlayersCount];
                GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
                NetworkServer.AddPlayerForConnection(conn, player);
                connectedPlayersCount++;
            }
            else
            {
                Debug.LogError("Cannot instantiate player, check your playerPrefabs and playerSpawns arrays. Ensure they are set properly in the inspector");
            }


            if (connectedPlayersCount == 4)
            {
                Debug.Log("Instantiating Ball and Timer");

                countdownTimer = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Countdown Timer"));
                //countdownTimer.transform.SetParent(ScoreManager.Instance.transform);
                NetworkServer.Spawn(countdownTimer);

                ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"));
                NetworkServer.Spawn(ball);

            }
        }

        public override void OnServerDisconnect ( NetworkConnectionToClient conn )
        {
            if (ball != null) NetworkServer.Destroy(ball);
            connectedPlayersCount = Mathf.Max(connectedPlayersCount - 1, 0);

            Debug.Log("Player Disconnected, Connected Players Count: " + connectedPlayersCount);

            base.OnServerDisconnect(conn);
        }
    }
}
