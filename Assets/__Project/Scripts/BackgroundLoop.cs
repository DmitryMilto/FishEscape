using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public GameObject[] levels;

    private Camera _camera;
    private Vector2 screenBounds;

    public float choke;

    private void Start()
    {
        _camera = Camera.main;
        screenBounds = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));
        foreach (var level in levels)
            LoadChildObject(level);
    }
    private void LoadChildObject(GameObject obj)
    {
        var objWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x - choke;
        var childsNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / objWidth);
        GameObject clone = Instantiate(obj) as GameObject;
        for (var i = 0; i < childsNeeded; i++)
        {
            var c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(objWidth * i, obj.transform.position.y, obj.transform.position.z);
            c.name = $"{obj.name} {i}";
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    private void RepositionChildObjects(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length == 0) return;
        var firstChild = children[1].gameObject;
        var lastChild = children[children.Length - 1].gameObject;

        float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x - choke;
        if(transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth)
        {
            firstChild.transform.SetAsLastSibling();
            firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth * 2, lastChild.transform.position.y, lastChild.transform.position.z);
        }
        else if (transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth * 2)
        {
            lastChild.transform.SetAsFirstSibling();
            lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjectWidth * 2, firstChild.transform.position.y, firstChild.transform.position.z);
        }
    }
    private void LateUpdate()
    {
        foreach (var obj in levels)
        {
            RepositionChildObjects(obj);
        }
    }
}
