using UnityEngine;

public class PlayerFPS : MonoBehaviour
{
    ///<summary>
    /// Référence à la caméra du joueur
    ///</summary>
    public Camera playerCamera;

    ///<summary>
    /// Composant pour le contrôle du personnage
    ///</summary>
    CharacterController characterController;

    ///<summary>
    /// Vitesses de déplacement
    ///</summary>
    public float walkingSpeed = 6.0f;
    public float runningSpeed = 9.0f;
    public float crouchingSpeed = 1f;

    ///<summary>
    /// Force de saut
    ///</summary>
    public float jumpForce = 8.0f;

    ///<summary>
    /// Gravité appliquée au personnage
    ///</summary>
    float gravity = 25.0f;

    ///<summary>
    /// Direction du mouvement
    ///</summary>
    Vector3 moveDirection;

    ///<summary>
    /// Drapeau indiquant si le joueur est en train de courir
    ///</summary>
    bool isRunning = false;

    ///<summary>
    /// Drapeau indiquant si le joueur est accroupi
    ///</summary>
    bool isCrouching = false;

    ///<summary>
    /// Rotation de la caméra
    ///</summary>
    float rotationX = 0;

    ///<summary>
    /// Vitesse de rotation de la caméra
    ///</summary>
    public float rotationSpeed = 2.0f;

    ///<summary>
    /// Limite de rotation verticale de la caméra
    ///</summary>
    public float rotationXLimit = 60f;

    ///<summary>
    /// Temps écoulé depuis le début du sprint
    ///</summary>
    float sprintTimer = 0f;

    ///<summary>
    /// Durée du sprint
    ///</summary>
    public float sprintDuration = 5f;

    ///<summary>
    /// Durée de la pause entre les sprints
    ///</summary>
    public float sprintCooldown = 10f;

    ///<summary>
    /// Drapeau indiquant si le sprint est en cours ou en pause
    ///</summary>
    bool sprinting = false;

    ///<summary>
    /// Start
    ///</summary>
    void Start()
    {
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
    }

    ///<summary>
    /// Update
    ///</summary>
    void Update()
    {
        UpdateMovement();
        UpdateCameraRotation();
    }

    ///<summary>
    /// Update movement of the player character based on user input
    ///</summary>
    void UpdateMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float speedZ = Input.GetAxis("Vertical");
        float speedX = Input.GetAxis("Horizontal");
        float speedY = moveDirection.y;

        isRunning = Input.GetKey(KeyCode.LeftShift);
        isCrouching = Input.GetKey(KeyCode.LeftControl);

        float speedMultiplier = walkingSpeed;

        if (isRunning && !sprinting && sprintTimer <= 0)
        {
            sprinting = true;
            sprintTimer = sprintDuration;
            Debug.Log("Sprint activé");
        }

        if (sprinting)
        {
            speedMultiplier = runningSpeed;
            sprintTimer -= Time.deltaTime;

            if (sprintTimer <= 0)
            {
                sprinting = false;
                sprintTimer = sprintCooldown;
                Debug.Log("Sprint désactivé");
            }
        }
        else
        {
            if (sprintTimer > 0)
                sprintTimer -= Time.deltaTime;
        }

        if (isCrouching)
            speedMultiplier = crouchingSpeed;

        speedX *= speedMultiplier;
        speedZ *= speedMultiplier;

        moveDirection = forward * speedZ + right * speedX;

        if (Input.GetButton("Jump") && characterController.isGrounded)
            moveDirection.y = jumpForce;
        else
            moveDirection.y = speedY;

        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    ///<summary>
    /// Update camera rotation
    ///</summary>
    void UpdateCameraRotation()
    {
        rotationX += -Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, -rotationXLimit, rotationXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);
    }
}
