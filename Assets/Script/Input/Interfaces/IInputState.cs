using UnityEngine;

namespace BBX.Input.Interfaces
{
	public interface IInputState
	{
		/// <summary>
		/// Input on X-Axis
		/// </summary>
		float XAxis { get; set; }
		
		/// <summary>
		/// Input on Y-Axis
		/// </summary>
		float YAxis { get; set; }
		
		/// <summary>
		/// If the shift button is held
		/// </summary>
		bool Shift { get; set; }
		
		/// <summary>
		/// If the jump button is held
		/// </summary>
		bool Control { get; set; }

		/// <summary>
		/// When the input first happens
		/// </summary>
		bool LeftMouseDown { get; set; }
		
		/// <summary>
		/// When the input is held
		/// </summary>
		bool LeftMouseDragging { get; set; }
		
		/// <summary>
		/// When the input ends
		/// </summary>
		bool LeftMouseUp { get; set; }
		
		/// <summary>
		/// If this input was a tap
		/// </summary>
		bool LeftMouseClicked { get; set; }
		
		/// <summary>
		/// When the input first happens
		/// </summary>
		bool RightMouseDown { get; set; }
		
		/// <summary>
		/// When the input is held
		/// </summary>
		bool RightMouseHeld { get; set; }
		
		/// <summary>
		/// When the input ends
		/// </summary>
		bool RightMouseUp { get; set; }
		
		/// <summary>
		/// If this input was a tap
		/// </summary>
		bool RightMouseClicked { get; set; }

		/// <summary>
		/// Screen position of input
		/// </summary>
		Vector2 PointerPosition { get; set; }
		
		/// <summary>
		/// Call this to reset the states to default 
		/// </summary>
		void Reset();
	}
}