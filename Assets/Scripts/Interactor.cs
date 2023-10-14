using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Interactor : MonoBehaviourPunCallbacks
{
    public float distance;
    public LayerMask layerMask;
    public GameObject currentInteractableObject = null;
    public Transform grabPosition;

    private Camera camera;
    private CanvasUserInterface playerUI;

    void Start()
    {
        if (!photonView.IsMine) return;

        camera = gameObject.GetComponentInChildren<Camera>();
        playerUI = gameObject.GetComponentInChildren<CanvasUserInterface>();

        Debug.Log("Interactor Start()");
        Debug.Log("Interactor camera: " + camera.name);
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        ShowInteractableObjectUI();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropCurrentObject();
        }

    }

    void Interact()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance, layerMask))
        {
            var interactable = hit.transform.gameObject.GetComponent<Interactable>();
            if (interactable == null) return;

            currentInteractableObject = hit.transform.gameObject;

            if (!interactable.isTrigger)
            {
                GrabCurrentObject();
                return;
            }
        }
    }

    void ShowInteractableObjectUI()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        if (Physics.Raycast(ray, out hit, distance, layerMask))
        {

            if (hit.transform.gameObject.GetComponent<Interactable>() != null)
            {
                Debug.DrawRay(ray.origin, ray.direction * distance, Color.green);
                playerUI.showInteractUI(true);
                return;
            }
        }

        playerUI.showInteractUI(false);
        return;
    }

    void GrabCurrentObject()
    {
        Debug.Log("Requesting ownership");
        photonView.RequestOwnership();

        int objectId = currentInteractableObject.GetPhotonView().ViewID;
        photonView.RPC("RPC_GrabObject", RpcTarget.AllBuffered, objectId);
    }

    void DropCurrentObject()
    {
        if (currentInteractableObject == null) return;

        int objectId = currentInteractableObject.GetPhotonView().ViewID;
        photonView.RPC("RPC_DropObject", RpcTarget.AllBuffered, objectId);

    }

    [PunRPC]
    void RPC_GrabObject(int objectId)
    {
        GameObject objectToGrab = PhotonView.Find(objectId)?.gameObject;

        if (objectToGrab.transform.parent != grabPosition)
        {
            Debug.Log("Grabbing object");
            objectToGrab.transform.SetParent(grabPosition);
            objectToGrab.transform.position = grabPosition.position;
            objectToGrab.GetComponent<Rigidbody>().useGravity = false;
            objectToGrab.GetComponent<BoxCollider>().enabled = false;
        }
    }

    [PunRPC]
    void RPC_DropObject(int objectId)
    {
        GameObject objectToDrop = PhotonView.Find(objectId)?.gameObject;

        objectToDrop.GetComponent<BoxCollider>().enabled = true;
        objectToDrop.transform.SetParent(null);
        objectToDrop.GetComponent<Rigidbody>().useGravity = true;
        currentInteractableObject = null;
        objectToDrop = null;
    }

}
