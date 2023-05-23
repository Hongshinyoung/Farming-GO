using UnityEngine;
using UnityEngine.EventSystems; // Ű����, ���콺, ��ġ�� �̺�Ʈ�� ������Ʈ�� ���� �� �ִ� ����� ����

public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;
    [SerializeField, Range(10f, 150f)]
    private float leverRange;

    private Vector2 inputDirection;    // �߰�
    private bool isInput;    // �߰�
    [SerializeField]
    private PlayerController playerController;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //var inputPos = eventData.position - rectTransform.anchoredPosition;
        //var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        //lever.anchoredPosition = inputVector;
        //inputDirection = inputVector / leverRange;


        ControlJoystickLever(eventData);  // �߰�
        isInput = true;   // �߰�
    }

    // ������Ʈ�� Ŭ���ؼ� �巡�� �ϴ� ���߿� ������ �̺�Ʈ
    // ������ Ŭ���� ������ ���·� ���콺�� ���߸� �̺�Ʈ�� ������ ����    
    public void OnDrag(PointerEventData eventData)
    {
        // var inputDir = eventData.position - rectTransform.anchoredPosition;
        // var clampedDir = inputDir.magnitude < leverRange ? inputDir 
        //     : inputDir.normalized * leverRange;
        // lever.anchoredPosition = clampedDir;

        ControlJoystickLever(eventData);    // �߰�
        
    }

    // �߰�
    public void ControlJoystickLever(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTransform.anchoredPosition;
        var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
        playerController.Move(Vector2.zero);
    }

    private void InputControlVector()
    {
        playerController.Move(inputDirection);
    }

    void Start()
    {

    }

    void Update()
    {
        if (isInput)
        {
            InputControlVector();
        }
    }
}
