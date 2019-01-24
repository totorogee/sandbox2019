using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeShape : MonoBehaviour
{
    private float shapeNow = 0;
    [SerializeField] public List<SpriteRenderer> Images = new List<SpriteRenderer>();

    // Start is called before the first frame update
    void Start()
    {
        if (Images == null || Images.Count == 0)
        {
            if (GetComponent<SpriteRenderer>() != null)
                Images.Add(GetComponent<SpriteRenderer>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        ReadKeysInput();
    }

    private void ReadKeysInput()
    {
        if (Input.GetKey(KeyCode.DownArrow) ^ Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.DownArrow)) { shapeNow -= 0.1f; };
            if (Input.GetKey(KeyCode.UpArrow)) { shapeNow += 0.1f; };

            shapeNow = Mathf.Clamp(shapeNow, 0, 13); // TODO : HARD CODE
        }

        foreach (var item in Images)
        {
            item.sprite = ShapeSetting.Instance.GetShape( Mathf.RoundToInt(shapeNow));
        }
    }
}
