using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HealableGroup : MonoBehaviour
{
    [SerializeField] Transform body;
    [SerializeField] WoundedBody woundedBodyPrefabs;
    [SerializeField] Transform baseArea;

    [SerializeField] Transform woundedBodyContainer;

    private int maxNumberOfMember = 20;
    private int members = 20;

    private Vector3 startinglocalScale;

    private void Awake()
    {
        members = maxNumberOfMember;
        startinglocalScale = transform.localScale;

        if (woundedBodyContainer == null)
        {
            woundedBodyContainer = transform.Find("WoundedBodyContainer");
        }

        if (woundedBodyContainer == null)
        {
            var go = new GameObject();
            woundedBodyContainer = go.transform;
            woundedBodyContainer.name = "WoundedBodyContainerr";
           // woundedBodyContainer.SetParent(this.transform);
            woundedBodyContainer.localPosition = Vector3.zero;
        }
    }

        // Start is called before the first frame update
        void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnGetHit();
        }
    }

    public void OnGetHit()
    {
        Transform t = Instantiate(woundedBodyPrefabs.transform, woundedBodyContainer);
        t.position = this.transform.position;
        t.GetComponent<WoundedBody>().baseArea = baseArea.GetComponent<Collider2D>();
        t.GetComponent<WoundedBody>().hostArea = this.GetComponent<Collider2D>();
        t.GetComponent<WoundedBody>().OnWounded();
        t.GetComponent<WoundedBody>().HostGroup = this;
        members--;
        transform.localScale = startinglocalScale * members / maxNumberOfMember;
    }

    public void OnJoin()
    {
        members++;
        transform.localScale = startinglocalScale * members / maxNumberOfMember;
    }


}
