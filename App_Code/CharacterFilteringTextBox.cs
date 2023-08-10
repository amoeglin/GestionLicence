using System;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Text;

namespace DeKale
{
	// TODO: Fix bug in FunctionKeys. Function keys have same CharCode as q...z, so that's not good.

	/// <summary>
	/// Base class for all TextBoxes that have use a filter on the client (javascript)
	/// to enable a subset of the keys.
	/// </summary>
	public abstract class CharacterFilteringTextBox : TextBox
	{
		private static string _altKey       = "if (evt.altKey) return; // Alt Key";
		private static string _ctrlKey      = "if (evt.ctrlKey) return; // Ctrl Key";
		private static string _specialKeys  = "if (charCode<32) return; // Special Keys";
		private static string _arrowKeys    = "if (charCode>=33 && charCode<=40) return; // Arrow Keys";
		private static string _functionKeys = "if (charCode>=112 && charCode<=123) return; // F-Keys";
		private static string _numericKeys  = "if(charCode>=48 && charCode<=57) return; // Numeric Keys";
		private static string _spaceKey     = "if (charCode==32) return;";

		private static string _negativeSignKey
		{
			get { return "if (ch=='" + NumberFormatInfo.CurrentInfo.NegativeSign + "') return;"; }
		}

		private static string _numberDecimalSeparatorKey
		{
			get { return "if (ch=='" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "') return;"; }
		}

		/// <summary>Gets the JavaScript function name for the OnKeyPress event of the TextBox.</summary>
		/// <remarks>
		/// The property returns a string of '"Filter" + TypeName', so every descendant
		/// of the <see cref="CharacterLimitedTextBox"/> class will have it's unique
		/// function name.
		/// </remarks>
		protected string OnKeyPressFunctionName
		{
			get { return "Filter" + this.GetType().Name; }
		}

		/// <summary>
		/// Gets the value indicating if the TextBox allows all Alt + Key combinations.
		/// </summary>
		protected virtual bool AllowAltKey { get { return true; } }

		/// <summary>
		/// Gets the value indicating if the TextBox allows all Ctrl + Key combinations.
		/// </summary>
		protected virtual bool AllowCtrlKey { get { return true; } }

		/// <summary>
		/// Gets the value indicating if the TextBox allows arrow keys (Home, PgUp, End, PgDown,
		/// Left, Right, Up, Down). Descendants may override this function. Default is true.
		/// </summary>
		protected virtual bool AllowArrowKeys { get { return true; } }

		/// <summary>
		/// Gets the value indicating if the TextBox allows function keys (F1..F12).
		/// Descendants may override this function.  Default is true.
		/// </summary>
		protected virtual bool AllowFunctionKeys { get { return false; } }

		/// <summary>
		/// Gets the value indicating if the TextBox allows special keys (Tab, BackSpace,
		/// Return, etc). Descendants may override this function.  Default is true.
		/// </summary>
		protected virtual bool AllowSpecialKeys { get { return true; } }

		/// <summary>
		/// Gets the value indicating if the TextBox allows space key. 
		/// Descendants may override this function. Default is false.
		/// </summary>
		protected virtual bool AllowSpaceKey { get { return false; } }

		/// <summary>
		/// Gets the value indicating if the TextBox allows numeric keys (0...9). 
		/// Descendants may override this function. Default is false.
		/// </summary>
		protected virtual bool AllowNumericKeys { get { return false; } }

		/// <summary>
		/// Gets the value indicating if the TextBox allows the Decimal Separator sign. 
		/// Descendants may override this function. Default is false.
		/// </summary>
		protected virtual bool AllowDecimalSeparatorKey { get { return false; } }

		/// <summary>
		/// Gets the value indicating if the TextBox allows the negative sign. 
		/// Descendants may override this function. Default is false.
		/// </summary>
		protected virtual bool AllowNegativeSignKey { get { return false; } }

