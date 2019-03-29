namespace NUnit.Samples.Money 
{
	using System;
	using NUnit.Framework;
	/// <summary>
	/// Tests Money
	/// </summary>
	/// 
	[TestFixture]
	public class MonedaTests 
	{
		private Moneda _moneda12CHF;
		private Moneda _moneda14CHF;
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
			_moneda12CHF= new Moneda(12, Divisa.CHF);
			_moneda14CHF= new Moneda(14, Divisa.CHF);
			_moneda7USD= new Moneda( 7, Divisa.USD);
			_moneda21USD= new Moneda(21, Divisa.USD);

			_monedero1= new Monedero(_moneda12CHF, _moneda7USD);
			_monedero2= new Monedero(_moneda14CHF, _moneda21USD);
		}

		/// <summary>
		/// Assert that Monederos multiply correctly
		/// </summary>
		/// 
		[Test]
		public void BagMultiply() 
		{
			// {[12 CHF][7 USD]} *2 == {[24 CHF][14 USD]}
			Moneda[] bag = { new Moneda(24, Divisa.CHF), new Moneda(14, Divisa.USD) };
			Monedero expected= new Monedero(bag);
			Assert.AreEqual(expected, _monedero1.Multiplicar(2));
			Assert.AreEqual(_monedero1, _monedero1.Multiplicar(1));
			Assert.IsTrue(_monedero1.Multiplicar(0).EnCeros);
		}

		/// <summary>
		/// Assert that Monederos negate(positive to negative values) correctly
		/// </summary>
		/// 
		[Test]
		public void BagNegate() 
		{
			// {[12 CHF][7 USD]} negate == {[-12 CHF][-7 USD]}
			Moneda[] bag= { new Moneda(-12, Divisa.CHF), new Moneda(-7, Divisa.USD) };
			Monedero expected= new Monedero(bag);
			Assert.AreEqual(expected, _monedero1.Negar());
		}

		/// <summary>
		/// Assert that adding currency to Monederos happens correctly
		/// </summary>
		/// 
		[Test]
		public void BagSimpleAdd() 
		{
			// {[12 CHF][7 USD]} + [14 CHF] == {[26 CHF][7 USD]}
			Moneda[] bag= { new Moneda(26, Divisa.CHF), new Moneda(7, Divisa.USD) };
			Monedero expected= new Monedero(bag);
			Assert.AreEqual(expected, _monedero1.Agregar(_moneda14CHF));
		}

		/// <summary>
		/// Assert that subtracting currency to Monederos happens correctly
		/// </summary>
		/// 
		[Test]
		public void BagSubtract() 
		{
			// {[12 CHF][7 USD]} - {[14 CHF][21 USD] == {[-2 CHF][-14 USD]}
			Moneda[] bag= { new Moneda(-2, Divisa.CHF), new Moneda(-14, Divisa.USD) };
			Monedero expected= new Monedero(bag);
			Assert.AreEqual(expected, _monedero1.Restar(_monedero2));
		}

		/// <summary>
		/// Assert that adding multiple currencies to Monederos in one statement happens correctly
		/// </summary>
		/// 
		[Test]
		public void BagSumAdd() 
		{
			// {[12 CHF][7 USD]} + {[14 CHF][21 USD]} == {[26 CHF][28 USD]}
			Moneda[] bag= { new Moneda(26, Divisa.CHF), new Moneda(28, Divisa.USD) };
			Monedero expected= new Monedero(bag);
			Assert.AreEqual(expected, _monedero1.Agregar(_monedero2));
		}

		/// <summary>
		/// Assert that Monederos hold zero value after adding zero value
		/// </summary>
		/// 
		[Test]
		public void IsZero() 
		{
			Assert.IsTrue(_monedero1.Restar(_monedero1).EnCeros);

			Moneda[] bag = { new Moneda(0, Divisa.CHF), new Moneda(0, Divisa.USD) };
			Assert.IsTrue(new Monedero(bag).EnCeros);
		}

		/// <summary>
		/// Assert that a new bag is the same as adding value to an existing bag
		/// </summary>
		/// 
		[Test]
		public void MixedSimpleAdd() 
		{
			// [12 CHF] + [7 USD] == {[12 CHF][7 USD]}
			Moneda[] bag= { _moneda12CHF, _moneda7USD };
			Monedero expected= new Monedero(bag);
			Assert.AreEqual(expected, _moneda12CHF.Agregar(_moneda7USD));
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
			Monedero equal= new Monedero(new Moneda(12, Divisa.CHF), new Moneda(7, Divisa.USD));
			Assert.IsTrue(_monedero1.Equals(equal));
			Assert.IsTrue(!_monedero1.Equals(_moneda12CHF));
			Assert.IsTrue(!_moneda12CHF.Equals(_monedero1));
			Assert.IsTrue(!_monedero1.Equals(_monedero2));
		}

		/// <summary>
		/// Assert that the hash of a new bag is the same as 
		/// the hash of an existing bag with added value
		/// </summary>
		/// 
		[Test]
		public void MonederoHash() 
		{
			Monedero equal= new Monedero(new Moneda(12, Divisa.CHF), new Moneda(7, Divisa.USD));
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
			Assert.IsFalse(_moneda12CHF.Equals(null)); 
			Moneda equalMoney= new Moneda(12, Divisa.CHF);
			Assert.IsTrue(_moneda12CHF.Equals( _moneda12CHF ));
			Assert.IsTrue(_moneda12CHF.Equals( equalMoney ));
			Assert.IsFalse(_moneda12CHF.Equals(_moneda14CHF));
		}

		/// <summary>
		/// Assert that the hash of new Money is the same as 
		/// the hash of initialized Money
		/// </summary>
		/// 
		[Test]
		public void MoneyHash() 
		{
			Assert.IsFalse(_moneda12CHF.Equals(null)); 
			Moneda equal= new Moneda(12, Divisa.CHF);
			Assert.AreEqual(_moneda12CHF.GetHashCode(), equal.GetHashCode());
		}

		/// <summary>
		/// Assert that adding multiple small values is the same as adding one big value
		/// </summary>
		/// 
		[Test]
		public void Normalize() 
		{
			Moneda[] bag= { new Moneda(26, Divisa.CHF), new Moneda(28, Divisa.CHF), new Moneda(6, Divisa.CHF) };
			Monedero Monedero= new Monedero(bag);
			Moneda[] expected = { new Moneda(60, Divisa.CHF) };
			// note: expected is still a Monedero
			Monedero expectedBag= new Monedero(expected);
			Assert.AreEqual(expectedBag, Monedero);
		}

		/// <summary>
		/// Assert that removing a value is the same as not having such a value
		/// </summary>
		/// 
		[Test]
		public void Normalize2() 
		{
			// {[12 CHF][7 USD]} - [12 CHF] == [7 USD]
			Moneda expected= new Moneda(7, Divisa.USD);
			Assert.AreEqual(expected, _monedero1.Restar(_moneda12CHF));
		}

		/// <summary>
		/// Assert that removing multiple values works correctly
		/// </summary>
		/// 
		[Test]
		public void Normalize3() 
		{
			// {[12 CHF][7 USD]} - {[12 CHF][3 USD]} == [4 USD]
			Moneda[] s1 = { new Moneda(12, Divisa.CHF), new Moneda(3, Divisa.USD) };
			Monedero ms1= new Monedero(s1);
			Moneda expected= new Moneda(4, Divisa.USD);
			Assert.AreEqual(expected, _monedero1.Restar(ms1));
		}

		/// <summary>
		/// Assert that if value is subtracted from 0, the result will be negative.
		/// </summary>
		/// 
		[Test]
		public void Normalize4() 
		{
			// [12 CHF] - {[12 CHF][3 USD]} == [-3 USD]
			Moneda[] s1 = { new Moneda(12, Divisa.CHF), new Moneda(3, Divisa.USD) };
			Monedero ms1= new Monedero(s1);
			Moneda expected= new Moneda(-3, Divisa.USD);
			Assert.AreEqual(expected, _moneda12CHF.Restar(ms1));
		}

		/// <summary>
		/// Assert that Money.ToString() function works correctly
		/// </summary>
		/// 
		[Test]
		public void Print() 
		{
			Assert.AreEqual("[12 CHF]", _moneda12CHF.ToString());
		}

		/// <summary>
		/// Assert that adding more value to Money happens correctly
		/// </summary>
		/// 
		[Test]
		public void SimpleAdd() 
		{
			// [12 CHF] + [14 CHF] == [26 CHF]
			Moneda expected= new Moneda(26, Divisa.CHF);
			Assert.AreEqual(expected, _moneda12CHF.Agregar(_moneda14CHF));
		}

		/// <summary>
		/// Assert that adding multiple currencies to Monederos happens correctly
		/// </summary>
		/// 
		[Test]
		public void SimpleBagAdd() 
		{
			// [14 CHF] + {[12 CHF][7 USD]} == {[26 CHF][7 USD]}
			Moneda[] bag= { new Moneda(26, Divisa.CHF), new Moneda(7, Divisa.USD) };
			Monedero expected= new Monedero(bag);
			Assert.AreEqual(expected, _moneda14CHF.Agregar(_monedero1));
		}

		/// <summary>
		/// Assert that multiplying currency in Money happens correctly
		/// </summary>
		/// 
		[Test]
		public void SimpleMultiply() 
		{
			// [14 CHF] *2 == [28 CHF]
			Moneda expected= new Moneda(28, Divisa.CHF);
			Assert.AreEqual(expected, _moneda14CHF.Multiplicar(2));
		}

		/// <summary>
		/// Assert that negating(positive to negative values) currency in Money happens correctly
		/// </summary>
		/// 
		[Test]
		public void SimpleNegate() 
		{
			// [14 CHF] negate == [-14 CHF]
			Moneda expected= new Moneda(-14, Divisa.CHF);
			Assert.AreEqual(expected, _moneda14CHF.Negar());
		}

		/// <summary>
		/// Assert that removing currency from Money happens correctly
		/// </summary>
		/// 
		[Test]
		public void SimpleSubtract() 
		{
			// [14 CHF] - [12 CHF] == [2 CHF]
			Moneda expected= new Moneda(2, Divisa.CHF);
			Assert.AreEqual(expected, _moneda14CHF.Restar(_moneda12CHF));
		}
	}
}
