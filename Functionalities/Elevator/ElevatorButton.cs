using DG.Tweening;
using System.Linq;
using UnityEngine;

namespace HexKeyGames
{
	namespace Elevator
	{
		/// <summary>
		/// this class handles the elevator buttons and what they should do. 
		/// </summary>
		public class ElevatorButton : MonoBehaviour, IInteractablePrimary
		{
			[SerializeField]
			private Elevator my_e;

			public elevatorButtons elevatorButtonType;

            public void Interact()
			{
                switch (elevatorButtonType)
                {
					case elevatorButtons.Up:
						my_e.DoorsClosing();
						my_e.FindNextElevatorFloor(true);
						break;
					case elevatorButtons.Down:
						my_e.DoorsClosing();
						my_e.FindNextElevatorFloor(false);
						break;
					case elevatorButtons.CloseDoor:
						my_e.DoorsClosing();
						break;
					case elevatorButtons.OpenDoor:
						my_e.DoorsOpening();
						break;
                }
			}
        } 
	}
}

public enum elevatorButtons
{
	Up,
	Down, 
	CloseDoor,
	OpenDoor
}
