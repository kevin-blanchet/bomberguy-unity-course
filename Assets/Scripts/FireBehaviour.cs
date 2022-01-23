using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    [SerializeField] private float timeToExplosion = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeToExplosion());
    }

    private IEnumerator TimeToExplosion()
    {
        yield return new WaitForSeconds(timeToExplosion);
        Destroy(gameObject);
    }
}
