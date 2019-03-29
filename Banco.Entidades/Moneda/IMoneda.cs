namespace Banco.Entidades
{

	/// <summary>The common interface for simple Monies and MoneyBags.</summary>
	public interface IMoneda 
	{
		/// <summary>Adds a money to this money.</summary>
		IMoneda Agregar(IMoneda m);

		/// <summary>Adds a simple Money to this money. This is a helper method for
		/// implementing double dispatch.</summary>
		IMoneda AgregarMoneda(Moneda m);

		/// <summary>Adds a MoneyBag to this money. This is a helper method for
		/// implementing double dispatch.</summary>
		IMoneda AgregarMonedero(Monedero s);

		/// <value>True if this money is zero.</value>
		bool EnCeros { get; }

		/// <summary>Multiplies a money by the given factor.</summary>
		IMoneda Multiplicar(int factor);

		/// <summary>Negates this money.</summary>
		IMoneda Negar();

		/// <summary>Subtracts a money from this money.</summary>
		IMoneda Restar(IMoneda m);
	}
}
