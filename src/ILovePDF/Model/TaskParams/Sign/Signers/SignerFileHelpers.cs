using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams.Sign.Elements;

namespace LovePdf.Model.TaskParams.Sign.Signers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SignerFile
    {
        /// <summary> 
        /// Use one of this elements: 
        /// <list type="bullet"> 
        /// <item><see cref="DateElement" /> </item> 
        /// <item><see cref="TextElement" /></item> 
        /// <item><see cref="InitialsElement" /></item> 
        /// <item><see cref="InputElement" /></item> 
        /// <item><see cref="NameElement" /></item> 
        /// <item><see cref="SignatureElement" /> </item> 
        /// </list>
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public ISignElement AddElement(ISignElement element)
        {
            Elements.Add(element);
            return element;
        }

        /// <inheritdoc cref="SignElementTypes.Text"/>
        public TextElement AddText(string text)
        {
            var element = new TextElement(text, "1", new Position(50, -50));
            Elements.Add(element);
            return element;
        }

        /// <inheritdoc cref="SignElementTypes.Date"/>
        public DateElement AddDate(string date)
        {
            var element = new DateElement(date, "1", new Position(50, -50));
            Elements.Add(element);
            return element;
        }

        /// <inheritdoc cref="SignElementTypes.Initials"/>
        public InitialsElement AddInitials()
        {
            var element = new InitialsElement("1", new Position(50, -50));
            Elements.Add(element);
            return element;
        }

        /// <inheritdoc cref="SignElementTypes.Input"/>
        public InputElement AddInput()
        {
            var element = new InputElement("1", new Position(50, -50));
            Elements.Add(element);
            return element;
        }

        /// <inheritdoc cref="SignElementTypes.Name"/>
        public NameElement AddName()
        {
            var element = new NameElement("1", new Position(50, -50));
            Elements.Add(element);
            return element;
        }

        /// <inheritdoc cref="SignElementTypes.Signature"/>
        public SignatureElement AddSignature()
        {
            var element = new SignatureElement("1", new Position(50, -50));
            Elements.Add(element);
            return element;
        }

        public void ClearElements()
        {
            Elements.Clear();
        }
    }
}
