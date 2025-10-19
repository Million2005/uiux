using UnityEngine;
using System.Collections.Generic;

public class BlockGameManager : MonoBehaviour
{
    public GameObject blockPrefab; // Prefab của khối
    public int gridWidth = 5; // Chiều rộng lưới
    public int gridHeight = 5; // Chiều cao lưới
    public float spacing = 1.1f; // Khoảng cách giữa các khối
    public int hearts = 3; // Số mạng
    private List<Block> blocks = new List<Block>();
    public GameObject backgroundImage; // Hình nền ẩn (SpriteRenderer)

    void Start()
    {
        GenerateGrid();
        if (backgroundImage) backgroundImage.SetActive(false); // Ẩn hình nền ban đầu
    }

    void GenerateGrid()
    {
        // Tạo lưới khối
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 pos = new Vector3(x * spacing - (gridWidth - 1) * spacing / 2, y * spacing - (gridHeight - 1) * spacing / 2, 0);
                GameObject blockObj = Instantiate(blockPrefab, pos, Quaternion.identity);
                Block block = blockObj.GetComponent<Block>();
                blocks.Add(block);
            }
        }
    }

    public void RemoveBlock(Block block)
    {
        blocks.Remove(block);
        if (blocks.Count == 0)
        {
            // Thắng: hiện hình nền
            if (backgroundImage) backgroundImage.SetActive(true);
            Debug.Log("You Win!");
        }
    }

    public void LoseHeart()
    {
        hearts--;
        Debug.Log($"Hearts left: {hearts}");
        if (hearts <= 0)
        {
            Debug.Log("Game Over!");
            // Có thể thêm logic reset hoặc màn hình thua
        }
    }
}