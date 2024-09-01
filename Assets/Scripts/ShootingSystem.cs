using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShootingSystem : MonoBehaviour
{
    public ShellManager shellManager; // Reference to ShellManager script
    public Animator handAnimator; // Animator for hand animations
    public Button shootYourselfButton; // Button to shoot yourself
    public Button shootDealerButton; // Button to shoot the dealer

    public GameObject revolverObj; // Reference to the revolver GameObject
    public GameObject handObj; // Reference to the revolver GameObject

    private int currentShellIndex = 0; // Tracks current shell to fire

    int target; // 0 == Dealer 1 == Player

    void Start()
    {
        shootYourselfButton.onClick.AddListener(ShootYourself);
        shootDealerButton.onClick.AddListener(ShootDealer);
    }

    void ShootYourself()
    {
        handAnimator.SetTrigger("ShootYourself"); // Trigger shooting animation
        Invoke("Fire", 4.41f); // Delay to sync with animation timing
        Invoke("TakeGun", 1.5f);
        target = 1;
    }

    void ShootDealer()
    {
        handAnimator.SetTrigger("Shoot"); // Trigger shooting animation
        Invoke("Fire", 4.41f); // Delay to sync with animation timing
        Invoke("TakeGun", 1.5f);
        target = 0;
    }

    void TakeGun()
    {
        revolverObj.transform.parent = handObj.transform;
        revolverObj.transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    void Fire()
    {
        bool isLive = shellManager.GetShellStatus(currentShellIndex);

        if (isLive)
        {
            Debug.Log("-1 live"); // Simulate the shot being live
            // Add effects for live shot here
        }
        else
        {
            Debug.Log("Click - Empty shell");
        }

        currentShellIndex++; // Move to the next shell
        // Check if all shells have been used
        if (currentShellIndex >= shellManager.shellCount)
        {
            currentShellIndex = 0; // Reset index if out of shells
            Debug.Log("Emtpied the clip");
        }

        // Trigger hand return animation
        StartCoroutine(ReturnHand());
    }

    IEnumerator ReturnHand()
    {
        yield return new WaitForSeconds(0.24f);
        if (target == 0)
        {
            handAnimator.SetTrigger("Return");
        }
        else
        {
            handAnimator.SetTrigger("ReturnYourself");
        }
        yield return new WaitForSeconds(1.5f);
        revolverObj.transform.parent = null;
        revolverObj.transform.position = new Vector3(0f, 0f, 0f);
    }
}
