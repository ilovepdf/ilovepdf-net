using iLovePdf.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace iLovePdf.Model.TaskParams.Sign.Elements
{
    public class InitialsElement : BaseSignElement
    {
        public InitialsElement(string pages, Position position, int size = 18) 
            : base(SignElementTypes.Initials, pages, position, size)
        { 
        }
    }
}
