/*namespace TestBankProject
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
			//Minden teszteset elõtt lefut
			//Alapállapot beállítására használjuk
			//Neve nem számít, csak a [SetUp] annotációval kell ellátni
		}

		[Test]
		public void Test1()
		{
			//Sikeres teszt
			Assert.Pass();
		}

		[Test]
		public void Test2()
		{
			//Sikertelen teszt
			Assert.Fail();
		}

		[Test]
		public void Test3()
		{
			//Nincs assert -> sikeresen fut le	
		}

		public void Test4()
		{
			//Nincs ellátva [Test] annotációval -> nem teszt
			Assert.Pass();
		}
	}
}*/