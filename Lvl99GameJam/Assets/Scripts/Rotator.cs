using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Transform rotateParent;
    public GameObject fishObject;

    public bool clockwise;
    private bool rotating = false;

    public float timeToRotate;
    private bool noConflicts;

    public List<Elevator> elevators;

    private Quaternion currentRotation;
    private BoxCollider2D boxCollider;

    private Vector3 angleToRotate = new Vector3(0, 0, -90);

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        foreach (Elevator elevator in elevators)
        {
            if (elevator.finishedAction)
                noConflicts = true;
        }

        if (!rotating && noConflicts)
        {
            if (!clockwise)
            {
                angleToRotate *= -1;
            }
            else
                angleToRotate *= 1;
            StartCoroutine(RotateLevel(angleToRotate));
        }
    }

    public IEnumerator RotateLevel(Vector3 angle)
    {
        currentRotation = rotateParent.rotation;
        Quaternion finalRotation = Quaternion.Euler(rotateParent.eulerAngles + angle);
        float timeElapsed = 0;
        boxCollider.enabled = false;
        rotating = true;
        GameManager.Instance.actionInProgress = true;
        fishObject.GetComponent<Rigidbody2D>().simulated = false;

        while (rotateParent.rotation != finalRotation && rotating)
        {
            rotateParent.rotation = Quaternion.Slerp(currentRotation, finalRotation, timeElapsed / timeToRotate);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        rotating = false;
        GameManager.Instance.actionInProgress = false;
        fishObject.GetComponent<Rigidbody2D>().simulated = true;
    }
}
