using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(ChangeShape))]
public class Body : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] Transform bodyPrefab;
    [SerializeField] List<Transform> bodies;

    private void Awake()
    {
        if (container == null)
        {
            container = transform.Find("Container");
        }

        if (container == null)
        {
            var go = new GameObject();
            container = go.transform;
            container.name = "Container";
            container.SetParent(this.transform);
            container.localPosition = Vector3.zero;
        }

        if (bodyPrefab == null)
        {
            bodyPrefab = transform.Find("BodyPrefab");

        }

        if (bodyPrefab == null)
        {
            var go = new GameObject();
            bodyPrefab = go.transform;
            bodyPrefab.name = "BodyPrefab";
            bodyPrefab.SetParent(this.transform);
            bodyPrefab.localPosition = Vector3.zero;

        }

        if (bodyPrefab.GetComponent<SpriteRenderer>() == null)
        {
            bodyPrefab.gameObject.AddComponent<SpriteRenderer>();
        }

        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }

        bodyPrefab.gameObject.SetActive(true);
        bodies = new List<Transform>();
        var body = Instantiate(bodyPrefab, container.transform);
        bodies.Add(body);
        OnBodyCountChanged();
        bodyPrefab.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ReadKeysInput();
    }

    private void ReadKeysInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) ^ Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)) { Split(); };
            if (Input.GetKeyDown(KeyCode.LeftArrow)) { Combine(); };
        }
    }

    private void Split()
    {


        var body = Instantiate(bodyPrefab, container.transform);
        body.gameObject.SetActive(true);
        bodies.Add(body);
        body.localPosition = GetBodyPosition(bodies.Count );
        OnBodyCountChanged();
    }

    private void Combine()
    {
        if ( bodies == null || bodies.Count <= 1) { return; }

        var body = bodies[bodies.Count - 1];
        bodies.RemoveAt(bodies.Count - 1);
        Destroy(body.gameObject);

        OnBodyCountChanged();
    }

    private void OnBodyCountChanged()
    {
        GetComponent<ChangeShape>().Images.Clear();
        foreach (var item in bodies)
        {
            GetComponent<ChangeShape>().Images.Add(item.GetComponent<SpriteRenderer>());
        }

        ChangeSize();
    }

    private void ChangeSize()
    {
        float size = 0.75f / bodies.Count  + 0.25f;
        foreach (var item in bodies)
        {
            item.localScale = Vector3.one * size;
        }
    }

    private Vector3 GetBodyPosition(int count , float specing = 1.1f)
    {
        Vector3 result = Vector3.zero;

        if (count <= 1 ) { return Vector3.zero; } 

        int r = 0;
        int d = 0;
        int ld = 0;

        for (int n = 1;  n < 5;  n++)
        {
            r = n;
            ld = (n - 1) * 2 + 1;
            d = n * 2 + 1;
            if( count <= d * d)
            {
                break;
            }
        }

        if (count <= (ld *ld + 2 * ld))
        {
            result.x = (count % 2 == 0) ? r  : - r;
            result.y = -1 *( (count -1 - ld * ld) / 2 );
        }

        else
        {
            int temp = (count -1) % d ;
            result.x = (temp  % 2 == 0) ? Mathf.FloorToInt(((float)temp + 1f) / 2) : -1 * Mathf.FloorToInt(((float)temp + 1f) / 2);
            result.y = -1 * (((count - 1) / d) );
        }

        result *= specing;

        return result;
    }

}
