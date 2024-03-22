using UnityEngine;

public class PlayerFPS : MonoBehaviour
{
    // Référence à la caméra du joueur
    public Camera playerCamera;

    // Composant pour le contrôle du personnage
    CharacterController characterController;

    // Vitesses de déplacement
    public float walkingSpeed = 6.0f;
    public float runningSpeed = 9.0f;
    public float crouchingSpeed = 1f;

    // Force de saut
    public float jumpForce = 8.0f;

    // Gravité appliquée au personnage
    float gravity = 25.0f;

    // Direction du mouvement
    Vector3 moveDirection;

    // Drapeau indiquant si le joueur est en train de courir
    bool isRunning = false;

    // Drapeau indiquant si le joueur est accroupi
    bool isCrouching = false;

    // Rotation de la caméra
    float rotationX = 0;

    // Vitesse de rotation de la caméra
    public float rotationSpeed = 2.0f;

    // Limite de rotation verticale de la caméra
    public float rotationXLimit = 60f;

    // Temps écoulé depuis le début du sprint
    float sprintTimer = 0f;

    // Durée du sprint
    public float sprintDuration = 5f;

    // Durée de la pause entre les sprints
    public float sprintCooldown = 10f;

    // Drapeau indiquant si le sprint est en cours ou en pause
    bool sprinting = false;

    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        // Cacher le curseur de la souris au démarrage
        Cursor.visible = false;

        // Récupérer le composant CharacterController attaché à ce GameObject
        characterController = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Update
    /// </summary>
    void Update()
    {
        // Mettre à jour le mouvement du joueur et la rotation de la caméra
        UpdateMovement();
        UpdateCameraRotation();
    }

    /// <summary>
    /// Update movement of the player character based on user input
    /// </summary>
    void UpdateMovement()
    {
        // Direction locale avant et droite du joueur
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Input de l'axe vertical (avant/arrière)
        float speedZ = Input.GetAxis("Vertical");

        // Input de l'axe horizontal (gauche/droite)
        float speedX = Input.GetAxis("Horizontal");

        // Composante Y de la vitesse actuelle du mouvement
        float speedY = moveDirection.y;

        // Vérifier si le joueur est en train de courir
        isRunning = Input.GetKey(KeyCode.LeftShift);

        // Vérifier si le joueur est accroupi
        isCrouching = Input.GetKey(KeyCode.LeftControl);

        // Initialiser le multiplicateur de vitesse avec la vitesse de marche
        float speedMultiplier = walkingSpeed;

        // Changer le multiplicateur de vitesse en fonction de l'état du joueur
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

        // Appliquer la vitesse sur les axes X et Z
        speedX *= speedMultiplier;
        speedZ *= speedMultiplier;

        // Calculer la direction de mouvement
        moveDirection = forward * speedZ + right * speedX;

        // Gérer le saut
        if (Input.GetButton("Jump") && characterController.isGrounded)
            moveDirection.y = jumpForce;
        else
            moveDirection.y = speedY;

        // Gérer la gravité lorsque le joueur n'est pas au sol
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        // Appliquer le mouvement au CharacterController
        characterController.Move(moveDirection * Time.deltaTime);
    }

    /// <summary>
    /// Update camera rotation
    /// </summary>
    void UpdateCameraRotation()
    {
        // Rotation verticale de la caméra en fonction du mouvement de la souris
        rotationX += -Input.GetAxis("Mouse Y") * rotationSpeed;

        // Limiter la rotation verticale
        rotationX = Mathf.Clamp(rotationX, -rotationXLimit, rotationXLimit);

        // Appliquer la rotation verticale à la caméra
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        // Rotation horizontale du joueur en fonction du mouvement de la souris
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);
    }
}
