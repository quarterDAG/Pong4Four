using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHitpointsChanged))]
    public int hitpoints = 5;

    public Image healthImage;
    public GameObject paddle;
    public Collider2D goalCollider;

    // Reference to the ScoreManager instance to access the health sprites.
    private ScoreManager scoreManager;

    private void Start ()
    {
        scoreManager = ScoreManager.Instance;
    }

    void OnHitpointsChanged ( int oldHitpoints, int newHitpoints )
    {
        if (scoreManager && scoreManager.healthSprites.Length > newHitpoints)
        {
            healthImage.sprite = scoreManager.healthSprites[newHitpoints];
        }
    }

    [Server]
    public void TakeDamage ( int damage )
    {
        hitpoints = Mathf.Max(0, hitpoints - damage);

        if (hitpoints == 0)
        {
            paddle.SetActive(false);
            goalCollider.isTrigger = false;
        }
    }
}
