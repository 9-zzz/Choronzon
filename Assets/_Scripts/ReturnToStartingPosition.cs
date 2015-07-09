using UnityEngine;
using System.Collections;

public class ReturnToStartingPosition : MonoBehaviour
{

    // Maybe require component RigidBody
    Vector3 startPosition, driftPosition;
    Quaternion startRotation, driftRotation;

    public float driftSeconds = 3;
    private float driftTimer = 0;

    public bool isDrifting;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void StartDrift()
    {
        isDrifting = true;
        driftTimer = 0;
        driftPosition = transform.position;
        driftRotation = transform.rotation;

        // Make sure has rigidbody
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void StopDrift()
    {
        isDrifting = false;
        driftTimer = 0;
        transform.position = startPosition;
        transform.rotation = startRotation;

        // Make sure has rigidbody
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            StartDrift();

        if (isDrifting)
        {
            driftTimer += Time.deltaTime;

            if (driftTimer > driftSeconds)
            {
                StopDrift();
            }
            else
            {
                float ratio = driftTimer / driftSeconds;
                transform.position = Vector3.Lerp(driftPosition, startPosition, ratio);
                transform.rotation = Quaternion.Slerp(driftRotation, startRotation, ratio);
            }
        }
    }

}
