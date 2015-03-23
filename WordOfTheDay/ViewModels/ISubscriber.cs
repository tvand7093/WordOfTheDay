using System;

namespace WordOfTheDay.ViewModels
{
	public interface ISubscriber
	{
		void Subscribe();
		void Unsubscribe();
	}
}

