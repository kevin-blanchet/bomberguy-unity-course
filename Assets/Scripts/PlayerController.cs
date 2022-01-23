using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject runePrefab;
    [SerializeField] private float speed = 1;

    private bool isMoving = false;
    private bool isFacingRight = true;
    private bool isCasting = false;

    private InputAction move;
    private InputAction cast;

    private PlayerInput playerInput;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        move = playerInput.actions["Move"];
        cast = playerInput.actions["Cast"];

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = speed * Time.deltaTime * move.ReadValue<Vector2>();
        transform.position = new Vector3(transform.position.x + movement.x, transform.position.y + movement.y, transform.position.z);
        
        if(movement != Vector2.zero && !isMoving && !isCasting )
        {
            isMoving = true;
            animator.Play("wizard_red_move");
        }
        else if(movement == Vector2.zero && isMoving && !isCasting)
        {
            isMoving = false;
            animator.Play("wizard_red_idle");
        }
        if(movement.x > 0 && !isFacingRight)
        {
            isFacingRight = true;
            transform.localScale = new Vector2(1, 1);
        }
        else if(movement.x < 0 && isFacingRight)
        {
            isFacingRight = false;
            transform.localScale = new Vector2(-1, 1);
        }

        float casting = cast.ReadValue<float>();
        if(casting != 0 && !isCasting)
        {
            isCasting = true;
            StartCoroutine(CastRune());
            //animator.Play("wizard_red_cast"); //Animation pas encore implementée
        }
    }

    private IEnumerator CastRune()
    {
        PlaceRune(transform.position);
        yield return new WaitForSeconds(.2f);
        isCasting = false;
    }

    private void PlaceRune(Vector3 position)
    {
        float snapX = Mathf.RoundToInt(position.x);
        float snapY = Mathf.RoundToInt(position.y);

        Vector3 runePosition = new(snapX, snapY, 0);

        Instantiate(runePrefab, runePosition, Quaternion.identity);
    }
}
