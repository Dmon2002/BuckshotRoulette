using System.Collections.Generic;
using UnityEngine;

public class ShellManager : MonoBehaviour
{
    public GameObject revolver; // Reference to the revolver GameObject
    public Animator revolverAnimator; // Animator for revolver animations
    public List<GameObject> shellPrefabs; // List of 3D shell prefabs
    public int shellCount = 6; // Number of shells in the revolver

    private List<bool> shellStatuses; // List to store shell statuses (live or blank)

    void Start()
    {
        GenerateShellStatuses();
        revolverAnimator = revolver.GetComponent<Animator>();
        revolverAnimator.SetTrigger("Spin"); // Trigger animation to spin revolver
    }

    // Generates random statuses for each shell (true = live, false = blank)
    void GenerateShellStatuses()
    {
        shellStatuses = new List<bool>();

        for (int i = 0; i < shellCount; i++)
        {
            bool isLive = Random.value > 0.5f; // 50% chance of being live
            shellStatuses.Add(isLive);
        }
    }

    // Returns shell status at a specific index
    public bool GetShellStatus(int index)
    {
        if (index >= 0 && index < shellStatuses.Count)
        {
            return shellStatuses[index];
        }
        return false; // Default to blank if index is out of range
    }
}
