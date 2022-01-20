using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameTest
{

    [UnityTest]
    public IEnumerator GameManagerCanGenerateRamdomPositionsForApples()
    {
        // Arrange
        GameObject manager = new GameObject();
        var controller = manager.AddComponent<GameManager>();
        Vector2 min = new Vector2(0, 0);
        Vector2 max = new Vector2(22, 15);

        // Act
        int corrdinatesAmount = 100;
        int correctCorrdinates = 0;

        for (int i = 0; i < corrdinatesAmount; i++)
        {
            Vector3 GeneratedPosition = controller.GenerateRandomPosition(min, max);
            if (GeneratedPosition.x >= min.x && GeneratedPosition.x <= max.x)
            {
                if (GeneratedPosition.y >= min.y && GeneratedPosition.y <= max.y)
                {
                    correctCorrdinates++;
                }
            }
        }

        yield return null;
        // Assert
        Assert.AreEqual(correctCorrdinates, corrdinatesAmount); ;

    }

}
