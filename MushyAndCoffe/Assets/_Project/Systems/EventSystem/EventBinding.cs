using System;

namespace EventSystem
{
	internal interface IEventBinding<T>
	{
		public Action<T> OnEvent { get; set; }
		public Action OnEventNoArgs { get; set; }
	}

	public class EventBinding<T> : IEventBinding<T> where T : IEvent
	{
		Action<T> onEvent = _ => { };
		Action onEventNoArgs = () => { };

		Action<T> IEventBinding<T>.OnEvent { get => onEvent; set => onEvent = value; }
		Action IEventBinding<T>.OnEventNoArgs { get => onEventNoArgs; set => onEventNoArgs = value; }

		// Constructors
		public EventBinding(Action<T> onEvent) => this.onEvent = onEvent;
		public EventBinding(Action onEventNoArgs) => this.onEventNoArgs = onEventNoArgs;

		// Add and remove events on runtime 
		public void Add(Action<T> onEvent) => this.onEvent += onEvent;
		public void Remove(Action<T> onEvent) => this.onEvent -= onEvent;
		public void Add(Action onEventNoArgs) => this.onEventNoArgs += onEventNoArgs;
		public void Remove(Action onEventNoArgs) => this.onEventNoArgs -= onEventNoArgs;
	}
}
