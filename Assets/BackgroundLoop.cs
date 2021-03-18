using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public GameObject[] objects;
    private Camera mainCamera;
    private Vector2 screenBounds;
    public float choke;

    private void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        foreach (GameObject obj in objects)
        {
            loadChildObjects(obj);
        }
    }

    void loadChildObjects(GameObject obj)
    {
        float objectHeigth = obj.GetComponent<SpriteRenderer>().bounds.size.y - choke;
        int childsNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / objectHeigth);
        GameObject clone = Instantiate(obj);
        for (int i = 0; i <= childsNeeded; i++)
        {
            GameObject c = Instantiate(clone);
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(obj.transform.position.x, objectHeigth * i, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    private void repositionChildObjects(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length > 1)
        {
            GameObject first = children[1].gameObject;
            GameObject last = children[children.Length - 1].gameObject;
            float halfObjectHeigth = last.GetComponent<SpriteRenderer>().bounds.extents.y - choke;
            if (transform.position.y + screenBounds.y > last.transform.position.y + halfObjectHeigth)
            {
                first.transform.SetAsLastSibling();
                first.transform.position = new Vector3(last.transform.position.x, last.transform.position.y + halfObjectHeigth * 2, last.transform.position.z);
            }
        }
    }

    private void LateUpdate()
    {
        foreach (GameObject obj in objects)
        {
            repositionChildObjects(obj);
        }
    }
}
