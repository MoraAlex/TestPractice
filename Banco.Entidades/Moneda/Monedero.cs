namespace Banco.Entidades
{

	using System;
	using System.Collections;
	using System.Text;

	/// <summary>A MoneyBag defers exchange rate conversions.</summary>
	/// <remarks>For example adding 
	/// 12 Swiss Francs to 14 US Dollars is represented as a bag 
	/// containing the two Monies 12 CHF and 14 USD. Adding another
	/// 10 Swiss francs gives a bag with 22 CHF and 14 USD. Due to 
	/// the deferred exchange rate conversion we can later value a 
	/// MoneyBag with different exchange rates.
	///
	/// A MoneyBag is represented as a list of Monies and provides 
	/// different constructors to create a MoneyBag.</remarks>
	public class Monedero: IMoneda 
	{
		private ArrayList fMonies= new ArrayList(5);

		private Monedero() 
		{
		}
		public Monedero(Moneda[] bag) 
		{
			for (int i= 0; i < bag.Length; i++) 
			{
				if (!bag[i].EnCeros)
					AppendMoney(bag[i]);
			}
		}
		public Monedero(Moneda m1, Moneda m2) 
		{
			AppendMoney(m1);
			AppendMoney(m2);
		}
		public Monedero(Moneda m, Monedero bag) 
		{
			AppendMoney(m);
			AppendBag(bag);
		}
		public Monedero(Monedero m1, Monedero m2) 
		{
			AppendBag(m1);
			AppendBag(m2);
		}
		public IMoneda Agregar(IMoneda m) 
		{
			return m.AgregarMonedero(this);
		}
		public IMoneda AgregarMoneda(Moneda m) 
		{
			return (new Monedero(m, this)).Simplify();
		}
		public IMoneda AgregarMonedero(Monedero s) 
		{
			return (new Monedero(s, this)).Simplify();
		}
		private void AppendBag(Monedero aBag) 
		{
			foreach (Moneda m in aBag.fMonies)
				AppendMoney(m);
		}
		private void AppendMoney(Moneda aMoney) 
		{
			IMoneda old= FindMoney(aMoney.Divisa);
			if (old == null) 
			{
				fMonies.Add(aMoney);
				return;
			}
			fMonies.Remove(old);
			IMoneda sum= old.Agregar(aMoney);
			if (sum.EnCeros) 
				return;
			fMonies.Add(sum);
		}
		private bool Contains(Moneda aMoney) 
		{
			Moneda m= FindMoney(aMoney.Divisa);
			return m.Cantidad == aMoney.Cantidad;
		}
		public override bool Equals(Object anObject) 
		{
			if (EnCeros)
				if (anObject is IMoneda)
					return ((IMoneda)anObject).EnCeros;
            
			if (anObject is Monedero) 
			{
				Monedero aMoneyBag= (Monedero)anObject;
				if (aMoneyBag.fMonies.Count != fMonies.Count)
					return false;
                
				foreach (Moneda m in fMonies) 
				{
					if (!aMoneyBag.Contains(m))
						return false;
				}
				return true;
			}
			return false;
		}
		private Moneda FindMoney(Divisa currency) 
		{
			foreach (Moneda m in fMonies) 
			{
				if (m.Divisa.Equals(currency))
					return m;
			}
			return null;
		}
		public override int GetHashCode() 
		{
			int hash= 0;
			foreach (Moneda m in fMonies) 
			{
				hash^= m.GetHashCode();
			}
			return hash;
		}
		public bool EnCeros 
		{
			get { return fMonies.Count == 0; }
		}
		public IMoneda Multiplicar(int factor) 
		{
			Monedero result= new Monedero();
			if (factor != 0) 
			{
				foreach (Moneda m in fMonies) 
				{
					result.AppendMoney((Moneda)m.Multiplicar(factor));
				}
			}
			return result;
		}
		public IMoneda Negar() 
		{
			Monedero result= new Monedero();
			foreach (Moneda m in fMonies) 
			{
				result.AppendMoney((Moneda)m.Negar());
			}
			return result;
		}
		private IMoneda Simplify() 
		{
			if (fMonies.Count == 1)
				return (IMoneda)fMonies[0];
			return this;
		}
		public IMoneda Restar(IMoneda m) 
		{
			return Agregar(m.Negar());
		}
		public override String ToString() 
		{
			StringBuilder buffer = new StringBuilder();
			buffer.Append("{");
			foreach (Moneda m in fMonies)
				buffer.Append(m);
			buffer.Append("}");
			return buffer.ToString();
		}
	}
}
