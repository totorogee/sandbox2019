using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSetting : MonoBehaviour
{
    [SerializeField] List<Sprite> Shape;
    public static ShapeSetting Instance; 

    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetShape(int level)
    {
        level = Mathf.Clamp(level, 0, Shape.Count - 1);
        return Shape[level];
    }
}
