using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBankProject
{
	internal class BankTests
	{
		Bank bank;

		[SetUp] 
		public void SetUp() 
		{
			bank = new Bank();
		}

		[Test]
		public void UjSzamla_UresNevvel()
		{
			Assert.Throws<ArgumentException>(() => bank.UjSzamla("", "1234"));
		}

		[Test]
		public void UjSzamla_NullNevvel()
		{
			Assert.Throws<ArgumentNullException>(() => bank.UjSzamla(null, "1234"));
		}

		[Test]
		public void UjSzamla_UresSzammal()
		{
			Assert.Throws<ArgumentException>(() => bank.UjSzamla("Gipsz", ""));
		}

		[Test]
		public void UjSzamla_NullSzammal()
		{
			Assert.Throws<ArgumentNullException>(() => bank.UjSzamla("Gipsz", null));
		}

		[Test]
		public void UjSzamla_SikeresenLetrejon()
		{
			Assert.DoesNotThrow(() => bank.UjSzamla("Gipsz", "1234"));
		}

		[Test]
		public void UjSzamla_EgyenlegNulla()
		{
			bank.UjSzamla("Gipsz", "1234");
			Assert.That(bank.Egyenleg("1234"), Is.EqualTo(0));
		}

		[Test]
		public void EgyenlegFeltolt_EgyenlegValtozik()
		{
			bank.UjSzamla("Gipsz", "1234");
			bank.EgyenlegFeltolt("1234", 10000);
			Assert.That(bank.Egyenleg("1234"), Is.EqualTo(10000));
		}

		[Test]
		public void EgyenlegFeltolt_NullaOsszeg()
		{
			bank.UjSzamla("Gipsz", "1234");
			Assert.Throws<ArgumentException>(() => bank.EgyenlegFeltolt("1234", 0));
		}

		[Test]
		public void Utal_RosszHonnan()
		{
			bank.UjSzamla("Gipsz", "1234");
			bank.UjSzamla("Jack", "4321");
			Assert.Throws<ArgumentException>(() => bank.Utal("", "4321", 10000));
		}

		[Test]
		public void Utal_RosszHova()
		{
			bank.UjSzamla("Gipsz", "1234");
			bank.UjSzamla("Jack", "4321");
			Assert.Throws<ArgumentException>(() => bank.Utal("1234", "", 10000));
		}

		[Test]
		public void Utal_NullaOsszeg()
		{
			bank.UjSzamla("Gipsz", "1234");
			bank.UjSzamla("Jack", "4321");
			Assert.Throws<ArgumentException>(() => bank.Utal("", "4321", 0));
		}

		[Test]
		public void Utal_Utalas()
		{
			bank.UjSzamla("Gipsz", "1234");
			bank.UjSzamla("Jack", "4321");
			Assert.IsTrue(bank.Utal("1234", "4321", 10000));
		}
	}
}
