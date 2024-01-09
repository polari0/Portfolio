using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Assertions.Must;
using Unity.VisualScripting.Antlr3.Runtime;
using TMPro;

namespace HexKeyGames
{
    namespace Elevator
    {
        /// <summary>
        /// This script handles the Interactions 
        /// </summary>
        public class Elevator : MonoBehaviour
        {
            [SerializeField, Tooltip("List of all the floor/buttons in the elevator")]
            private List<int> elevatorFloors = new List<int>();
            [SerializeField]
            private GameObject elevatorPrefab;
            [SerializeField]
            private int floorHeight;
            [SerializeField]
            private float elevatorSpeed;
            private bool elevatorMoving = false;
            [SerializeField]
            private List<Vector3> elevatorPositions = new List<Vector3>();
            private Vector3 elevatorNextPosition;

            #region Elevator Animation variables
            private Animation elevatorDoorsAnim;
            private string elevatorDoorsAnimName;
            [SerializeField]
            private float elevatorDoorsAnimSpeed = 1f;
            private Tween elevatorTween;
            [SerializeField]
            private bool doorsOpen = false;
            [SerializeField]
            private TextMeshPro textInside, textOutside;
            #endregion

            #region sound stuff
            [Header("Sound Effects settings")]

            public AudioClip Bell;
            [Range(0, 1)]
            public float BellVolume = 1;

            public AudioClip DoorsOpen;
            [Range(0, 1)]
            public float DoorsOpenVolume = 1;

            public AudioClip DoorsClose;
            [Range(0, 1)]
            public float DoorsCloseVolume = 1;

            public AudioClip ElevatorMove;
            [Range(0, 1)]
            public float ElevatorMoveVolume = 1;

            public AudioClip ElevatorBtn;
            [Range(0, 1)]
            public float ElevatorBtnVolume = 1;

            public AudioClip ElevatorError;
            [Range(0, 1)]
            public float ElevatorErrorVolume = 1;

            private AudioSource elevatorAudioSourceFX;
            #endregion


            private void Awake()
            {
                elevatorAudioSourceFX = GetComponent<AudioSource>();
                elevatorDoorsAnim = gameObject.GetComponent<Animation>();
                elevatorDoorsAnimName = elevatorDoorsAnim.clip.name;
            }

            private void Start()
            {
                foreach (int v in elevatorFloors)
                {
                    elevatorPositions.Add(new Vector3(elevatorPrefab.transform.position.x, elevatorPrefab.transform.position.y + floorHeight * v, elevatorPrefab.transform.position.z));
                }
            }
            /// <summary>
            /// this is how we check were the elevator should go next
            /// </summary>
            /// <param name="up">Should we go up or down?</param>
            public void FindNextElevatorFloor(bool up)
            {
                if (!elevatorMoving)
                {
                    if (up)
                    {
                        if ((elevatorPrefab.transform.position + new Vector3(0, floorHeight, 0)).y <= elevatorPositions.Last().y)
                        {
                            elevatorNextPosition = elevatorPrefab.transform.position + new Vector3(0, floorHeight, 0);
                            Debug.Log(elevatorNextPosition);
                            MoveElevator(elevatorNextPosition);
                            UpdateTextFields();
                        }
                    }
                    else if (!up)
                    {
                        if ((elevatorPrefab.transform.position - new Vector3(0, floorHeight, 0)).y >= elevatorPositions.First().y)
                        {
                            elevatorNextPosition = elevatorPrefab.transform.position + new Vector3(0, -floorHeight, 0);
                            Debug.Log(elevatorNextPosition);
                            MoveElevator(elevatorNextPosition);
                            UpdateTextFields();
                        }
                    } 
                }
            }

            /// <summary>
            /// Here we actually move the elevator
            /// </summary>
            /// <param name="movePoint">Where to move the elevator</param>
            private void MoveElevator(Vector3 movePoint)
            {
                elevatorAudioSourceFX.clip = ElevatorMove;
                elevatorTween = elevatorPrefab.transform.DOMove(movePoint, elevatorSpeed, false).OnComplete(DoorsOpening).SetDelay(2);
                elevatorAudioSourceFX.Play();
            }

            private void UpdateTextFields()
            {
                float currentFloor = elevatorNextPosition.y / floorHeight;
                textInside.text = currentFloor.ToString("0");
                textOutside.text = currentFloor.ToString("0");

            }

            /// <summary>
            /// In fixed update we check if the elevator is moving or not 
            /// </summary>
            private void FixedUpdate()
            {
                if(elevatorPrefab.transform.position.y % floorHeight != 0)
                {
                    elevatorMoving = true;
                }
                else if (elevatorPrefab.transform.position.y % floorHeight == 0)
                {
                    elevatorMoving = false;
                }

            }
            #region Opening and closing the doors 
            public void DoorsOpening()
            {
                elevatorAudioSourceFX.clip = DoorsOpen;
                elevatorDoorsAnim[elevatorDoorsAnimName].normalizedTime = 0;
                elevatorDoorsAnim[elevatorDoorsAnimName].speed = elevatorDoorsAnimSpeed;
                elevatorDoorsAnim.Play();
                elevatorAudioSourceFX.Play();
                doorsOpen = true;
            }

            public void DoorsClosing()
            {
                if (doorsOpen)
                {
                    elevatorAudioSourceFX.clip = DoorsClose;
                    elevatorDoorsAnim[elevatorDoorsAnimName].normalizedTime = 1;
                    elevatorDoorsAnim[elevatorDoorsAnimName].speed = -elevatorDoorsAnimSpeed;
                    elevatorDoorsAnim.Play();
                    elevatorAudioSourceFX.Play();
                    doorsOpen = false;
                }
            }
            #endregion


            private void OnTriggerEnter(Collider other)
            {
                other.transform.parent = elevatorPrefab.transform;
            }
            private void OnTriggerExit(Collider other)
            {
                other.transform.parent = null;
            }
        } 
    }
}
