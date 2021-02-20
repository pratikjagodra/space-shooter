using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        deathFX.SetActive(true);
        SendMessage("OnPlayerDeath");
    }
}
