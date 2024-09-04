using System;

namespace Tests
{
    public class BaseElementForTest
    {
        protected BaseElementForTest()
        {
            Rotation = iLovePdf.Model.Enums.Rotate.Degrees0;
        }

        public String Password { get; set; }
        public iLovePdf.Model.Enums.Rotate Rotation { get; set; } 
    }
}