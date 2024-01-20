using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Part : MonoBehaviour
{
    public Dictionary<int, bool> isActive = new()
    {
        { 0, false},
        { 1, false},
        { 2, false},
        { 3, false},
        { 4, false},
        { 5, false},
        { 6, false},
        { 7, false},
        { 8, false},
        { 9, false},
        { 10, false},
        { 11, false},
        { 12, false},
        { 13, false},
    };
    public GameObject[] parts; // Массив объектов
    public Button[] partsButtons; // Массив кнопок для выбора части детали
    public Vector3[] partsOffset;
    public Vector3 collapseOffset;
    public Vector3 expandOffset;
}
