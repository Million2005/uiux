using UnityEngine;

public class Block : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right }
    public Direction moveDirection; // Hướng mũi tên của khối
    private Vector3 targetPosition; // Vị trí đích khi di chuyển
    private bool isMoving = false;
    private BlockGameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<BlockGameManager>();
        // Gán hướng ngẫu nhiên cho khối (có thể tùy chỉnh trong Inspector)
        if (moveDirection == 0) moveDirection = (Direction)Random.Range(0, 4);
        // Hiển thị mũi tên (dùng material hoặc sprite, tạm thời log để debug)
        Debug.Log($"{gameObject.name} direction: {moveDirection}");
    }

    public bool CanMove()
    {
        // Kiểm tra xem khối có ở rìa và không bị chặn
        Vector3 checkPos = transform.position;
        switch (moveDirection)
        {
            case Direction.Up:
                checkPos += Vector3.up;
                break;
            case Direction.Down:
                checkPos += Vector3.down;
                break;
            case Direction.Left:
                checkPos += Vector3.left;
                break;
            case Direction.Right:
                checkPos += Vector3.right;
                break;
        }
        // Kiểm tra va chạm (dùng Physics.OverlapSphere để tìm khối chặn)
        Collider[] colliders = Physics.OverlapSphere(checkPos, 0.1f);
        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.GetComponent<Block>())
            {
                return false; // Bị chặn
            }
        }
        return true; // Có thể di chuyển
    }

    public void OnTapped()
    {
        if (!isMoving && CanMove())
        {
            // Tính vị trí đích (di chuyển ra khỏi bảng)
            targetPosition = transform.position;
            switch (moveDirection)
            {
                case Direction.Up:
                    targetPosition += Vector3.up * 10f;
                    break;
                case Direction.Down:
                    targetPosition += Vector3.down * 10f;
                    break;
                case Direction.Left:
                    targetPosition += Vector3.left * 10f;
                    break;
                case Direction.Right:
                    targetPosition += Vector3.right * 10f;
                    break;
            }
            isMoving = true;
            gameManager.RemoveBlock(this); // Thông báo GameManager xóa khối
        }
        else if (!CanMove())
        {
            gameManager.LoseHeart(); // Trừ mạng nếu tap sai
        }
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5f);
            Debug.Log($"Moving {gameObject.name} to {targetPosition}, current: {transform.position}");
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f) // Tăng ngưỡng
            {
                Debug.Log($"Destroying {gameObject.name}");
                Destroy(gameObject);
            }
        }
    }
}