using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneBehaviour : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float timeToExplosion = 1;
    public Color circle_color = new Color(1, 1, 1, 1);

    [SerializeField] private GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = circle_color;
        StartCoroutine(TimeToExplosion());
    }

    private IEnumerator TimeToExplosion()
    {
        yield return new WaitForSeconds(timeToExplosion);
        Explode();
    }

    private void Explode()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
