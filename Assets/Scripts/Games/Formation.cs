using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int depth;

    public IEnumerator<Vector3> EvaluatePoints()
    {
        var middle = new Vector3(width * .5f, 0, depth * .5f);

        yield return Vector3.zero;
    }
}
