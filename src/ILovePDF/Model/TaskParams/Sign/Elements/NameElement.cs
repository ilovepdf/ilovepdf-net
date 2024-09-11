using iLovePdf.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace iLovePdf.Model.TaskParams.Sign.Elements
{
    public class NameElement : BaseSignElement
    {
        public NameElement(string pages, Position position, int size = 18) 
            : base(SignElementTypes.Name, pages, position, size)
        { 
        }
    }
}
