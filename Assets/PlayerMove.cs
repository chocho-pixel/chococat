using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    [Header("방향별 캐릭터 이미지")]
    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Sprite backSprite;
    [SerializeField] private Sprite sideSprite;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        Vector2 direction = Vector2.zero;

        if (Keyboard.current.wKey.isPressed ||
            Keyboard.current.upArrowKey.isPressed)
        {
            direction.y += 1f;
        }

        if (Keyboard.current.sKey.isPressed ||
            Keyboard.current.downArrowKey.isPressed)
        {
            direction.y -= 1f;
        }

        // 현재 화면 기준 왼쪽
        if (Keyboard.current.aKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed)
        {
            direction.x += 1f;
        }

        // 현재 화면 기준 오른쪽
        if (Keyboard.current.dKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed)
        {
            direction.x -= 1f;
        }

        direction = direction.normalized;

        ChangeDirectionSprite(direction);

        transform.position +=
            (Vector3)(direction * moveSpeed * Time.deltaTime);
    }

    private void ChangeDirectionSprite(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return;

        // 좌우 입력이 더 강하면 옆모습
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            spriteRenderer.sprite = sideSprite;

            // A는 화면 왼쪽, D는 화면 오른쪽
            spriteRenderer.flipX = direction.x < 0f;
        }
        else if (direction.y > 0f)
        {
            // 위로 이동할 때 뒷모습
            spriteRenderer.sprite = backSprite;
            spriteRenderer.flipX = false;
        }
        else
        {
            // 아래로 이동할 때 정면
            spriteRenderer.sprite = frontSprite;
            spriteRenderer.flipX = false;
        }
    }
}