using Banco.Entidades;
using System;
using NUnit.Framework;

namespace Banco.UnitTests
{	
	/// <summary>
	/// Tests Money
	/// </summary>
	/// 
	[TestFixture]
	public class MonedaTests 
	{
		private Moneda _moneda12MXN;
		private Moneda _moneda14MXN;
		private Moneda _moneda7USD;
		private Moneda _moneda21USD;
        
		private Monedero _monedero1;
		private Monedero _monedero2;

		/// <summary>
		/// Initializes Money test objects
		/// </summary>
		/// 
		[SetUp]
		protected void SetUp() 
		{
			_moneda12MXN= new Moneda(12, Divisa.MXN);
			_moneda14MXN= new Moneda(14, Divisa.MXN);
			_moneda7USD= new Moneda( 7, Divisa.USD);
			_moneda21USD= new Moneda(21, Divisa.USD);

			_monedero1= new Monedero(_moneda12MXN, _moneda7USD);
			_monedero2= new Monedero(_moneda14MXN, _moneda21USD);
		}

		/// <summary>
		/// Assert that Monederos multiply correctly
		/// </summary>
		/// 
		[Test]
		public void MonederoMultiply() 
		{
			// {[12 MXN][7 USD]} *2 == {[24 MXN][14 USD]}
			Moneda[] monedero = { new Moneda(24, Divisa.MXN), new Moneda(14, Divisa.USD) };
			Monedero expectativa= new Monedero(monedero);
			Assert.AreEqual(expectativa, _monedero1.Multiplicar(2));
			Assert.AreEqual(_monedero1, _monedero1.Multiplicar(1));
			Assert.IsTrue(_monedero1.Multiplicar(0).EnCeros);
		}

		/// <summary>
		/// Assert that Monederos negate(positive to negative values) correctly
		/// </summary>
		/// 
		[Test]
		public void MonederoNegate() 
		{
			// {[12 MXN][7 USD]} negate == {[-12 MXN][-7 USD]}
			Moneda[] monedero= { new Moneda(-12, Divisa.MXN), new Moneda(-7, Divisa.USD) };
			Monedero expectativa= new Monedero(monedero);
			Assert.AreEqual(expectativa, _monedero1.Negar());
		}

		/// <summary>
		/// Assert that adding currency to Monederos happens correctly
		/// </summary>
		/// 
		[Test]
		public void MonederoSimpleAgregar() 
		{
			// {[12 MXN][7 USD]} + [14 MXN] == {[26 MXN][7 USD]}
			Moneda[] monedero= { new Moneda(26, Divisa.MXN), new Moneda(7, Divisa.USD) };
			Monedero expectativa= new Monedero(monedero);
			Assert.AreEqual(expectativa, _monedero1.Agregar(_moneda14MXN));
		}

		/// <summary>
		/// Assert that subtracting currency to Monederos happens correctly
		/// </summary>
		/// 
		[Test]
		public void MonederoRestar() 
		{
			// {[12 MXN][7 USD]} - {[14 MXN][21 USD] == {[-2 MXN][-14 USD]}
			Moneda[] monedero= { new Moneda(-2, Divisa.MXN), new Moneda(-14, Divisa.USD) };
			Monedero expectativa= new Monedero(monedero);
			Assert.AreEqual(expectativa, _monedero1.Restar(_monedero2));
		}

		/// <summary>
		/// Assert that adding multiple currencies to Monederos in one statement happens correctly
		/// </summary>
		/// 
		[Test]
		public void MonederoSumAgregar() 
		{
			// {[12 MXN][7 USD]} + {[14 MXN][21 USD]} == {[26 MXN][28 USD]}
			Moneda[] monedero= { new Moneda(26, Divisa.MXN), new Moneda(28, Divisa.USD) };
			Monedero expectativa= new Monedero(monedero);
			Assert.AreEqual(expectativa, _monedero1.Agregar(_monedero2));
		}

