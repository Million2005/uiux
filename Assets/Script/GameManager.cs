using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelData
{
    public Sprite backgroundImage;
    public string levelName;
    public int width, height;

}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public BoardManager board;
    public Image backgroundImage;
    public List<Sprite> levelImages;
    public int currentLevel = 0;

    void Awake()
    {
        Instance = this;
        StartLevel(currentLevel);   
    }
    public void StartLevel(int lvl)
    {
        currentLevel = lvl;
        //tang kich thuoc bang theo tung lvl
        board.width = board.width; // keep
        board.height = board.height;
        if(backgroundImage != null && levelImages !=null && levelImages.Count > 0)
        {
            backgroundImage.sprite = levelImages[Mathf.Clamp(lvl,0, levelImages.Count-1)];
            backgroundImage.SetNativeSize();
        }  
        board.GenerateEmptyBoard();
        //phan tan cac chan
        ScatterBlockers(0);
    }
    void ScatterBlockers(int count)
    {
        int attempts = 0;
        int placed = 0;
        while (placed < count && attempts < 1000)
        {
            int x = Random.Range(0, board.width);
            int y = Random.Range(0, board.height);
            var t = board.GetTileAt(x, y);
            if(t != null && t.tileType == TileType.Arrow)
            {
                t.tileType = TileType.Blocker;
                t.RefreshVisual();
                placed++;
            }
            attempts++ ;
        }

    }
    public void HandleTileTap(int x , int y)
    {
        bool changed = board.TryActivateTile(x, y);
        if (changed)
        {
            CheckWinCondition();
            // Đảm bảo sau mỗi nước đi vẫn còn nước để chơi
            board.EnsurePlayableNow();
        }
    }
    void CheckWinCondition() {
        int total = board.width * board.height;
        int revealed = 0;
        for (int yy = 0; yy < board.height; yy++)
        {
            for (int xx = 0; xx < board.width; xx++)
            {
                var t = board.GetTileAt(xx, yy);
                if (t != null && t.tileType == TileType.Revealed) revealed++;
            }
        }
        if (total > 0 && (float)revealed / total >= 0.6f)
        {
            Debug.Log("Game complete");
            // them UI vao day UI win
        }
    }
    public void Usebomb(int x,int y)
    {
        board.BomAt(x, y, 1);
        CheckWinCondition();
        board.EnsurePlayableNow();

    }
    public void UseRowClear(int row)
    {
        board.ClearRow(row);
        CheckWinCondition();
        board.EnsurePlayableNow();
    }


}
