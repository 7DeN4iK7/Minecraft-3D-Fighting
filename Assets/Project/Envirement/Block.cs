using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Envirement
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class Block : MonoBehaviour, IDamagable
    {
        [SerializeField] private BlockTextureMap map;

        private void OnValidate()
        {
            if(map)
                CreateMesh();
        }

        [field: SerializeField] public int maxHealth { get; set; }
        public int currentHealth { get; set; }
        
        public Action Died { get; set; }
        public void TakeDamageFrom(int damage, Vector3 directionFrom, int effectMultiplyer)
        {
            currentHealth--;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        private MeshFilter _meshFilter;
        private Mesh _mesh;
        
        private void Awake()
        {
            currentHealth = maxHealth;
            
            CreateMesh();
        }

        private void CreateMesh()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _mesh = _meshFilter.sharedMesh;
            
            Vector3[] vertices =
            {
                //Top
                new Vector3(-0.5f, 0.5f, -0.5f),
                new Vector3(-0.5f, 0.5f,  0.5f),
                new Vector3(0.5f, 0.5f, -0.5f),
                new Vector3(0.5f, 0.5f, 0.5f),
                
                //Bottom
                new Vector3(-0.5f, -0.5f, -0.5f),
                new Vector3(-0.5f, -0.5f,  0.5f),
                new Vector3(0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, -0.5f, 0.5f),
                
                //BackSide
                new Vector3(-0.5f, -0.5f, -0.5f),
                new Vector3(-0.5f, 0.5f, -0.5f),
                new Vector3(0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, 0.5f, -0.5f),
                
                //RightSide
                new Vector3(0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, 0.5f, -0.5f),
                new Vector3(0.5f, -0.5f, 0.5f),
                new Vector3(0.5f, 0.5f, 0.5f),
                
                //LeftSide
                new Vector3(-0.5f, -0.5f, 0.5f),
                new Vector3(-0.5f, 0.5f, 0.5f),
                new Vector3(-0.5f, -0.5f, -0.5f),
                new Vector3(-0.5f, 0.5f, -0.5f),
                
                //ForwardSide
                new Vector3(0.5f, -0.5f, 0.5f),
                new Vector3(0.5f, 0.5f, 0.5f),
                new Vector3(-0.5f, -0.5f, 0.5f),
                new Vector3(-0.5f, 0.5f, 0.5f),
            };

            int[] triangles =
            {
                0, 1, 2, 2, 1, 3, //Top face
                6, 5, 4, 7, 5, 6,  //Bottom face
                8, 9, 10, 10, 9, 11, //Back face
                12, 13, 14, 14, 13, 15, //Right face
                16, 17, 18, 18, 17, 19, //Left face
                20, 21, 22, 22, 21, 23, //Forward face
            };

            List<Vector2> uvs = new List<Vector2>();

            AddTexture(map.topTexture);
            AddTexture(map.bottomTexture);
            AddTexture(map.sideTexture);
            AddTexture(map.sideTexture);
            AddTexture(map.sideTexture);
            AddTexture(map.sideTexture);

            _mesh.vertices = vertices;
            _mesh.triangles = triangles;
            _mesh.uv = uvs.ToArray();

            _mesh.RecalculateNormals();

            void AddTexture(int textureID)
            {
                float y = textureID / VoxelData.TextureAtlasSizeInBlocks;
                float x = textureID - (y * VoxelData.TextureAtlasSizeInBlocks);

                x *= VoxelData.NormalizedBlockTextureSize;
                y *= VoxelData.NormalizedBlockTextureSize;

                y = 1f - y - VoxelData.NormalizedBlockTextureSize;
                
                uvs.Add(new Vector2(x, y));
                uvs.Add(new Vector2(x, y + VoxelData.NormalizedBlockTextureSize));
                uvs.Add(new Vector2(x + VoxelData.NormalizedBlockTextureSize, y));
                uvs.Add(new Vector2(x + VoxelData.NormalizedBlockTextureSize, y + VoxelData.NormalizedBlockTextureSize));
            }
        }
    }

    [CreateAssetMenu]
    public class BlockTextureMap : ScriptableObject
    {
        public int sideTexture;
        public int topTexture;
        public int bottomTexture;
    }
    

    public static class VoxelData
    {
        public static int TextureAtlasSizeInBlocks = 8;
        
        public static float NormalizedBlockTextureSize {

            get { return 1f / (float)TextureAtlasSizeInBlocks; }

        }
    }
}