using System;
using UnityEngine;

[Serializable]
public class UM_InAppProduct
{
	public bool IsConsumable;

	public bool IsOpen;

	public string id = "new_product";

	public string IOSId = string.Empty;

	public string AndroidId = string.Empty;

	public string _price = string.Empty;

	private IOSProductTemplate _IOSTemplate = new IOSProductTemplate();

	private GoogleProductTemplate _AndroidTemplate = new GoogleProductTemplate();

	private UM_InAppProductTemplate _template = new UM_InAppProductTemplate();

	private bool _isTemplateSet;

	public IOSProductTemplate IOSTemplate
	{
		get
		{
			return _IOSTemplate;
		}
	}

	public GoogleProductTemplate AndroidTemplate
	{
		get
		{
			return _AndroidTemplate;
		}
	}

	public UM_InAppProductTemplate template
	{
		get
		{
			return _template;
		}
	}

	public string Title
	{
		get
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				return _AndroidTemplate.Title;
			case RuntimePlatform.IPhonePlayer:
				return _IOSTemplate.DisplayName;
			default:
				return string.Empty;
			}
		}
	}

	public string Description
	{
		get
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				return _AndroidTemplate.Description;
			case RuntimePlatform.IPhonePlayer:
				return _IOSTemplate.Description;
			default:
				return string.Empty;
			}
		}
	}

	public string LocalizedPrice
	{
		get
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				return (!_isTemplateSet) ? (_price + " $") : _AndroidTemplate.LocalizedPrice;
			case RuntimePlatform.IPhonePlayer:
				return (!_isTemplateSet) ? (_price + " $") : _IOSTemplate.LocalizedPrice;
			default:
				return _price + " $";
			}
		}
	}

	public string CurrencyCode
	{
		get
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				return (!_isTemplateSet) ? "USD" : _AndroidTemplate.PriceCurrencyCode;
			case RuntimePlatform.IPhonePlayer:
				return (!_isTemplateSet) ? "USD" : _IOSTemplate.CurrencyCode;
			default:
				return string.Empty;
			}
		}
	}

	public string ActualPrice
	{
		get
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				return (!_isTemplateSet) ? _price : _AndroidTemplate.Price.ToString();
			case RuntimePlatform.IPhonePlayer:
				return (!_isTemplateSet) ? _price : _IOSTemplate.Price.ToString();
			default:
				return _price;
			}
		}
		set
		{
			_price = value;
		}
	}

	public float ActualPriceValue
	{
		get
		{
			float result = 0f;
			if (float.TryParse(ActualPrice, out result))
			{
				return result;
			}
			return 0f;
		}
	}

	public void SetTemplate(IOSProductTemplate tpl)
	{
		_IOSTemplate = tpl;
		_template = new UM_InAppProductTemplate();
		_template.id = tpl.Id;
		_template.title = tpl.DisplayName;
		_template.description = tpl.Description;
		_template.price = tpl.Price.ToString();
		_isTemplateSet = true;
	}

	public void SetTemplate(GoogleProductTemplate tpl)
	{
		_AndroidTemplate = tpl;
		_template = new UM_InAppProductTemplate();
		_template.id = tpl.SKU;
		_template.title = tpl.Title;
		_template.description = tpl.Description;
		_template.price = tpl.Price.ToString();
		_isTemplateSet = true;
	}

	public override string ToString()
	{
		return string.Format("[UM_InAppProduct: template={0}, Title={1}, Description={2}, Price={3},  IOSTemplate={4}, AndroidTemplate={5}]", template, Title, Description, LocalizedPrice, IOSTemplate, AndroidTemplate);
	}
}
