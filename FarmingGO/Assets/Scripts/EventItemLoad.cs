using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class EventItemLoad : MonoBehaviour
{
    public string imagePath = "Assets/ScreenShot/";
    [SerializeField]
    private RawImage imageRenderer;

    private void Start()
    {
        imageRenderer = GetComponent<RawImage>();
        // RawImage ������Ʈ�� �Ҵ�Ǿ����� Ȯ��
        if (imageRenderer == null)
        {
            Debug.LogError("RawImage ������Ʈ�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        LoadAndDisplayImage();
    }

    private void LoadAndDisplayImage()
    {
        // Load the PNG files in the specified directory
        string[] files = Directory.GetFiles(imagePath, "*.png");
        if (files.Length == 0)
        {
            Debug.Log("No PNG files found in the specified directory.");
            return;
        }

        // Sort the files based on their names (assuming the names are numbers)
        System.Array.Sort(files);

        // Get the first PNG file in the directory
        string filePath = files[0];

        byte[] bytes = File.ReadAllBytes(filePath);

        // Create a Texture2D and load the image bytes
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);

        // Assign the texture to the RawImage component
        imageRenderer.texture = texture;
    }
}
