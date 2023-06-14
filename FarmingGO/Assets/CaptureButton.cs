using UnityEngine;
using UnityEngine.UI;

namespace SimpleCapture
{
    public class CaptureButton : MonoBehaviour
    {
        public ScreenShut screenShut;

        private void Start()
        {
            // ��ư ������Ʈ�� �����ɴϴ�.
            Button button = GetComponent<Button>();

            // ��ư Ŭ�� �̺�Ʈ�� �Կ� ����� �����ϴ� �Լ��� �߰��մϴ�.
            button.onClick.AddListener(CaptureScreen);
        }

        private void CaptureScreen()
        {
            // ScreenShut ��ũ��Ʈ�� CaptureScreen �޼��带 ȣ���Ͽ� �Կ��� �����մϴ�.
            screenShut.CaptureScreen();
        }
    }
}
