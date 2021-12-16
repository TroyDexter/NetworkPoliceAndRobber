using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPunCallbacks, IPunObservable
{
    private Drive drive;
    
    private Vector3 curPos;
    private Quaternion curRot;

    private void Awake()
    {
        drive = GetComponent<Drive>();
    }

    private void Update()
    {
        // update opponent's movement
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, curPos, drive.speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, curRot, drive.rotationSpeed * Time.deltaTime);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else if (stream.IsReading)
        {
            curPos = (Vector3)stream.ReceiveNext();
            curRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
