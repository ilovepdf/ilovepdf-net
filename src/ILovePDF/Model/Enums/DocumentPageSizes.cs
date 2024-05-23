using System.Runtime.Serialization;

namespace LovePdf.Model.Enums
{
    /// <summary>
    /// Final document sizes that provides API. 
    /// </summary>
    public enum DocumentPageSizes
    {
        /// <summary>
        /// Your final document will have the same size as the original web page.
        /// </summary>
        [EnumMember(Value = "auto")] Auto,

        /// <summary>
        /// Your final document will have the same size as the original web page.
        /// </summary>
        [EnumMember(Value = "fit")] Fit,

        /// <summary>
        /// Your final document will have an A3 standard size and your web page will fit in that size.
        /// </summary>
        [EnumMember(Value = "A3")] A3,


        /// <summary>
        /// Your final document will have an A4 standard size and your web page will fit in that size.
        /// </summary>
        [EnumMember(Value = "A4")] A4,

        /// <summary>
        /// Your final document will have an A5 standard size and your web page will fit in that size.
        /// </summary>
        [EnumMember(Value = "A5")] A5,

        /// <summary>
        /// Your final document will have an A6 standard size and your web page will fit in that size.
        /// </summary>
        [EnumMember(Value = "A6")] A6,

        /// <summary>
        /// Your final document will have Letter standard size and your web page will fit in that size.
        /// </summary>
        [EnumMember(Value = "letter")] Letter
    }
}