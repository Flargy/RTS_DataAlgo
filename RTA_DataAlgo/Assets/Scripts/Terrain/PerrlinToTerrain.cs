using System;
using System.Collections;
using System.Collections.Generic;
using FutureGames.Lib;
using UnityEngine;


namespace FutureGames.Algorts
{
    public class PerrlinToTerrain : MonoBehaviour
    {
        private Terrain terrain = null;

        private Terrain Terrain
        {
            get
            {
                if (terrain == null)
                {
                    terrain = GetComponent<Terrain>();
                }

                return terrain;
            }
        }

        [SerializeField] private int width = 512;
        [SerializeField] private int height = 512;
        [SerializeField] private float scale = 80f;
        [SerializeField] private int octaves = 3;
        [SerializeField] private float persistence = 1.2f;
        [SerializeField] private float lacunarity = 1f;

        private TerrainData terrainData = null;

        private void Start()
        {
            float[,] heightmap = PerlinNoise.Generate(width, height, scale, octaves, persistence, lacunarity);
            Terrain.terrainData.SetHeights(0,0, heightmap );
        }

        private void Update()
        {
            Vector3 position = new Vector3(0f, Terrain.terrainData.GetHeight(0,0),0);
            Vector3 normal = Terrain.terrainData.GetInterpolatedNormal(0, 0);
            Debug.DrawLine(position, normal * 10, Color.red);

        }
    }
}