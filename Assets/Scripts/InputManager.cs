using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] string verticalAxe;
    [SerializeField] string horizontalAxe;

    float verticalInput;
    float horizontalInput;
    private void Update()
    {
        verticalInput = Input.GetAxisRaw(verticalAxe);
        horizontalInput = Input.GetAxisRaw(horizontalAxe);

        if (verticalInput == 1)
        {
            SnakeController.sharedInstance.setDirection(SnakeController.Directions.up);
        }
        else if (horizontalInput == 1)
        {
            SnakeController.sharedInstance.setDirection(SnakeController.Directions.right);
        }
        else if (verticalInput == -1)
        {
            SnakeController.sharedInstance.setDirection(SnakeController.Directions.down);
        }
        else if (horizontalInput == -1)
        {
            SnakeController.sharedInstance.setDirection(SnakeController.Directions.left);
        }
    }
}
