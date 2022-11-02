using LovePdf.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LovePdf.Model.TaskParams.Sign.Elements
{
    public class InitialsElement : BaseSignElement
    {
        public InitialsElement(string pages, Position position, int size = 18) 
            : base(SignElementTypes.Initials, pages, position, size)
        { 
        }
    }
}
