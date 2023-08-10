using System;
using System.Web.UI.WebControls;

namespace DeKale
{
	/// <summary>
	/// Displays a TextBox that only allows Positive values (and 0) through javascript.
	/// WARNING: A <see cref="UnsignedIntegerTextBoxValidator"/> should always be enclosed 
	/// when using this control.
	/// </summary>
	/// <remarks>
	/// <para>This TextBox contains a onkeypress event that catches illegal characters, based
	/// on the CurrentCulture of the user.</para>
	/// <para>This control should always be used together with a 
	/// <see cref="UnsignedIntegerTextBoxValidator"/>, because users could have
	/// JavaScript turned of. This is the code for the validator:
	/// <code>
	///		UnsignedIntegerTextBox ntb = new UnsignedIntegerTextBox();
	///		ntb.ID = "UnsignedIntegerTextBox1";
	///		UnsignedIntegerTextBoxValidator cv = new UnsignedIntegerTextBoxValidator();
	///		cv.ControlToValidate = ntb.ID;
	/// </code>
	/// </para>
	/// </remarks>
	public class UnsignedIntegerTextBox : CharacterFilteringTextBox
	{
		/// <summary>Overridden.</summary>
		protected override bool AllowNumericKeys { get { return true; } }

		/// <summary>Overridden.</summary>
		protected override string TextAlign { get { return "right"; } }
	}

	/// <summary>
	/// This control is a <see cref="CompareValidator"/>, that can be used together
	/// with the <see cref="SignedIntegerTextBox"/>. It sets the <see cref="Operator"/>
	/// property to GreaterThanEqual, the <see cref="Type"/> property to Integer and
	/// the <see cref="ValueToCompare"/> property to "0".
	/// </summary>
	public class UnsignedIntegerTextBoxValidator : CompareValidator
	{
		public UnsignedIntegerTextBoxValidator() : base()
		{
			this.Operator = ValidationCompareOperator.GreaterThanEqual;
			this.Type = ValidationDataType.Integer;
			this.ValueToCompare = "0";
		}
	}
}
