using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    public GameObject plusBrick;
    public GameObject minusBrick;
    public GameObject multiplyBrick;
    public GameObject numberBrickPrefab;
    private List<Vector2> locations;
    private List<Vector2> activeLocations;
    private List<GameObject> activeBricks;
    private List<GameObject> activeOperationBricks;

    void Start()
    {
        locations = new List<Vector2>();
        activeLocations = new List<Vector2>();
        activeBricks = new List<GameObject>();
        activeOperationBricks = new List<GameObject>();

        SetLocations();
    }

    void Update()
    {

    }

    public void SpawnBricks(List<int> brickNumbers)
    {
        int randomIndex = Random.Range(0, locations.Count - 1);
        GameObject go;

        go = Instantiate(plusBrick);
        go.SetActive(true);
        go.transform.SetParent(transform);
        go.transform.position = locations[randomIndex];
        go.isStatic = true;
        activeLocations.Add(locations[randomIndex]);
        locations.RemoveAt(randomIndex);
        activeOperationBricks.Add(go);

        randomIndex = Random.Range(0, locations.Count - 1);
        go = Instantiate(minusBrick);
        go.SetActive(true);
        go.transform.SetParent(transform);
        go.transform.position = locations[randomIndex];
        go.isStatic = true;
        activeLocations.Add(locations[randomIndex]);
        locations.RemoveAt(randomIndex);
        activeOperationBricks.Add(go);

        randomIndex = Random.Range(0, locations.Count - 1);
        go = Instantiate(multiplyBrick);
        go.SetActive(true);
        go.transform.SetParent(transform);
        go.transform.position = locations[randomIndex];
        go.isStatic = true;
        activeLocations.Add(locations[randomIndex]);
        locations.RemoveAt(randomIndex);
        activeOperationBricks.Add(go);

        foreach (int i in brickNumbers)
        {
            randomIndex = Random.Range(0, locations.Count - 1);
            go = Instantiate(numberBrickPrefab);
            go.SetActive(true);
            go.transform.SetParent(transform);
            go.transform.position = locations[randomIndex];
            go.isStatic = true;
            activeBricks.Add(go);

            activeLocations.Add(locations[randomIndex]);
            locations.RemoveAt(randomIndex);

            NumberBlock numberBlock = go.GetComponent<NumberBlock>();
            numberBlock.number = i;
            numberBlock.UpdateText();
        }
    }

    public void SetLocations()
    {
        locations.Add(new Vector2(11, 8));
        locations.Add(new Vector2(11, 19));
        locations.Add(new Vector2(11, 30));
        locations.Add(new Vector2(22, 8));
        locations.Add(new Vector2(22, 19));
        locations.Add(new Vector2(22, 30));
        locations.Add(new Vector2(33, 8));
        locations.Add(new Vector2(33, 19));
        locations.Add(new Vector2(33, 30));
        locations.Add(new Vector2(44, 8));
        locations.Add(new Vector2(44, 19));
        locations.Add(new Vector2(44, 30));
        locations.Add(new Vector2(55, 8));
        locations.Add(new Vector2(55, 19));
        locations.Add(new Vector2(55, 30));
    }
}
