// LICENSE https://github.com/AndrewKeepCoding/AK.Toolkit

namespace SettingsUI.Tools;

public record StringResource(string Key, string DependencyPropertyName, string Value);

public class StringResourceList : List<StringResource>
{
}

public class StringResourceListDictionary : ReadOnlyDictionary<string, StringResourceList>
{
    public StringResourceListDictionary(IDictionary<string, StringResourceList> dictionary) : base(dictionary)
    {
    }
}
