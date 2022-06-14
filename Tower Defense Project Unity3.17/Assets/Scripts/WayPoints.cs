using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public static Transform[] transforms;

    private void Awake()
    {
        transforms = GetComponentsInChildren<Transform>();
        transforms = transforms.Skip(1).ToArray();
        //positions = positions.ToList().RemoveAt(0).ToArray();
    }
}
