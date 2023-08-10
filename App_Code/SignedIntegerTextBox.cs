using System;
using System.Web.UI.WebControls;

namespace DeKale
{
	/// <summary>
	/// Displays a TextBox that only allows positive and negative integer values through javascript.
	/// WARNING: A <see cref="SignedIntegerTextBoxValidator"/> should always be enclosed when 
	/// using this control.
	/// </summary>
	/// <remarks>
	/// <para>This TextBox contains a onkeypress event that catches illegal characters, based
	/// on the CurrentCulture of the user.</para>
	/// <para>This control should always be used together with a 
	/// <see cref="SignedIntegerTextBoxValidator"/>, because users could have
	/// JavaScript turned of. This is the code for the validator:
	/// <code>
	///		SignedIntegerTextBox ntb = new SignedIntegerTextBox();
	///		ntb.ID = "SignedIntegerTextBox1";
	///		SignedIntegerTextBoxValidator cv = new SignedIntegerTextBoxValidator();
	///		cv.ControlToValidate = ntb.ID;
	/// </code>
	/// </para>
	/// </remarks>
	public class SignedIntegerTextBox : CharacterFilteringTextBox
	{
		/// <summary>Overridden.</summary>
		protected override bool AllowNumericKeys { get { return true; }	}

		/// <summary>Overridden.</summary>
		protected override bool AllowNegativeSignKey { get { return true; } }

		/// <summary>Overridden.</summary>
		protected override string TextAlign { get { return "right"; } }
	}


	/// <summary>
	/// This control is a <see cref="CompareValidator"/>, that can be used together
	/// with the <see cref="SignedIntegerTextBox"/>. It sets the <see cref="Operator"/>
	/// property to DataTypeCheck and the <see cref="Type"/> property to Integer.
	/// </summary>
	public class SignedIntegerTextBoxValidator : CompareValidator
	{
		public SignedIntegerTextBoxValidator() : base()
		{
			this.Operator = ValidationCompareOperator.DataTypeCheck;
			this.Type = ValidationDataType.Integer;
		}
	}
}
