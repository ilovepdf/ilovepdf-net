using iLovePdf.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace iLovePdf.Model.TaskParams.Sign.Elements
{
    public class SignatureElement : BaseSignElement
    {
        public SignatureElement(string pages, Position position, int size = 18)
            : base(SignElementTypes.Signature, pages, position, size)
        { 
        }
    }
}
