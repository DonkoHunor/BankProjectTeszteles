using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject
{
	/// <summary>
	/// Nem létező számla estén dobhatjuk bármejik függvényből
	/// </summary>
	internal class HibasSzamlaszamException : Exception
	{
		/// <summary>
		/// Létrehozza a kivételt, amely számlaszám üzenetet fogja kiírni a megadott számlaszámmal
		/// </summary>
		/// <param name="szamlaszam">A számlaszám, ami nem létezik</param>
		public HibasSzamlaszamException(string szamlaszam)
		: base("Hibas szamlaszam: " + szamlaszam)
		{
		}
	}
}
