using System.Runtime.Serialization;

namespace LovePdf.Model.Enums
{
    /// <summary>
    ///  Languages
    /// </summary>
    public enum Languages
    {
        [EnumMember(Value = "ar")] Arabic,
        [EnumMember(Value = "bg")] Bulgarian,
        [EnumMember(Value = "ca")] Catalan,
        [EnumMember(Value = "de")] German,
        [EnumMember(Value = "el")] Greek,
        [EnumMember(Value = "en-US")] English,
        [EnumMember(Value = "es")] Spanish,
        [EnumMember(Value = "fr")] French,
        [EnumMember(Value = "hi")] Hindi,
        [EnumMember(Value = "id")] Indonesian,
        [EnumMember(Value = "it")] Italian,
        [EnumMember(Value = "ja")] Japanese,
        [EnumMember(Value = "ko")] Korean,
        [EnumMember(Value = "ms")] Malaysian,
        [EnumMember(Value = "nl")] Dutch,
        [EnumMember(Value = "pl")] Polish,
        [EnumMember(Value = "pt")] Portuguese,
        [EnumMember(Value = "ru")] Russian,
        [EnumMember(Value = "sv")] Swedish,
        [EnumMember(Value = "th")] Thai,
        [EnumMember(Value = "tr")] Turkish,
        [EnumMember(Value = "uk")] Ukrainian,
        [EnumMember(Value = "vi")] Vietnamese,
        [EnumMember(Value = "zh-Hans")] SimplifiedChinese,
        [EnumMember(Value = "zh-Hant")] TraditionalChinese,
        [EnumMember(Value = "zh-cn")] Chinese,
        [EnumMember(Value = "zh-tw")] TaiwanChinese,
    }
}