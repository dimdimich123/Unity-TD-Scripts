using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

enum EnemyLookDirection
{
    Right = 0,
    Left = 180
}

public class MovementPath : MonoBehaviour
{
    [SerializeField] private Transform[] PathPoints; // Массив точек пути движения
    [SerializeField] private EnemyLookDirection[] EnemyLookDirectionOnPoints;

    // private void OnDrawGizmos() // Прорисовка линий, для удобства редактирования точек
    // {
    //     if(PathPoints == null || PathPoints.Length < 2)
    //     {
    //         return;
    //     }

    //     for(ushort i = 0; i < PathPoints.Length - 1; ++i)
    //     {
    //         Gizmos.DrawLine(PathPoints[i].position, PathPoints[i + 1].position);
    //         Handles.Label(PathPoints[i + 1].position, (i + 1).ToString());
    //     }
    // }

    public Transform[] GetPathPoints()
    {
        return PathPoints;
    }

    public int GetLookDirection(int index)
    {
        return (int)EnemyLookDirectionOnPoints[index];
    }

    public int[] GetLookDirection()
    {
        int[] Temp = new int[EnemyLookDirectionOnPoints.Length];
        for(int i = 0; i < Temp.Length; ++i)
        {
            Temp[i] = (int)EnemyLookDirectionOnPoints[i];
        }
        return Temp;
    }
}
