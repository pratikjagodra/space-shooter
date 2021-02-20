using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("For Movement")]
    [Tooltip("In (m/s)")][SerializeField] float Speed = 150f;
    [Tooltip("Horizontal range In (m)")] [SerializeField] float xRange = 5f;
    [Tooltip("Vertical Range In (m)")] [SerializeField] float yRange = 5f;
    [SerializeField] GameObject[] guns;

    [Header("For Movement Because of Position")]
    [SerializeField] float positionPitchFactor = -1f;
    [SerializeField] float controlPitchFactor = -25f;

    [SerializeField] float positionYawFactor = -1f;

    [SerializeField] float controlRollFactor = -25f;

    [Header("For gameover text")]
    [SerializeField] GameObject gameOver;

    float xThrow, yThrow;
    bool isControlEnable = true;

    void Start()
    {
        Invoke("OnPlayerDeath", 31f);
    }

    void Update()
    {
        if (isControlEnable)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
        else
        {
            if (CrossPlatformInputManager.GetButtonDown("Submit"))
            {
                ReloadLevel();
            }
        }
    }

    void OnPlayerDeath() //string referenced
    {
        isControlEnable = false;
        SetGunsActive(false);
        gameOver.SetActive(true);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * Speed * Time.deltaTime;
        float yOffset = yThrow * Speed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControl;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach(GameObject gun in guns)
        {
            var gunEmission = gun.GetComponent<ParticleSystem>().emission;
            gunEmission.enabled = isActive;
        }
    }
}
