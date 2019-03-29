namespace Banco.Entidades
{

	using System;
	using System.Text;

    /// <summary>A simple Money.</summary>
    public class Moneda: IMoneda 
	{

		private decimal _amount;
		private Divisa _divisa;
        
		/// <summary>Constructs a money from the given amount and
		/// currency.</summary>
		public Moneda(decimal amount, Divisa currency) 
		{
			_amount= amount;
			_divisa= currency;
		}

		/// <summary>Adds a money to this money. Forwards the request to
		/// the AddMoney helper.</summary>
		public IMoneda Agregar(IMoneda m) 
		{
			return m.AgregarMoneda(this);
		}

		public IMoneda AgregarMoneda(Moneda m) 
		{
			if (m.Divisa.Equals(Divisa) )
				return new Moneda(Cantidad+m.Cantidad, Divisa);
			return new Monedero(this, m);
		}

		public IMoneda AgregarMonedero(Monedero s) 
		{
			return s.AgregarMoneda(this);
		}

		public decimal Cantidad 
		{
			get { return _amount; }
		}

		public Divisa Divisa 
		{
			get { return _divisa; }
		}

		public override bool Equals(Object anObject) 
		{
			if (EnCeros)
				if (anObject is IMoneda)
					return ((IMoneda)anObject).EnCeros;
			if (anObject is Moneda) 
			{
				Moneda aMoney= (Moneda)anObject;
				return aMoney.Divisa.Equals(Divisa)
					&& Cantidad == aMoney.Cantidad;
			}
			return false;
		}

		public override int GetHashCode() 
		{
			return _divisa.GetHashCode()+(int)_amount;
		}

		public bool EnCeros 
		{
			get { return Cantidad == 0; }
		}

		public IMoneda Multiplicar(int factor) 
		{
			return new Moneda(Cantidad*factor, Divisa);
		}

		public IMoneda Negar() 
		{
			return new Moneda(-Cantidad, Divisa);
		}

		public IMoneda Restar(IMoneda m) 
		{
			return Agregar(m.Negar());
		}

		public override String ToString() 
		{
			StringBuilder buffer = new StringBuilder();
			buffer.Append("["+Cantidad+" "+Divisa+"]");
			return buffer.ToString();
		}
	}
}
