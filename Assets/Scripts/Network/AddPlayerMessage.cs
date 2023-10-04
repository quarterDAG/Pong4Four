using Mirror;

namespace Mirror.Examples.Pong
{
    public struct AddPlayerMessage : NetworkMessage
    {
        public int playerPrefabIndex;
    }

}