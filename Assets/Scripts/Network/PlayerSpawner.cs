using UnityEngine;
using Mirror;

namespace Mirror.Examples.Pong
{
    public class PlayerSpawner : NetworkBehaviour
    {
        [Client]
        public void AddPlayer ( int prefabIndex )
        {
            if (!isLocalPlayer)
                return;

            AddPlayerMessage msg = new AddPlayerMessage { playerPrefabIndex = prefabIndex };
            NetworkClient.Send(msg);
        }
    }

}
