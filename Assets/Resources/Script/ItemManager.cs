using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    public static bool isChecked = false;
    bool isDelete = false;
    bool isUpgrade = false;
    bool isChange = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        deleteObject();
        upgradeObject();
    }

    public void checkClicked()
    {
        GameObject.Find("AreaLight").GetComponent<Light>().enabled = !GameObject.Find("AreaLight").GetComponent<Light>().enabled;
        isChecked = !GameObject.Find("AreaLight").GetComponent<Light>().enabled;
    }

    void deleteObject()
    {
        if (!isDelete) return;

        RaycastHit hit;
        GameObject target = null;
        if (Input.GetMouseButton(0))
        {
            Vector3 pointer = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

            Ray ray = Camera.main.ScreenPointToRay(pointer);
            
            if (Physics.Raycast(ray.origin, ray.direction * 10, out hit) && hit.collider.gameObject.tag == "board")
            {
                target = hit.collider.gameObject;
                Destroy(target);
                checkClicked();
                isDelete = false;
            }
        }        
    }
    void upgradeObject()
    {
        if (!isUpgrade) return;

        RaycastHit hit;
        GameObject target = null;
        if (Input.GetMouseButton(0))
        {
            Vector3 pointer = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

            Ray ray = Camera.main.ScreenPointToRay(pointer);
            Debug.Log(ray);
            if (Physics.Raycast(ray.origin, ray.direction * 10, out hit) && hit.collider.gameObject.tag == "board")
            {
                target = hit.collider.gameObject;

                target.GetComponent<Square>().value = target.GetComponent<Square>().value * 2;
                target.transform.FindChild("Text").GetComponent<TextMesh>().text = target.GetComponent<Square>().value.ToString();
                target.GetComponent<SpriteRenderer>().sprite = Square.curImgArray[Square.getPow(target.GetComponent<Square>().value) - 1];             

                checkClicked();
                isUpgrade = false;
            }
        }
    }

    public void setDelete()
    {
        checkClicked();
        isDelete = isChecked;
        isUpgrade = false;
        isChange = false;
    }
    public void setUpgrade()
    {
        checkClicked();
        isDelete = false;
        isUpgrade = isChecked;
        isChange = false;
    }
    public void setChange()
    {
        checkClicked();
        isDelete = false;
        isUpgrade = false;
        isChange = isChecked;
    }
}
