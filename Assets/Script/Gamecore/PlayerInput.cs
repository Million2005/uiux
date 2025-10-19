using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Click chuột hoặc tap
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Block block = hit.collider.GetComponent<Block>();
                if (block != null)
                {
                    block.OnTapped();
                }
            }
        }
    }
}