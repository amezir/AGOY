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

    void Start()
    {
        // Cacher le curseur de la souris au démarrage
        Cursor.visible = false;

        // Récupérer le composant CharacterController attaché à ce GameObject
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Mettre à jour le mouvement du joueur et la rotation de la caméra
        UpdateMovement();
        UpdateCameraRotation();
    }

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
        if (isRunning)
            speedMultiplier = runningSpeed;

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
