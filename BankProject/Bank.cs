namespace BankProject
{
	public class Bank
	{
		private List<Szamla> szamlak = new List<Szamla>();
		/// <summary>
		/// Egy létező számlára pénzt helyez
		/// </summary>
		/// <param name="szamlaszam">A számlaszám amire az egyenleget feltöltjük</param>
		/// <param name="osszeg">A feltöltendő egyenleg</param>
		/// <exception cref="NotImplementedException"></exception>
		public void EgyenlegFeltolt(string szamlaszam, ulong osszeg)
		{
			if(osszeg == 0)
			{
				throw new ArgumentException();
			}
			Szamla szamla = szamlak.FirstOrDefault(szamla => szamla.Szamlaszam == szamlaszam);
			szamla.Egyenleg += osszeg;
		}

		/// <summary>
		/// Új számlát nyit a megadott névvel, számlaszámmal
		/// </summary>
		/// <param name="nev">Az új számla tulajdonosának a neve</param>
		/// <param name="szamlaszam">Az új számla számlaszáma</param>
		/// <exception cref="NotImplementedException"></exception>
		public void UjSzamla(string nev, string szamlaszam)
		{
			if (nev == null)
			{
				throw new ArgumentNullException(nameof(nev));
			}
			if(nev.Trim() == "")
			{
				throw new ArgumentException("A név nem lehet üres",nameof(nev));
			}
			try 
			{
				SzamlaKeres(szamlaszam);
				throw new ArgumentException("A megadott számlaszámmal már létezik számla", nameof(szamlaszam));
			} 
			catch (HibasSzamlaszamException) 
			{
				szamlak.Add(new Szamla(nev, szamlaszam));
			}			
		}

		// Két számla között utal.
		// Ha nincs elég pénz a forrás számlán, akkor...
		public bool Utal(string honnan, string hova, ulong osszeg)
		{
			bool valid = false;
			try
			{
				SzamlaKeres(honnan);
			}
			catch (HibasSzamlaszamException)
			{
				throw new HibasSzamlaszamException("Nincs ilyen számlaszám");
			}
			try
			{
				SzamlaKeres(hova);
			}
			catch (HibasSzamlaszamException)
			{
				throw new HibasSzamlaszamException("Nincs ilyen számlaszám");
			}
			if(osszeg == 0 || osszeg < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(osszeg),"Az összeg nem lehet nulla vagy annál kisebb");
			}

			szamlak.ForEach(honnanSz =>
			{
				if(honnanSz.Szamlaszam == honnan)
				{
					szamlak.ForEach((hovaSz) => 
					{
						if(hovaSz.Szamlaszam == hova)
						{
							honnanSz.Egyenleg -= osszeg;
							hovaSz.Egyenleg += osszeg;
							valid = true;
						}
					});
				}
			});
			return valid;
		}

		/// <summary>
		/// Lekérdezi az adott számlán lévő pénzösszeget
		/// </summary>
		/// <param name="szamlaszam">A lekérdezett számla számlaszáma</param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public ulong Egyenleg(string szamlaszam)
		{
			Szamla szamla = szamlak.FirstOrDefault(szamla => szamla.Szamlaszam == szamlaszam);
			return szamla.Egyenleg;
		}

		private class Szamla
		{
			public Szamla(string tulajdonos, string szamlaszam)
			{
				Tulajdonos = tulajdonos;
				Szamlaszam = szamlaszam;
				Egyenleg = 0;
			}

			public string Tulajdonos { get; set; }
			public string Szamlaszam { get; set; }
			public ulong Egyenleg { get; set; }
		}

		private Szamla SzamlaKeres(string szamlaszam)
		{
			if (szamlaszam == null)
			{
				throw new ArgumentNullException(nameof(szamlaszam));
			}
			if (szamlaszam.Trim() == "")
			{
				throw new ArgumentException("A név nem lehet üres", nameof(szamlaszam));
			}
			int ind = 0;
			while(ind < szamlak.Count && szamlak[ind].Szamlaszam != szamlaszam)
			{
				ind++;
			}
			if(ind == szamlak.Count)
			{
				throw new HibasSzamlaszamException(szamlaszam);
			}
			return szamlak[ind];
		}
	}
}