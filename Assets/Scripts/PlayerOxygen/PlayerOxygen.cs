using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOxygen : MonoBehaviour
{
    //Player's Oxygen cannot stop it's decaying compared to plants since they can be raised to a given height requirement

    public int playerFoodSupplyCount; //Used to get food supply when harvesting for plants that gives 1 food supply point
    public float OxygenDecayTime = 3.2f; //This value is for area if it doesnt have clouds yet
    public float oxygenCount = 1f; //Default value, the scaling is between 0 and 1
    public bool isWeatherNeutral; //Standard weather drain, for plant its 'isDecaying' bool variable
    public bool isWeatherCloudy; //Same but 'IsDecayingInCloud
    bool isWeatherCalled; //This is just used for counter in coroutine

    private void Start()
    {
        if (isWeatherNeutral)
        {
            InvokeRepeating("DrainOxygenNeutral", 1.3f, 2.6f);
        }
    }
    private void Update()
    {
        if (isWeatherCloudy && !isWeatherCalled)
        {
            isWeatherNeutral = false;
            StartCoroutine(DrainOxygen_Cloudy());
        }
        if (isWeatherNeutral && !isWeatherCalled)
        {
            isWeatherCloudy = false;
            StartCoroutine(DrainOxygen_Neutral());
        }
    }

    void DrainOxygenNeutral() //This is for the default weather for player when the level starts
    {
        Debug.Log($"Decaying, Oxygenleft {oxygenCount}");
        oxygenCount -= .020f;
    }
    IEnumerator DrainOxygen_Cloudy()
    {
        isWeatherCalled = true;
        Debug.Log($"Decaying, Oxygenleft {oxygenCount}");
        oxygenCount -= .045f;
        yield return new WaitForSeconds(1f);
        isWeatherCalled = false;
    }

    IEnumerator DrainOxygen_Neutral()
    {
        isWeatherCalled = true;
        Debug.Log($"Decaying, Oxygenleft {oxygenCount}");
        oxygenCount -= .020f;
        yield return new WaitForSeconds(1.3f);
        isWeatherCalled = false;
    }

    public void RefillOxygen()
    {
        Debug.Log($"Gained Oxygen Count by {.25f}");
        oxygenCount += .25f;
        if (oxygenCount >= 1f) oxygenCount = 1f;
    }
}