		/// <summary>
		/// Assert that Monederos hold zero value after adding zero value
		/// </summary>
		/// 
		[Test]
		public void IsZero() 
		{
			Assert.IsTrue(_monedero1.Restar(_monedero1).EnCeros);

			Moneda[] monedero = { new Moneda(0, Divisa.MXN), new Moneda(0, Divisa.USD) };
			Assert.IsTrue(new Monedero(monedero).EnCeros);
		}

		/// <summary>
		/// Assert that a new monedero is the same as adding value to an existing monedero
		/// </summary>
		/// 
		[Test]
		public void MixedSimpleAgregar() 
		{
			// [12 MXN] + [7 USD] == {[12 MXN][7 USD]}
			Moneda[] monedero= { _moneda12MXN, _moneda7USD };
			Monedero expectativa= new Monedero(monedero);
			Assert.AreEqual(expectativa, _moneda12MXN.Agregar(_moneda7USD));
		}

		/// <summary>
		/// Assert that Monedero.Equals() works correctly
		/// </summary>
		/// 
		[Test]
		public void MonederoEquals() 
		{
			//NOTE: Normally we use Assert.AreEqual to test whether two
			// objects are equal. But here we are testing the Monedero.Equals()
			// method itself, so using AreEqual would not serve the purpose.
			Assert.IsFalse(_monedero1.Equals(null)); 

			Assert.IsTrue(_monedero1.Equals( _monedero1 ));
			Monedero equal= new Monedero(new Moneda(12, Divisa.MXN), new Moneda(7, Divisa.USD));
			Assert.IsTrue(_monedero1.Equals(equal));
			Assert.IsTrue(!_monedero1.Equals(_moneda12MXN));
			Assert.IsTrue(!_moneda12MXN.Equals(_monedero1));
			Assert.IsTrue(!_monedero1.Equals(_monedero2));
		}

		/// <summary>
		/// Assert that the hash of a new monedero is the same as 
		/// the hash of an existing monedero with added value
		/// </summary>
		/// 
		[Test]
		public void MonederoHash() 
		{
			Monedero equal= new Monedero(new Moneda(12, Divisa.MXN), new Moneda(7, Divisa.USD));
			Assert.AreEqual(_monedero1.GetHashCode(), equal.GetHashCode());
		}

		/// <summary>
		/// Assert that Money.Equals() works correctly
		/// </summary>
		/// 
		[Test]
		public void MoneyEquals() 
		{
			//NOTE: Normally we use Assert.AreEqual to test whether two
			// objects are equal. But here we are testing the Monedero.Equals()
			// method itself, so using AreEqual would not serve the purpose.
			Assert.IsFalse(_moneda12MXN.Equals(null)); 
			Moneda equalMoney= new Moneda(12, Divisa.MXN);
			Assert.IsTrue(_moneda12MXN.Equals( _moneda12MXN ));
			Assert.IsTrue(_moneda12MXN.Equals( equalMoney ));
			Assert.IsFalse(_moneda12MXN.Equals(_moneda14MXN));
		}

		/// <summary>
		/// Assert that the hash of new Money is the same as 
		/// the hash of initialized Money
		/// </summary>
		/// 
		[Test]
		public void MoneyHash() 
		{
			Assert.IsFalse(_moneda12MXN.Equals(null)); 
			Moneda equal= new Moneda(12, Divisa.MXN);
			Assert.AreEqual(_moneda12MXN.GetHashCode(), equal.GetHashCode());
		}

		/// <summary>
		/// Assert that adding multiple small values is the same as adding one big value
		/// </summary>
		/// 
		[Test]
		public void Normalize() 
		{
			Moneda[] monedero= { new Moneda(26, Divisa.MXN), new Moneda(28, Divisa.MXN), new Moneda(6, Divisa.MXN) };
			Monedero Monedero= new Monedero(monedero);
			Moneda[] expectativa = { new Moneda(60, Divisa.MXN) };
			// note: expectativa is still a Monedero
			Monedero expectativaMonedero= new Monedero(expectativa);
			Assert.AreEqual(expectativaMonedero, Monedero);
		}

