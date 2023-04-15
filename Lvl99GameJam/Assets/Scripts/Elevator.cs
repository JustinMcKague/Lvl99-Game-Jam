using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Vector2 distanceToTravelY;
    public float timeToElevate;
    private float elapsedTime = 0f;

    [Tooltip("When true, the elevator goes from neutral to lifted. If false, the elevator starts at a lifted position and lowers on activation.")]
    public bool startNeutral = true;

    private bool finishedAction = false;

    private BoxCollider2D trigger;

    private Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<BoxCollider2D>();
        startPos = transform.position;
        timeToElevate *= 60;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LiftElevator());
        trigger.enabled = false;
    }

    private IEnumerator LiftElevator()
    {
        while (elapsedTime < timeToElevate)
        {
            transform.position = Vector2.Lerp(transform.position, startPos + distanceToTravelY, (elapsedTime / timeToElevate));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        yield return null;
        transform.position = startPos + distanceToTravelY;
    }
}
