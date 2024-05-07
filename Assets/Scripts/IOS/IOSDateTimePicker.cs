using System;

public class IOSDateTimePicker : ISN_Singleton<IOSDateTimePicker>
{
	public Action<DateTime> OnDateChanged = delegate
	{
	};

	public Action<DateTime> OnPickerClosed = delegate
	{
	};

	public void Show(IOSDateTimePickerMode mode)
	{
	}

	private void DateChangedEvent(string time)
	{
		DateTime obj = DateTime.Parse(time);
		OnDateChanged(obj);
	}

	private void PickerClosed(string time)
	{
		DateTime obj = DateTime.Parse(time);
		OnPickerClosed(obj);
	}
}