		/// <summary>
		/// Gets the stylesheet "text-align" property (CSS) for the control.
		/// Descendants may override this function. Default is the empty string. When
		/// this value isn't set, the stylesheet property will not be set. Valid
		/// properties to return are "", "left", "center" and "right".
		/// </summary>
		protected virtual string TextAlign { get { return String.Empty; } }

		/// <summary>
		/// Mewthod could be implemented by descendants and must call the
		/// CreateJavascriptFunction(string innerCode) function. Overriding this
		/// method is only needed, when special javascript is needed, that goes
		/// behond the standard protected virtual boolean properties.
		/// </summary>
		protected virtual void CreateJavascriptFunction()
		{
			this.RegisterClientScriptFunction(String.Empty);
		}

		/// <summary>
		/// Creates default code for the descendants and adds the special js code
		/// from the descendants (innerCode) inside.
		/// </summary>
		/// <param name="innerCode">The descendant dependant code</param>
		protected void RegisterClientScriptFunction(string innerCode)
		{
			// Check if the function has already been registered, 
			// before we're doing more than strictly needed.
			// NOTE: In ASP.NET 2.0 The this.Page.IsClientScriptBlockRegistered is absolete
			if (this.Page.IsClientScriptBlockRegistered(OnKeyPressFunctionName) == false)
			{
				StringBuilder jsCode = new StringBuilder();

				jsCode.Append("<script language=\"javascript\"><!--\n");
				jsCode.Append("function " + OnKeyPressFunctionName + "(evt)");
				jsCode.Append(
					"{" +
					"	evt = (evt) ? evt : ((window.event) ? event : null);" +
					"	if (evt)" +
					"	{" +
					"		var charCode = (evt.charCode) ? evt.charCode :" +
					"		((evt.keyCode) ? evt.keyCode : " +
					"		((evt.which) ? evt.which : 0));" +
					"		var ch = String.fromCharCode(charCode);"
				);

				// Add the code from the descendant whitch implements valid key's and characters.
				jsCode.Append(innerCode);

				if (this.AllowAltKey)              jsCode.Append(_altKey)         .Append("\n");
				if (this.AllowCtrlKey)             jsCode.Append(_ctrlKey)        .Append("\n");
				if (this.AllowArrowKeys)           jsCode.Append(_arrowKeys)      .Append("\n");
				if (this.AllowDecimalSeparatorKey) jsCode.Append(_numberDecimalSeparatorKey).Append("\n");
				if (this.AllowFunctionKeys)        jsCode.Append(_functionKeys)   .Append("\n");
				if (this.AllowNegativeSignKey)     jsCode.Append(_negativeSignKey).Append("\n");
				if (this.AllowNumericKeys)         jsCode.Append(_numericKeys)    .Append("\n");
				if (this.AllowSpaceKey)            jsCode.Append(_spaceKey)       .Append("\n");
				if (this.AllowSpecialKeys)         jsCode.Append(_specialKeys)    .Append("\n");

				jsCode.Append(@"
							if (window.event) evt.returnValue = false;
							else evt.preventDefault();
						}
					}
				");
				jsCode.Append("\n--></script>");

				// Use this.Page.ClientScript.RegisterClientScriptBlock in ASP.NET 2.0
				this.Page.RegisterClientScriptBlock(
					OnKeyPressFunctionName, // the name of the function
					jsCode.ToString() // convert to string
					);
			}

		}

		/// <summary>
		/// Overridden. Registrers JavaScript function to Page and Adds the OnKeyPress event.
		/// </summary>
		/// <param name="e"><see cref="EventArgs"/></param>
		protected override void OnPreRender(EventArgs e)
		{
			this.CreateJavascriptFunction();

			// Adding the TextAlign property to the control's stylesheet.
			// For ASP.NET 2.0 this line should be: this.Style.Add(HtmlTextWriterStyle.TextAlign, this.TextAlign);
			if (this.TextAlign != null && this.TextAlign != String.Empty)
				this.Style.Add("text-align", this.TextAlign);

			this.Attributes.Add("onkeypress", OnKeyPressFunctionName + "(event)");

			base.OnPreRender(e);
		}
	}
}
