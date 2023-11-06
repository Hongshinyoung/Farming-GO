//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//using System.IO;
//using UnityEngine.UI;

//namespace SimpleCapture
//{
//    public class ScreenShut : MonoBehaviour
//    {
//        const int REF_X = 960;
//        const int REF_Y = 540;

//        // 4k = 3840 x 2160   1080p = 1920 x 1080
//        private enum resPreset { _1k = 1, _2k = 2, _4k = 4, _8k = 8, custom = 5 }
//        private resPreset res = resPreset._2k;

//        private enum camOrientationPreset { Horizon, Vertical }
//        private camOrientationPreset camOrientation = camOrientationPreset.Horizon;

//        public Camera camera;
//        public bool transparent;

//        public Vector2 captureSize = new Vector2(1920, 1080);

//        public void CaptureScreen()
//        {
//            // 해상도.
//            int captureWidth = (int)Mathf.Round(captureSize.x);
//            int captureHeight = (int)Mathf.Round(captureSize.y);

//            // 경로 적용.
//            string path = Application.dataPath + "/ScreenShot/";
//            DirectoryInfo dir = new DirectoryInfo(path);
//            if (!dir.Exists)
//            {
//                Directory.CreateDirectory(path);
//            }

//            // 파일명 생성.
//            string[] files = Directory.GetFiles(path, "*.png");
//            int fileCount = files.Length;
//            string name = path + (fileCount + 1).ToString() + ".png";

//            // 텍스쳐 생성.
//            TextureFormat format = transparent ? TextureFormat.ARGB32 : TextureFormat.RGB24;
//            Texture2D screenShot = new Texture2D(captureWidth, captureHeight, format, false);
//            RenderTexture rt = new RenderTexture(captureWidth, captureHeight, 24);

//            // 카메라 백업.
//            RenderTexture bak_cam_targetTexture = camera.targetTexture;
//            RenderTexture bak_RenderTexture_active = RenderTexture.active;
//            CameraClearFlags bak_cam_clearFlags = camera.clearFlags;
//            Color bak_cam_background = camera.backgroundColor;

//            // 카메라 설정.
//            RenderTexture.active = rt;
//            camera.targetTexture = rt;
//            if (transparent)
//            {
//                camera.clearFlags = CameraClearFlags.SolidColor;
//                camera.backgroundColor = Color.clear;
//            }

//            // 캡쳐.
//            Rect rec = new Rect(0, 0, screenShot.width, screenShot.height);
//            camera.Render();
//            RenderTexture.active = rt;
//            screenShot.ReadPixels(new Rect(0, 0, captureWidth, captureHeight), 0, 0);
//            screenShot.Apply();

//            // 카메라 복구.
//            camera.targetTexture = bak_cam_targetTexture;
//            RenderTexture.active = bak_RenderTexture_active;
//            camera.clearFlags = bak_cam_clearFlags;
//            camera.backgroundColor = bak_cam_background;

//            camera.Render();

//            // 저장.
//            byte[] bytes = screenShot.EncodeToPNG();
//            File.WriteAllBytes(name, bytes);

//            //AssetDatabase.Refresh();
//        }
//    }
//}
