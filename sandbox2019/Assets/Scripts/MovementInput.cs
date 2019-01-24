using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MovementInput : MonoBehaviour
{
    [SerializeField] private List<Transform> Bodys = new List<Transform>();

    [SerializeField] private Transform dummyTarget;

    private Rigidbody2D rigidbody2D;


    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Bodys == null || Bodys.Count == 0)
        {
            Bodys.Add(this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ReadKeysInput();

      //  transform.up = Vector3.zero - transform.position;
    }

    private void ReadKeysInput()
    {
        float x =0 , y = 0;

        if (Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A)) { x = -1; };
            if (Input.GetKey(KeyCode.D)) { x = 1; };
        }

        if (Input.GetKey(KeyCode.S) ^ Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.S)) { y = -1; };
            if (Input.GetKey(KeyCode.W)) { y = 1; };
        }

        // Move(x, y);
        RDMove(x, y);


    }

    //private void Move(float x , float y , float speed = 0.2f)
    //{
    //    if (x == 0 && y == 0) { return; }

    //    this.transform.Translate(new Vector3(x * speed, y * speed, 0) , Space.World);

    //}
    
    private void RDMove(float x, float y)
    {
        float forceX = 10f;
        float forceY = 10f;

        float drag = 0.01f;

        float maxSpeedX = 5f;
        float maxSpeedY = 5f;

        rigidbody2D.AddForce(new Vector2(x * forceX, y * forceY));

        rigidbody2D.velocity = rigidbody2D.velocity * (1f-drag);
        if (rigidbody2D.velocity.x > maxSpeedX)
        {
            rigidbody2D.velocity = new Vector2( maxSpeedX , rigidbody2D.velocity.y) ;
        }
        if (rigidbody2D.velocity.y > maxSpeedY)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x , maxSpeedY );
        }

    }

}
