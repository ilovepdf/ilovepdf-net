namespace Tests
{
    public class BaseElementForTest
    {
        public string Password { get; set; }
        public LovePdf.Model.Enums.Rotate Rotation { get; set; }

        protected BaseElementForTest()
        {
            Rotation = LovePdf.Model.Enums.Rotate.Degrees0;
        }
    }
}