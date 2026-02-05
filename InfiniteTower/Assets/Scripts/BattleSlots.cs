using UnityEngine;

public class BattleSlots : MonoBehaviour
{
    [Header("Arena Dimensions")]
    public float arenaWidth = 10f;
    private float arenaHeight = 16f;
    [Header("Arena Padding")]
    public float paddingSide = 1.2f;
    public float paddingTop = 1.5f;
    public float paddingBottom = 1.5f;

    [Header("Slot Settings")]
    public Vector3[] slotPositionsLeft = new Vector3[5];
    public Vector3[] slotPositionsRight = new Vector3[5];
    [Header("Prefabs")]
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject enemyPrefab;

    void Start()
    {
        float visibleWorldHeight = Camera.main.orthographicSize * 2f;
        arenaHeight = visibleWorldHeight*(2f/3f);

        float leftBoundary = -arenaWidth / 2f + paddingSide;
        float rightBoundary = arenaWidth / 2f - paddingSide;

        float arenaTop = visibleWorldHeight / 2f ;
        float arenaBottom = arenaTop - arenaHeight;

        float topUsable = arenaTop - paddingTop;
        float bottomUsable = arenaBottom + paddingBottom;

        // Calculate slot positions 3 2 X 2 3

        float playerBackX = leftBoundary + paddingSide;
        float playerFrontX = leftBoundary + paddingSide * 2f;

        float enemyFrontX = rightBoundary - paddingSide * 2f;
        float enemyBackX = rightBoundary - paddingSide;

        float[] y2= new float[2];
        float[] y3= new float[3];

        for (int i = 0; i < 2; i++)
        {
            float t = (i+0.5f) / 2f;
            y2[i] = Mathf.Lerp(bottomUsable, topUsable, t);
            
        }
        for (int i = 0; i < 3; i++)
        {
            float t = (i+0.5f) / 3f;
            y3[i] = Mathf.Lerp(bottomUsable, topUsable, t);   
        }

        slotPositionsLeft[0] = new Vector3(playerFrontX, y2[0], 0f);
        slotPositionsLeft[1] = new Vector3(playerFrontX, y2[1], 0f);
        slotPositionsLeft[2] = new Vector3(playerBackX, y3[0], 0f);
        slotPositionsLeft[3] = new Vector3(playerBackX, y3[1], 0f);
        slotPositionsLeft[4] = new Vector3(playerBackX, y3[2], 0f);

        slotPositionsRight[0] = new Vector3(enemyFrontX, y2[0], 0f);
        slotPositionsRight[1] = new Vector3(enemyFrontX, y2[1], 0f);
        slotPositionsRight[2] = new Vector3(enemyBackX, y3[0], 0f);
        slotPositionsRight[3] = new Vector3(enemyBackX, y3[1], 0f);
        slotPositionsRight[4] = new Vector3(enemyBackX, y3[2], 0f);


        InstantiateSlots();
    }

    void InstantiateSlots()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(playerPrefab).transform.position = slotPositionsLeft[i];
            Instantiate(enemyPrefab).transform.position = slotPositionsRight[i];
        }
        

    }

}
