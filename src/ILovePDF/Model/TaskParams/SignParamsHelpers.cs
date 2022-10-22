using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams.Sign.Elements;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    /// EditParams
    /// </summary>
    public partial class SignParams 
    {
        /// <summary>
        /// <para>
        /// Use one of this elements: 
        /// <see cref="DateElement" />, 
        /// <see cref="TextElement" />, 
        /// <see cref="InitialsElement" />, 
        /// <see cref="InputElement" />, 
        /// <see cref="NameElement" />, 
        /// <see cref="SignatureElement" /> 
        /// </para>
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
            var element = new TextElement(text, "1", new Position (50, -50));
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
          
        public void Clear()
        {
            Elements.Clear();
        }
    }
}