using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FutureGames.Lib
{


    public static class TextureExtensions 
    {
        public static void PngToDisk(this Texture2D t, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = Application.dataPath + "/" + "MyTexture" + ".png"; //persistent data path on build
            }
            
            Debug.Log(path);
            byte[] bytes = t.EncodeToPNG();
            
            File.WriteAllBytes(path, bytes);
        }
    }
}