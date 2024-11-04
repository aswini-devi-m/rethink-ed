using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public abstract class BaseTest
{
    protected readonly IPage _page;
    private readonly Dictionary<string, ILocator> _elements;

    public BaseTest(IPage page)
    {
        _page = page;
        _elements = new Dictionary<string, ILocator>();
        InitializeElements();
    }

    protected void RegisterElement(string name, string selector)
    {
        _elements[name] = _page.Locator(selector);
    }

    protected ILocator GetElement(string name) => _elements[name];

    private void InitializeElements()
    {
        foreach (var field in this.GetType().GetFields(System.Reflection.BindingFlags.NonPublic |
                                                        System.Reflection.BindingFlags.Instance))
        {
            if (field.FieldType == typeof(string))
            {
                var selector = field.GetValue(this) as string;
                if (!string.IsNullOrEmpty(selector))
                {
                    RegisterElement(field.Name, selector);
                }
            }
        }
    }
}
