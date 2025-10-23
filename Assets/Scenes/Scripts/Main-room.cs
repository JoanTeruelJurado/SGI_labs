using UnityEngine;

[ExecuteAlways] // Works both in Editor and Play mode
public class MainRoom : MonoBehaviour
{
    [Header("Room Dimensions (in meters)")]
    public float width = 10f;   // X-axis
    public float depth = 8f;    // Z-axis
    public float wallThickness = 0.1f;

    // Cached references to child cubes
    private Transform floor, ceiling, wallFront, wallBack, wallLeft, wallRight;

    private void OnValidate()
    {
        UpdateRoom();
    }

    private void Update()
    {
        // Optional: live updates in Play mode
        UpdateRoom();
    }

    private void UpdateRoom()
    {
        // --- Find or auto-assign child walls ---
        if (floor == null) floor = transform.Find("Floor");
        if (ceiling == null) ceiling = transform.Find("Ceiling");
        if (wallFront == null) wallFront = transform.Find("Wall_Front");
        if (wallBack == null) wallBack = transform.Find("Wall_Back");
        if (wallLeft == null) wallLeft = transform.Find("Wall_Left");
        if (wallRight == null) wallRight = transform.Find("Wall_Right");

        if (!floor || !ceiling || !wallFront || !wallBack || !wallLeft || !wallRight)
        {
            Debug.LogWarning("Some wall objects are missing! Please create 6 cubes named Floor, Ceiling, Wall_Front, Wall_Back, Wall_Left, Wall_Right inside MAIN_ROOM.");
            return;
        }

        // --- Floor ---
        floor.localScale = new Vector3(width, wallThickness, depth);
        floor.localPosition = new Vector3(0, -width / 2f, 0);

        // --- Ceiling ---
        ceiling.localScale = new Vector3(width, wallThickness, depth);
        ceiling.localPosition = new Vector3(0, width / 2f, 0);

        // --- Front Wall ---
        wallFront.localScale = new Vector3(width, width, wallThickness);
        wallFront.localPosition = new Vector3(0, 0, depth / 2f);

        // --- Back Wall ---
        wallBack.localScale = new Vector3(width, width, wallThickness);
        wallBack.localPosition = new Vector3(0, 0, -depth / 2f);

        // --- Left Wall ---
        wallLeft.localScale = new Vector3(wallThickness, width, depth);
        wallLeft.localPosition = new Vector3(-width / 2f, 0, 0);

        // --- Right Wall ---
        wallRight.localScale = new Vector3(wallThickness, width, depth);
        wallRight.localPosition = new Vector3(width / 2f, 0, 0);
    }
}
