using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FutureGames.Lib
{
    public class TestPerlinNoise : MonoBehaviour
    {
        [SerializeField] private int width = 512;
        [SerializeField] private int height = 512;
        [SerializeField] private float scale = 1f;
        [SerializeField] private int octaves = 3;
        [SerializeField] private float persistance = 1.2f;
        [SerializeField] private float lucunarity = 2f;
        void Start()
        {
            PerlinNoise.GenerateHeightMap(width, height, scale, octaves, persistance, lucunarity).PngToDisk("");
        }

    }
}