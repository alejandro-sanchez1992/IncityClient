using System;
using System.Globalization;

namespace IncityClient.Prism.Interfaces
{
	public interface ILocalize
	{
		CultureInfo GetCurrentCultureInfo();

		void SetLocale(CultureInfo ci);
	}
}
