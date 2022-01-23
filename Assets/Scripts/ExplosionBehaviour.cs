using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float timeToPropagate = 1;
    [SerializeField] private int distanceToPropagate = 3;
    private int directionsEnded = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateExplosions(Vector3.up));
        StartCoroutine(CreateExplosions(Vector3.left));
        StartCoroutine(CreateExplosions(Vector3.down));
        StartCoroutine(CreateExplosions(Vector3.right));
    }

    private void Update()
    {
        if(directionsEnded == 4)
        {
            StartCoroutine(DestroyAfterExplosion());
        }
    }

    private IEnumerator CreateExplosions(Vector3 direction)
    {
        Quaternion rotation = new();
        if (direction == Vector3.right) rotation.eulerAngles = new Vector3(0, 0, 90);
        else if (direction == Vector3.up) rotation.eulerAngles = new Vector3(0, 0, 180);
        else if (direction == Vector3.left) rotation.eulerAngles = new Vector3(0, 0, -90);
        else if (direction == Vector3.down) rotation.eulerAngles = new Vector3(0, 0, 0);

        for (int i = 0; i <= distanceToPropagate; i++)
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(transform.position, direction, (float)i, LayerMask.GetMask("Walls"));

            if (!hit.collider)
            {
                Instantiate(explosionPrefab, transform.position + (i * direction), rotation, transform);
            }

            yield return new WaitForSeconds(timeToPropagate);
        }
        directionsEnded++;

        yield return new WaitForSeconds(timeToPropagate);
    }
    private IEnumerator DestroyAfterExplosion()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
