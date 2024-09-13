using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNetwork : MonoBehaviour
{
    public bool generateNetwork = true;
    private int roomCount = 10;
    private List<GameObject> listOfRooms = new List<GameObject>();
    private GameObject cubeRoom;
    private GameObject cylinderTunnel;
    
    private Vector3 roomPos = new Vector3(1, 1, 1);
    
    private System.Random rngx = new System.Random(1);
    private System.Random rngy = new System.Random(2);
    private System.Random rngz = new System.Random(3);
    private System.Random rngsize = new System.Random(4);   
    private int xOffset, yOffset, zOffset;

    void Start()
    {
        if (generateNetwork == true)
        {
            cubeRoom = Resources.Load("Cube") as GameObject;
            cylinderTunnel = Resources.Load("Cylinder") as GameObject;

            while (roomCount != 0)
            {
                GameObject tempRoom = Instantiate(cubeRoom) as GameObject;
                tempRoom.transform.position = roomPos;
                /*int scalar = 0;
                while(scalar == 0) { scalar = rngsize.Next(1, 100) / 25; }
                temp.transform.localScale *= scalar;*/

                listOfRooms.Add(tempRoom);

                xOffset = rngx.Next(-100, 100);
                yOffset = rngy.Next(-100, 100);
                //zOffset = rngz.Next(-100, 100);

                roomPos.x = xOffset / 10;
                roomPos.y = yOffset / 10;
                //roomPos.z = zOffset / 10;
                roomCount--;
            }

            for (int i = 0; i < listOfRooms.Count - 1; i++)
            {
                Vector3 a = listOfRooms[i].transform.position;
                Vector3 b = listOfRooms[i + 1].transform.position;
                Vector2 a2 = new Vector2(a.x, a.y);
                Vector2 b2 = new Vector2(b.x, b.y);
                Vector2 a3 = new Vector2(a.x, a.z);
                Vector2 b3 = new Vector2(b.x, b.z);
                GameObject tempTunnel = Instantiate(cylinderTunnel) as GameObject;

                Vector3 cylinderPos = midpointBTV(a, b);
                Vector2 cylinderPos2 = new Vector2(cylinderPos.x, cylinderPos.y);
                double cylinderY = Math.Sqrt(Math.Pow(b.x - a.x, 2) + Math.Pow(b.y - a.y, 2) + Math.Pow(b.z - a.z, 2));
                double zrot;
                if (cylinderPos2.y < a2.y)
                    zrot = angleBTVv2(a2, b2);
                else
                    zrot = angleBTVv2(b2, a2);

                tempTunnel.transform.position = cylinderPos;
                tempTunnel.transform.localScale = new Vector3(1, (float)cylinderY / 2, 1);
                tempTunnel.transform.RotateAround(cylinderPos, new Vector3(0f, 0f, 1f), (float)zrot);
                //tempTunnel.transform.RotateAround(cylinderPos, new Vector3(0f,1f,0f), (float)yrot);
            }
        }
    }

    #region // Vector Math

    double angleBTVv2(Vector2 m_a, Vector2 m_b)
    {
        double y = m_b.y - m_a.y;
        double x = m_b.x - m_a.x;
        double rad = Math.Atan(x/y);
        double degrees = radToDeg(rad);
        //if (Math.Abs(degrees) == degrees)
        //    degrees += 6;
        //else
        //    degrees -= 6;
        return -degrees;
    }
    /// <summary>
    /// Finds the angle between two Vector3
    /// </summary>
    /// <param name="m_a"></param>
    /// <param name="m_b"></param>
    /// <returns></returns>
    double angleBTV(Vector3 m_a, Vector3 m_b)
    {
        double dotProdOfAB = dotProdBTV(m_a, m_b);
        double magA = magnitudeOOV(m_a);
        double magB = magnitudeOOV(m_b);
        double angleRadians = Math.Acos(dotProdOfAB / (magA * magB));
        double angleDegrees =  radToDeg(angleRadians);
        return angleDegrees;
    }
    /// <summary>
    /// Finds the angle between two Vector2
    /// </summary>
    /// <param name="m_a"></param>
    /// <param name="m_b"></param>
    /// <returns></returns>
    double angleBTV(Vector2 m_a, Vector2 m_b)
    {
        double dotProdOfAB = dotProdBTV(m_a, m_b);
        double magA = magnitudeOOV(m_a);
        double magB = magnitudeOOV(m_b);
        double angleRadians = Math.Acos(dotProdOfAB / (magA * magB));
        double angleDegrees = radToDeg(angleRadians);
        return angleDegrees;
    }

    /// <summary>
    /// Converts from radian to degrees
    /// </summary>
    /// <param name="m_radians"></param>
    /// <returns></returns>
    double radToDeg(double m_radians)
    {
        double degrees = (180/Math.PI) * m_radians;
        return degrees;
    }

    /// <summary>
    /// Finds the dot product between two Vector3
    /// </summary>
    /// <param name="m_a"></param>
    /// <param name="m_b"></param>
    /// <returns></returns>
    double dotProdBTV(Vector3 m_a, Vector3 m_b)
    {
        double dotProdOfAB = (m_a.x * m_b.x) + (m_a.y * m_b.y) + (m_a.z * m_b.z);
        return dotProdOfAB;
    }
    /// <summary>
    /// Finds the dot product between two Vector2
    /// </summary>
    /// <param name="m_a"></param>
    /// <param name="m_b"></param>
    /// <returns></returns>
    double dotProdBTV(Vector2 m_a, Vector2 m_b)
    {
        double dotProdOfAB = (m_a.x * m_b.x) + (m_a.y * m_b.y);
        return dotProdOfAB;
    }

    /// <summary>
    /// Finds the magnitude of Vector3
    /// </summary>
    /// <param name="m_a"></param>
    /// <returns></returns>
    double magnitudeOOV(Vector3 m_a)
    {
        double magA = Math.Sqrt(Math.Pow(m_a.x, 2) + Math.Pow(m_a.y, 2) + Math.Pow(m_a.z, 2));
        return magA;
    }
    /// <summary>
    /// Finds the magnitude of Vector2
    /// </summary>
    /// <param name="m_a"></param>
    /// <returns></returns>
    double magnitudeOOV(Vector2 m_a)
    {
        double magA = Math.Sqrt(Math.Pow(m_a.x, 2) + Math.Pow(m_a.y, 2));
        return magA;
    }

    /// <summary>
    /// Finds the midpoint between two Vector3
    /// </summary>
    /// <param name="m_a"></param>
    /// <param name="m_b"></param>
    /// <returns></returns>
    Vector3 midpointBTV(Vector3 m_a, Vector3 m_b)
    {
        Vector3 midpoint = (m_a + m_b) / 2;
        return midpoint;
    }
    /// <summary>
    /// Finds the midpoint between two Vector2
    /// </summary>
    /// <param name="m_a"></param>
    /// <param name="m_b"></param>
    /// <returns></returns>
    Vector3 midpointBTV(Vector2 m_a, Vector2 m_b)
    {
        Vector3 midpoint = (m_a + m_b) / 2;
        return midpoint;
    }

    #endregion

    void FixedUpdate() {}
}
