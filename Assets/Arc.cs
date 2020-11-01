using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arc : MonoBehaviour
{
    public IEnumerator TravelArc(Vector3 destination, float duration)
    {
        var startPos = transform.position;
        var percentComplete = 0f;
        while (percentComplete < 1f)
        {
            percentComplete += Time.deltaTime / duration;
            var curHeight = Mathf.Sin(Mathf.PI * percentComplete);
            transform.position = Vector3.Lerp(startPos, destination, percentComplete) + Vector3.up * curHeight;

            yield return null;
        }

        gameObject.SetActive(false);
    }
}
