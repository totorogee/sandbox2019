
using UnityEngine;
 using System.Collections;
 
[System.Serializable]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class SpriteMeshTest : MonoBehaviour
{


    public int scale = 1;
    public Material material;
    private Mesh tileMesh;
    private int v = 0;


    // Use this for initialization
    void Start()
    {
        this.tileMesh = new Mesh();
    }

    // Update is called once per frame
    void Update()
    {
        this.tileMesh = new Mesh();

        Vector3[] vertices = new Vector3[]
        {
             this.transform.InverseTransformPoint(new Vector3(this.transform.position.x, 0, this.transform.position.z)),
         this.transform.InverseTransformPoint(new Vector3(this.transform.position.x + scale, 0, this.transform.position.z)),
         this.transform.InverseTransformPoint(new Vector3(this.transform.position.x, 0, this.transform.position.z + scale))
        };
        v++;
        Vector2[] uv = new Vector2[]
        {
             new Vector2(v, 256), new Vector2(256, 256), new Vector2(256, v)
        };

        int[] tris = new int[]
        {
             2,1,0
        };
        Debug.Log(this.tileMesh);
        this.tileMesh.vertices = vertices;

        this.tileMesh.uv = uv;
        this.tileMesh.triangles = tris;
        this.GetComponent<MeshFilter>().mesh = this.tileMesh;
        this.GetComponent<MeshRenderer>().material = this.material;
    }

}