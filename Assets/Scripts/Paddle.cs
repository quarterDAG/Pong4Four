using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public Player assignedPlayer; // This assigns the player to the paddle
    public enum Player
    {
        Player1,
        Player2,
        Player3,
        Player4,
    }

    public float speed = 10.0f; // Speed at which the paddle moves
    public bool isHorizontal = false; // Whether the paddle moves horizontally or vertically

    #region Field Boundaries
    public float boundaryTop = 4.0f; 
    public float boundaryBottom = -4.0f; 
    public float boundaryLeft = -4.0f; 
    public float boundaryRight = 4.0f;
    #endregion

    // Update is called once per frame
    void Update ()
    {
        string axis = assignedPlayer.ToString() + (isHorizontal ? "Horizontal" : "Vertical");
        float move = Input.GetAxis(axis) * speed * Time.deltaTime;

        if (isHorizontal)
        {
            Vector3 newPosition = transform.position + new Vector3(move, 0, 0);
            newPosition.x = Mathf.Clamp(newPosition.x, boundaryLeft, boundaryRight);
            transform.position = newPosition;
        }
        else
        {
            Vector3 newPosition = transform.position + new Vector3(0, move, 0);
            newPosition.y = Mathf.Clamp(newPosition.y, boundaryBottom, boundaryTop);
            transform.position = newPosition;
        }
    }
}
