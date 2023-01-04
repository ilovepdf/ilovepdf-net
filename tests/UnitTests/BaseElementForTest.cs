using System;

namespace Tests
{
    public class BaseElementForTest
    {
        protected BaseElementForTest()
        {
            Rotation = LovePdf.Model.Enums.Rotate.Degrees0;
        }

        public String Password { get; set; }
        public LovePdf.Model.Enums.Rotate Rotation { get; set; } 
    }
}