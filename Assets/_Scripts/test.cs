using UnityEngine;
using System.Collections;

public class test : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {
        /*
        Vector3[] verts = new Vector3[] { Vector3.up, Vector3.right, Vector3.down, Vector3.left };
        int[] indicesForLines = new int[] { 0, 1, 1, 2, 2, 3, 3, 0 };
        Mesh mesh = new Mesh();
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = verts;
        mesh.SetIndices(indicesForLines, MeshTopology.Lines, 0);

        //mesh.RecalculateNormals();

        mesh.RecalculateBounds();
        */
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Vector3.SmoothDamp(transform.rotation, targetPosition, velocity, smoothTime);

        //if (lokat) SmoothLookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
    }

}