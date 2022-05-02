using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Mesh model;

    public Material playerMaterial;
    public Material enemiesMaterial;
    public Material securityCameraMaterial;
    public Material wallMaterial;
    public Material floorMaterial;
    public Material objectMaterial;
    public Material rampMaterial;

    public Transform[] minimapCamera;
    public Transform[] player;
    public Transform[] enemies;
    public Transform[] securityCamera;
    public Transform[] floor;
    public Transform[] wall;
    public Transform[] objects;
    public Transform[] ramp;

    public float scale = 0.5f;
    public float dotScale = 2.0f;

    public Vector3 cameraOffset;
    public Vector3 floorOffset;
    public Vector3 wallVerOffset;
    public Vector3 wallHorOffset;
    public Transform minimapPos;
    public float cameraDistance;

    private Vector3 playerC;

    private Matrix4x4 wallMatrix;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Player
        for (int i = 0; i < player.Length; i++)
        {
            Vector3 playerW = player[i].position;
            playerC = minimapPos.InverseTransformPoint(playerW);

            Matrix4x4 playerMatrix = minimapPos.localToWorldMatrix * Matrix4x4.Translate(cameraOffset) *
                                        Matrix4x4.Scale(Vector3.one * scale) * Matrix4x4.Translate(new Vector3(playerC.x, playerC.z, 1)) *
                                        Matrix4x4.Scale(Vector3.one * dotScale); // * Matrix4x4.Rotate(Quaternion.Euler(0, 0, -playerRot * rot));

            Graphics.DrawMesh(model, playerMatrix, playerMaterial, 0);
        }
        

        //minimapCamera.transform.position = new Vector3(minimapPos.transform.position.x * cameraOffset.x, minimapPos.transform.position.y * cameraOffset.y, minimapPos.transform.position.z * cameraOffset.z);

        //Camera move
        for (int i = 0; i < minimapCamera.Length; i++)
        {
            minimapCamera[i].transform.position = new Vector3(playerC.x * scale, playerC.z * scale, cameraDistance);

        }


        //Enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            Vector3 enemyW = enemies[i].position;
            Vector3 enemyC = minimapPos.InverseTransformPoint(enemyW);

            Matrix4x4 enemyMatrix = minimapPos.localToWorldMatrix * Matrix4x4.Translate(cameraOffset) *
                                    Matrix4x4.Scale(Vector3.one * scale) * Matrix4x4.Translate(new Vector3(enemyC.x, enemyC.z, 1)) *
                                    Matrix4x4.Scale(Vector3.one * dotScale);

            Graphics.DrawMesh(model, enemyMatrix, enemiesMaterial, 0);
        }

        //Security Camera
        for (int i = 0; i < securityCamera.Length; i++)
        {
            Vector3 secCameraW = securityCamera[i].position;
            Vector3 secCameraC = minimapPos.InverseTransformPoint(secCameraW);

            Matrix4x4 secCameraMatrix = minimapPos.localToWorldMatrix * Matrix4x4.Translate(cameraOffset) *
                                        Matrix4x4.Scale(Vector3.one * scale) * Matrix4x4.Translate(new Vector3(secCameraC.x, secCameraC.z, 1)) *
                                        Matrix4x4.Scale(Vector3.one * dotScale) * Matrix4x4.Rotate(Quaternion.Euler(0, 0, securityCamera[i].rotation.y * 360));

            Graphics.DrawMesh(model, secCameraMatrix, securityCameraMaterial, 0);
        }

        //Floor
        for (int i = 0; i < floor.Length; i++)
        {
            Vector3 floorW = floor[i].position;
            Vector3 floorC = minimapPos.InverseTransformPoint(floorW);

            Matrix4x4 floorMatrix = minimapPos.localToWorldMatrix * Matrix4x4.Translate(cameraOffset) *
                                    Matrix4x4.Scale(Vector3.one * scale) * Matrix4x4.Translate(new Vector3(floorC.x + floorOffset.x, floorC.z + floorOffset.y, 1.4f)) *
                                    Matrix4x4.Scale(Vector3.one * dotScale * floor[i].localScale.x);

            Graphics.DrawMesh(model, floorMatrix, floorMaterial, 0);
        }


        //Rooms Walls
        for (int i = 0; i < wall.Length; i++)
        {
            Vector3 wallW = wall[i].position;
            Vector3 wallC = minimapPos.InverseTransformPoint(wallW);

            if (wall[i].localRotation.y != 0) // 90º wall
            {
                wallMatrix = minimapPos.localToWorldMatrix * Matrix4x4.Translate(cameraOffset) *
                                    Matrix4x4.Scale(Vector3.one * scale) * Matrix4x4.Translate(new Vector3(wallC.x + wallHorOffset.x, wallC.z + wallHorOffset.y, 1.2f)) *
                                    Matrix4x4.Scale(new Vector3(wall[i].localScale.z * dotScale, 1 * dotScale, 1));
            }
            else // 0º wall
            {
                wallMatrix = minimapPos.localToWorldMatrix * Matrix4x4.Translate(cameraOffset) *
                                    Matrix4x4.Scale(Vector3.one * scale) * Matrix4x4.Translate(new Vector3(wallC.x - wallVerOffset.x, wallC.z - wallVerOffset.y, 1.2f)) *
                                    Matrix4x4.Scale(new Vector3(1 * dotScale, wall[i].localScale.z * dotScale, 1));
            }

            Graphics.DrawMesh(model, wallMatrix, wallMaterial, 0);
        }

        //Objects
        for (int i = 0; i < objects.Length; i++)
        {
            Vector3 objectW = objects[i].position;
            Vector3 objectC = minimapPos.InverseTransformPoint(objectW);

            Matrix4x4 objectMatrix = minimapPos.localToWorldMatrix * Matrix4x4.Translate(cameraOffset) *
                                    Matrix4x4.Scale(Vector3.one * scale) * Matrix4x4.Translate(new Vector3(objectC.x, objectC.z, 1.3f)) *
                                    Matrix4x4.Scale(Vector3.one * dotScale);

            Graphics.DrawMesh(model, objectMatrix, objectMaterial, 0);
        }


        //Up/Down ramps/stairs
        for (int i = 0; i < ramp.Length; i++)
         {
             Vector3 rampW = ramp[i].position;
             Vector3 rampC = minimapPos.InverseTransformPoint(rampW);

            Matrix4x4 rampMatrix = minimapPos.localToWorldMatrix * Matrix4x4.Translate(cameraOffset) *
                                    Matrix4x4.Scale(Vector3.one * scale) * Matrix4x4.Translate(new Vector3(rampC.x, rampC.z, 1.3f)) *
                                    Matrix4x4.Scale(Vector3.one * dotScale);

             Graphics.DrawMesh(model, rampMatrix, rampMaterial, 0);
         }

         
    }
}
