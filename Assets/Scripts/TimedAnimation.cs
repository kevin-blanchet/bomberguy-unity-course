using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedAnimation : MonoBehaviour
{
    private bool nextFrameIsRunning = false;

    private int currentSprite = 0;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float timer = 1;
    [SerializeField] private Sprite[] spriteList;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!nextFrameIsRunning)
        {
            StartCoroutine(NextFrame());
        }
    }

    private IEnumerator NextFrame()
    {
        nextFrameIsRunning = true;
        yield return new WaitForSeconds(timer / spriteList.Length);
        currentSprite++;
        if (currentSprite < spriteList.Length)
        {
            spriteRenderer.sprite = spriteList[currentSprite];
        }
        nextFrameIsRunning = false;
    }
}
