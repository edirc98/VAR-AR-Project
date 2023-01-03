using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("HenSpawner")]
    public GameObject HenPlacePrefab;
    
    [Header("Fox")]
    public GameObject FoxPrefab;

    [Header("UI Elements")]
    public Button StartButton;


    private GameObject FoxPlaceholder;
    private GameObject HenPlaceholder;

    private GameObject FoxInstance;
    private GameObject HenPlaceInstance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(FoxPlaceholder == null)
        {
            FoxPlaceholder = GameObject.Find("FoxPlaceholder");
        }
        if(HenPlaceholder == null)
        {
            HenPlaceholder = GameObject.Find("HenPlacePlaceholder");
        }

    }



    public void PlaceInPlane()
    {
        if(FoxInstance.transform.position.y < HenPlaceInstance.transform.position.y)
        {
            HenPlaceInstance.transform.position = new Vector3(HenPlaceInstance.transform.position.x, FoxInstance.transform.position.y, HenPlaceInstance.transform.position.z); 
        }
        else
        {
            FoxInstance.transform.position = new Vector3(FoxInstance.transform.position.x, HenPlaceInstance.transform.position.y, FoxInstance.transform.position.z);
        }
    }


    public void StartGame()
    {
        FoxInstance = Instantiate(FoxPrefab, FoxPlaceholder.transform.position, Quaternion.identity);
        HenPlaceInstance = Instantiate(HenPlacePrefab, HenPlaceholder.transform.position, Quaternion.identity);
        HenPlaceInstance.GetComponent<HenSpawner>().activateSpawner = true; 

        HenPlaceholder.GetComponent<MeshRenderer>().enabled = false;
        FoxPlaceholder.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;

        StartButton.gameObject.SetActive(false);
        PlaceInPlane(); 
    }
}


