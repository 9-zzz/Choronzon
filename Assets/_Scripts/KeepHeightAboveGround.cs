using UnityEngine;
using System.Collections;

public class KeepHeightAboveGround : MonoBehaviour
{

    RaycastHit hit;
    public float heightAboveGround;
    public bool reachedPos = false;
    public bool lockedToGround = false;
    public Vector3 desiredPos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float step = 2 * Time.deltaTime;

        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
            desiredPos = new Vector3(hit.point.x, hit.point.y + heightAboveGround, hit.point.z);

        if (!reachedPos)
            transform.position = Vector3.MoveTowards(transform.position, desiredPos, step);

        if (transform.position == desiredPos && !lockedToGround)
        {
            reachedPos = true;
            lockedToGround = true;
        }

        //Vector3.Lerp(transform.position, desiredPos, 0.45f * Time.deltaTime);

        if (reachedPos)
        {
            //if (hit.collider.gameObject.tag == "terrain")
            transform.position = Vector3.MoveTowards(transform.position, desiredPos, step * 2);
        }
        //transform.position = desiredPos;
    }

}
