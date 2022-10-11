using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using TMPro;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    WebCamTexture camTexture;

    public RawImage cameraViewImage;
    public TextMeshProUGUI ScoreText;
    private float timer = 0f;
    public static bool RecordSwitch = false;

    private void Start()
    {
        //ī�޶� ���� �ο�
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            //������ ������ ���� �ο�
            Permission.RequestUserPermission(Permission.Camera);
        }
    }

    private void Update()
    {
        if(RecordSwitch && timer <= 15f)
        {
            ScoreText.text = $"{(int)timer}";
            timer += Time.deltaTime;
        }
        else if(RecordSwitch && timer > 15f)
        {
            RecordSwitch = false;
            timer = 0f;
        }
    }

    //ī�޶��ѱ�
    public void CameraOn()
    {

        //ī�޶� ������ ���� �ȵǰ� �ϱ�
        if (WebCamTexture.devices.Length == 0)
        {
            Debug.Log("no Camera");
            return;
        }

        //����Ʈ���� ī�޶� ������ ��� ������
        WebCamDevice[] devices = WebCamTexture.devices;
        int selectedCameraIndex = -1;

        //�ĸ� ī�޶� ã��
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == true)
            {
                selectedCameraIndex = i;
                break;
            }
        }

        //ī�޶� �ѱ�
        if (selectedCameraIndex >= 0)
        {
            //���õ� �ĸ� ī�޶� ������.
            camTexture = new WebCamTexture(devices[selectedCameraIndex].name);

            //ī�޶� ������ ����
            camTexture.requestedFPS = 30;

            //������ raw Image�� �Ҵ�
            cameraViewImage.texture = camTexture;

            //ī�޶� �����ϱ�
            camTexture.Play();
        }

    }

    //ī�޶� ����
    public void CameraOff()
    {
        //ī�޶� ������
        if (camTexture != null)
        {
            //ī�޶� ����
            camTexture.Stop();

            //ī�޶� ��ü �ݳ�
            WebCamTexture.Destroy(camTexture);

            //���� �ʱ�ȭ
            camTexture = null;
        }
    }


}