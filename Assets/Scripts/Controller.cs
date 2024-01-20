using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Controller : Part
{
    
    [SerializeField]
    private GameObject list; //обьект списка кнопок
    public CamController camController; // скрипт управления камерой
    public GameObject cameraTarget;
    public Button expandCollapseButton; //кнопка сворачивания и разворачивания схемы
    [SerializeField]
    private GameObject planetarniyTarget;
    private Vector3[] initialPositions; // Исходные позиции объектов
    private bool isExpanded = false; // Состояние объектов (разъехались или нет)
    [Header("Настройки движения чвстей детали")]
    [SerializeField]
    private float smoothness = 1f; // Плавность движения
    [SerializeField]
    private float distance = 1f; // Расстояние от центра
    void Start()
    {
        initialPositions = new Vector3[parts.Length];
        for (int i = 0; i < parts.Length; i++)
        {
            initialPositions[i] = parts[i].transform.localPosition;
        }
    }
    public void OnButtonClick()
    {
        StartCoroutine(isExpanded ? Collapse() : Expand());
        isExpanded = !isExpanded;
        ShowAllObjects();
        StartCoroutine(ButtonsActivated());
        StartCoroutine(StartButtonActivated());

    }
    IEnumerator StartButtonActivated()
    {
        expandCollapseButton.interactable = false;
        yield return new WaitForSeconds(1);
        expandCollapseButton.interactable = true;
    }
    IEnumerator StartButtonActivatedCollapse()
    {
        expandCollapseButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        expandCollapseButton.gameObject.SetActive(true);
    }

    IEnumerator ButtonsActivated()
    {      
        if (partsButtons[0].gameObject.activeSelf)
        {
            for (int i = 0; i < partsButtons.Length; i++)
            {
                partsButtons[i].interactable = false;
            }
        }
        yield return new WaitForSeconds(1);
        if (partsButtons[0].gameObject.activeSelf)
        {
            Debug.Log("3");
            for (int i = 0; i < partsButtons.Length; i++)
            {
                partsButtons[i].interactable = true;
            }
        } 
    }
    IEnumerator CloseTarget()
    {
        camController.SetCameraTarget(cameraTarget.transform, expandOffset);
        yield return new WaitForSeconds(1);
        camController.SetCameraTarget(cameraTarget.transform, collapseOffset);
    }
    IEnumerator Expand() //разворачивание детали
    {
        camController.SetCameraTarget(planetarniyTarget.transform,expandOffset);
        list.SetActive(true);
        for (float t = 0; t < 1; t += Time.deltaTime / smoothness)
        {
            for (int i = 0; i < parts.Length; i++)
            {
                float direction = i < parts.Length / 2 ? i : -i + parts.Length / 2;

                parts[i].transform.localPosition = Vector3.Lerp(initialPositions[i], initialPositions[i] + new Vector3(0, direction * distance, 0), t);
            }
            yield return null;
        }
    }
    IEnumerator Collapse() // сворачивание детали
    {
        StartCoroutine(StartButtonActivatedCollapse());
        StartCoroutine(CloseTarget());
        expandCollapseButton.interactable = false;
        list.SetActive(false);
        for (float t = 0; t < 1; t += Time.deltaTime / smoothness)
        {
            for (int i = 0; i < parts.Length; i++)
            {
                float direction = i < parts.Length / 2 ? i : -i + parts.Length / 2;
                parts[i].transform.localPosition = Vector3.Lerp(initialPositions[i] + new Vector3(0, direction * distance, 0), initialPositions[i], t);
            }
            expandCollapseButton.interactable = true;
            yield return null;
        }
    }
    private int selectedPart = 100;
    public void SelectPart(int numPart)
        {
        if(selectedPart == numPart)
        {
            camController.SetCameraTarget(cameraTarget.transform, expandOffset);
            ShowAllObjects();
        }
        else
        {
            selectedPart = numPart;
            if (isActive[numPart] == false)
            {
                camController.SetCameraTarget(parts[numPart].transform.GetChild(0).transform, partsOffset[numPart]);
                ShowOneObject(parts[numPart]);
            }
            else
            {
                camController.SetCameraTarget(cameraTarget.transform, expandOffset);
                ShowAllObjects();
            }
        }
        
    }
    private void ShowAllObjects() // отображение всех обьектов детали
    {
        for (int i = 0; i < parts.Length; i++)
        {
            partsButtons[i].interactable = true;
            parts[i].SetActive(true);
            isActive[i] = false;
            selectedPart = 100;
            var colors = partsButtons[i].colors;
            colors.normalColor = Color.black;
            colors.selectedColor = Color.black;
            partsButtons[i].colors = colors;
        }
        
    }
    private void ShowOneObject(GameObject obj) // отображение одного объекта детали
    {
        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i] != obj)
            {
                parts[i].SetActive(false);
                isActive[i] = false;
                //partsButtons[i].interactable = true;
                var colors = partsButtons[i].colors;
                colors.normalColor = Color.black;
                colors.selectedColor = Color.black;
                partsButtons[i].colors = colors;
            }
            else
            {
                var colors = partsButtons[i].colors;
                colors.normalColor = Color.green;
                colors.selectedColor = Color.green;
                partsButtons[i].colors = colors;
                //partsButtons[i].interactable = false;
                parts[i].SetActive(true);
                isActive[i] = true;
            } 
                
        }
    }
}