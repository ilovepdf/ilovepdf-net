using LovePdf.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LovePdf.Model.TaskParams.Sign.Elements
{
    public class NameElement : BaseSignElement
    {
        public NameElement(string pages, Position position, int size = 18) 
            : base(SignElementTypes.Name, pages, position, size)
        { 
        }
    }
}
