using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float timeToCompleteLevel;
    private float currentTimeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        currentTimeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTimeElapsed = Time.deltaTime;

        if (currentTimeElapsed > timeToCompleteLevel)
        {
            Debug.Log("Time limit reached");
        }
    }

    private void ResetTimer()
    {
        currentTimeElapsed = 0;
    }

    public bool TimeRemaining()
    {
        return currentTimeElapsed > timeToCompleteLevel;
    }
}
