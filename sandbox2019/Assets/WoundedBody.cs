using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WoundedBodyState { Inactive , Wounded , Healing , Back , Done}


public class WoundedBody : MonoBehaviour
{
    public HealableGroup HostGroup;
    public Collider2D baseArea;
    public Collider2D hostArea;

    private Collider2D moveTarget;
    private Collider2D myCollider;

    private Vector3 targetPoint;
    private WoundedBodyState myState = WoundedBodyState.Inactive;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    private void Move(float speed = 2f)
    {
        if (moveTarget == null) { return; }

        targetPoint = moveTarget.ClosestPoint(this.transform.position);
        transform.up = targetPoint - transform.position;
        transform.Translate(transform.up * Time.fixedDeltaTime * speed , Space.World);
    }

    public void OnWounded()
    {
        myState = WoundedBodyState.Wounded;
        moveTarget = baseArea.GetComponent<Collider2D>();
    }

    public void OnHealing()
    {
        moveTarget = null;
        StartCoroutine(DelayBack());

    }

    private IEnumerator DelayBack(float delayTime = 2f)
    {
        yield return new WaitForSeconds(delayTime);
        OnBack();
    }

    public void OnBack()
    {
        myState = WoundedBodyState.Back;
        moveTarget = hostArea.GetComponent<Collider2D>();
    }


    private void StageUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("here");
        if (other == baseArea)
        {
            OnHealing();
        }

        if ( myState == WoundedBodyState.Back && other == hostArea)
        {
            HostGroup.OnJoin();
            Destroy(this.gameObject);
        }
    }

    public void OnDrawGizmos()
    {

       // Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.DrawWireSphere(targetPoint, 0.1f);
    }

}
