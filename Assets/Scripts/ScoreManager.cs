using UnityEngine;
using TMPro;
using Mirror;

public class ScoreManager : NetworkBehaviour
{
    public static ScoreManager Instance;

    public PlayerHealth[] playersHealth;
    public Sprite[] healthSprites; // Populate this array with your health sprites in the inspector

    void Awake ()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // This function will handle when any player's health changes
    private void OnHitpointsChanged ( int playerNumber, int newHitpoints )
    {
        UpdatePlayerHealthUI(playerNumber, newHitpoints);
    }

    [Server]
    public void PlayerHit ( int playerNumber, Collider2D goalCollider )
    {
        if (playerNumber < 1 || playerNumber > playersHealth.Length) return;

        playersHealth[playerNumber - 1].goalCollider = goalCollider;
        playersHealth[playerNumber - 1].TakeDamage(1);

        // Notify all clients to update the player's health UI
        RpcUpdateHealthUI(playerNumber, playersHealth[playerNumber - 1].hitpoints);
    }

    [ClientRpc]
    public void RpcUpdateHealthUI ( int playerNumber, int newHitpoints )
    {
        OnHitpointsChanged(playerNumber, newHitpoints);
    }

    void UpdatePlayerHealthUI ( int playerNumber, int newHitpoints )
    {
        // Ensure valid player number and that the sprite index does not exceed the bounds of the array
        if (playerNumber < 1 || playerNumber > playersHealth.Length || newHitpoints >= healthSprites.Length)
            return;

        playersHealth[playerNumber - 1].healthImage.sprite = healthSprites[newHitpoints];
    }
}
