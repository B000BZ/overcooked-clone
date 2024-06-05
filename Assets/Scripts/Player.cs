using System;
using System.Collections;
using UnityEngine;
using CandyCoded.HapticFeedback;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler OnDoubleClick;
    public event EventHandler OnSingleClick;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    [SerializeField] private Joystick joystick;


    private KitchenObject kitchenObject;
    private bool isWalking;
    private Vector3 lastIneractDir;
    private BaseCounter selectedCounter;
    private bool isGamePaused = false;

    private void Awake()
    {
        if (Instance != null)
        {
   
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
        OnDoubleClick += Player_OnDoubleClick;
        OnSingleClick += Player_OnSingleClick;
        GameManager.Instance.onGamePaused += GameManager_onGamePaused;
        GameManager.Instance.onGamePaused += GameManager_onGameUnpaused;
    }

    private void GameManager_onGamePaused(object sender, EventArgs e)
    {
        isGamePaused = true;
    }
    private void GameManager_onGameUnpaused(object sender, EventArgs e)
    {
        isGamePaused = false;
    }

    private void Player_OnSingleClick(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
            HapticFeedback.HeavyFeedback();
        }
    }

    private void Player_OnDoubleClick(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
            HapticFeedback.MediumFeedback();
        }
    }

    private float lastClicked;

    private void Update()
    {

            HandleMovement();
            HandleInteractions();

        

        if (Input.GetMouseButtonDown(0) && GameManager.Instance.isPaused == false)
        {
            
            if(Time.time - lastClicked < 0.25f )
            {
                lastClicked = 0f;
                OnDoubleClick?.Invoke(this, EventArgs.Empty);
            }
            else 
            {
                OnSingleClick?.Invoke(this, EventArgs.Empty);
            }

            lastClicked = Time.time;
        }

    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null )
        {
            selectedCounter.Interact(this);
        }
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null )
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void HandleMovement()
    {
        
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;
        Vector3 joystickMoveDir = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;

        
        bool canMoveJoy = !Physics.CapsuleCast(transform.position, transform.position +
            Vector3.up * playerHeight, playerRadius, joystickMoveDir, moveDistance, countersLayerMask);

        if (!canMoveJoy)
        {
            Vector3 joystickMoveDirX = new Vector3(joystickMoveDir.x, 0f, 0f);
            canMoveJoy = joystickMoveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position+
                Vector3.up * playerHeight, playerRadius, joystickMoveDirX, moveDistance);

            if (canMoveJoy && !isGamePaused)
            {
                joystickMoveDir = joystickMoveDirX.normalized;
            }
            else
            {
                Vector3 joystickMoveDirZ = new Vector3(0f, 0f, joystickMoveDir.z);
                canMoveJoy = joystickMoveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position+
                    Vector3.up * playerHeight, playerRadius, joystickMoveDirZ, moveDistance);

                if (canMoveJoy && !isGamePaused)
                {
                    joystickMoveDir = joystickMoveDirZ.normalized;
                }
            }
        }
        
        if (canMoveJoy && !isGamePaused)
        {
            
            transform.position += joystickMoveDir * moveDistance;
        }

        isWalking = joystickMoveDir != Vector3.zero;
        float rotationSpeed = 5.25f;
        transform.forward = Vector3.Slerp(transform.forward, joystickMoveDir, Time.deltaTime * rotationSpeed);

    }


    private void HandleInteractions()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;
        Vector3 joystickMoveDir = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (joystickMoveDir != Vector3.zero)
        {
            lastIneractDir = joystickMoveDir;
        }

        if (Physics.Raycast(transform.position, lastIneractDir, out RaycastHit raycastHit,
            interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter clearCounter))
            {
                if (clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);

                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter

        }) ;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }

    public void ClearKitchenObject(){
        kitchenObject = null;
    }

    public bool HasKitchenObject(){
        return kitchenObject != null;
    }

    public bool IsWalking(){
        return isWalking;
    }

 


    
}
