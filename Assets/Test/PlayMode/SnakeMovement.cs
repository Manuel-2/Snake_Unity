using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SnakeMovement
{
    [UnityTest]
    public IEnumerator SnakeCanNotMoveBackward()
    {
        // Arrange
        GameObject snakeBody = new GameObject();
        var controller = snakeBody.AddComponent<SnakeController>();
        controller.SetUpSnakeWhitDefaultConfiguration();
        var snakeHead = snakeBody.transform.Find("thehead");
        // Act
        yield return null;
        controller.setDirection(SnakeController.Directions.left);
        yield return null;
        controller.setDirection(SnakeController.Directions.down);
        yield return null;
        // Assert
        Assert.AreEqual(snakeHead.transform.position, new Vector3(-1, 1, 0));
        Debug.Log(snakeHead.transform.position);
    }
}