		/// <summary>
		/// Assert that removing a value is the same as not having such a value
		/// </summary>
		/// 
		[Test]
		public void Normalize2() 
		{
			// {[12 MXN][7 USD]} - [12 MXN] == [7 USD]
			Moneda expectativa= new Moneda(7, Divisa.USD);
			Assert.AreEqual(expectativa, _monedero1.Restar(_moneda12MXN));
		}

		/// <summary>
		/// Assert that removing multiple values works correctly
		/// </summary>
		/// 
		[Test]
		public void Normalize3() 
		{
			// {[12 MXN][7 USD]} - {[12 MXN][3 USD]} == [4 USD]
			Moneda[] s1 = { new Moneda(12, Divisa.MXN), new Moneda(3, Divisa.USD) };
			Monedero ms1= new Monedero(s1);
			Moneda expectativa= new Moneda(4, Divisa.USD);
			Assert.AreEqual(expectativa, _monedero1.Restar(ms1));
		}

		/// <summary>
		/// Assert that if value is subtracted from 0, the result will be negative.
		/// </summary>
		/// 
		[Test]
		public void Normalize4() 
		{
			// [12 MXN] - {[12 MXN][3 USD]} == [-3 USD]
			Moneda[] s1 = { new Moneda(12, Divisa.MXN), new Moneda(3, Divisa.USD) };
			Monedero ms1= new Monedero(s1);
			Moneda expectativa= new Moneda(-3, Divisa.USD);
			Assert.AreEqual(expectativa, _moneda12MXN.Restar(ms1));
		}

		/// <summary>
		/// Assert that Money.ToString() function works correctly
		/// </summary>
		/// 
		[Test]
		public void Print() 
		{
			Assert.AreEqual("[12 MXN]", _moneda12MXN.ToString());
		}

		/// <summary>
		/// Assert that adding more value to Money happens correctly
		/// </summary>
		/// 
		[Test]
		public void SimpleAgregar() 
		{
			// [12 MXN] + [14 MXN] == [26 MXN]
			Moneda expectativa= new Moneda(26, Divisa.MXN);
			Assert.AreEqual(expectativa, _moneda12MXN.Agregar(_moneda14MXN));
		}

		/// <summary>
		/// Assert that adding multiple currencies to Monederos happens correctly
		/// </summary>
		/// 
		[Test]
		public void SimpleMonederoAgregar() 
		{
			// [14 MXN] + {[12 MXN][7 USD]} == {[26 MXN][7 USD]}
			Moneda[] monedero= { new Moneda(26, Divisa.MXN), new Moneda(7, Divisa.USD) };
			Monedero expectativa= new Monedero(monedero);
			Assert.AreEqual(expectativa, _moneda14MXN.Agregar(_monedero1));
		}

		/// <summary>
		/// Assert that multiplying currency in Money happens correctly
		/// </summary>
		/// 
		[Test]
		public void SimpleMultiply() 
		{
			// [14 MXN] *2 == [28 MXN]
			Moneda expectativa= new Moneda(28, Divisa.MXN);
			Assert.AreEqual(expectativa, _moneda14MXN.Multiplicar(2));
		}

		/// <summary>
		/// Assert that negating(positive to negative values) currency in Money happens correctly
		/// </summary>
		/// 
		[Test]
		public void SimpleNegate() 
		{
			// [14 MXN] negate == [-14 MXN]
			Moneda expectativa= new Moneda(-14, Divisa.MXN);
			Assert.AreEqual(expectativa, _moneda14MXN.Negar());
		}

		/// <summary>
		/// Assert that removing currency from Money happens correctly
		/// </summary>
		/// 
		[Test]
		public void SimpleRestar() 
		{
			// [14 MXN] - [12 MXN] == [2 MXN]
			Moneda expectativa= new Moneda(2, Divisa.MXN);
			Assert.AreEqual(expectativa, _moneda14MXN.Restar(_moneda12MXN));
		}
	}
}
