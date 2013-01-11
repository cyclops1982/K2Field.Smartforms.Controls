// README
//
// There are two steps to adding a property:
//
// 1. Create a member variable to store your property
// 2. Add the get_ and set_ accessors for your property
//
// Remember that both are case sensitive!
/// <reference name="MicrosoftAjaxTimer.debug.js" />
/// <reference name="MicrosoftAjaxWebForms.debug.js" />
/// <reference name="AjaxControlToolkit.ExtenderBase.BaseScripts.js" assembly="AjaxControlToolkit" />

if (typeof K2Field === "undefined" || K2Field === null) K2Field = {};
if (typeof K2Field.Smartforms === "undefined" || K2Field.Smartforms === null) K2Field.Smartforms = {};
if (typeof K2Field.Smartforms.Controls === "undefined" || K2Field.Smartforms.Controls === null) K2Field.Smartforms.Controls = {};
if (typeof K2Field.Smartforms.Controls.AutocompleteTextbox === "undefined" || K2Field.Smartforms.Controls.AutocompleteTextbox === null) K2Field.Smartforms.Controls.AutocompleteTextbox = {};


K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextbox = function (element) {
    K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextbox.initializeBase(this, [element]);
    this._options = [];
    this._isEnabled = true;
    this._isVisible = true;
    this._isReadOnly = false;
    this._dataSourceType = ""; // "Static", "SmartObject"
    this._fixedListItems = ""; // &lt;Items&gt;&lt;Item&gt;1&lt;/Item&gt;&lt;Item&gt;2&lt;/Item&gt;&lt;Item&gt;3&lt;/Item&gt;&lt;Item&gt;4&lt;/Item&gt;&lt;Item&gt;5&lt;/Item&gt;&lt;/Items&gt;
    this._displaytemplate = null; // What fields to display from the SMO: 
    // &lt;Template&gt;&lt;Item SourceType="ObjectProperty" SourceID="CountryId" DataType="Text"/&gt;&lt;Item SourceType="ObjectProperty" SourceID="NAme" DataType="Text"/&gt;&lt;/Template&gt;
    this._valueproperty = null; // value property 'Id'
    this._associationSO = null; // Guid of an SO? 11817d76-c1a0-4ee4-bff4-74fdb0f511da
    this._associationMethod = ""; // method selected "GetList"
    this._initialized = false;
}



K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextbox.prototype = {
    initialize: function () {
        K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextbox.callBaseMethod(this, 'initialize');
        if (this._dataSourceType == "Static") {
            this.setItems();
        }

        this._initialized = true;
    },

    setItems: function (objInfo) {
        var thisDropDown = this.get_element();
        var jqDropDown = jQuery(thisDropDown);
        var originalValue = thisDropDown.value;
        var selectedValue = "";
        var displayNodes;
        var xmlDoc = null;
        var useItems = false;

        if (this._dataSourceType == "Static") {
            xmlDoc = parseXML(this._fixedListItems);
            displayNodes = xmlDoc.selectNodes("Items/Item");
            useItems = true;
        } else {
            xmlDoc = objInfo.XmlDocument;
            displayNodes = xmlDoc.selectNodes("collection/object[@parentid='" + this._associationSO + "']");
        }

        jqDropDown.val('');

        this._options = [];
        var values = [];
        var optionDisplay = "";
        var optionValue = "";
        var optionSelected = false;
        /*
        if (this._allowEmptySelection) {
        optionSelected = (i == 0 && this._allowEmptySelection == false);
        var option =
        {
        index: optionIndex,
        text: "",
        value: "",
        className: "",
        selected: optionSelected,
        disabled: ""
        }
        options[optionIndex++] = option;
        }*/

        for (var i = 0; i < displayNodes.length; i++) {
            optionSelected = (i == 0 && this._allowEmptySelection == false);

            if (useItems) {
                optionDisplay = displayNodes[i].text;
                optionValue = optionDisplay;
            } else {
                optionDisplay = UtilitiesHelper.setDisplayTemplateValue(this._displaytemplate, displayNodes[i]);
                optionValue = displayNodes[i].selectSingleNode("fields/field[@name='" + this._valueproperty + "']/value").text;
            }

            var option = {
                index: i,
                value: optionValue,
                label: optionDisplay
            }
            values[i] = option.label;
            this._options[i] = option;

            if (optionSelected) {
                selectedValue = optionValue;
            }
        }

        jQuery(this.get_element()).autocomplete({
            source: values
        });


        thisDropDown.value = selectedValue;


        if (originalValue != thisDropDown.value) {
            raiseEvent(this._id, "Control", "OnChange");
        }

        if (displayNodes.length == 0) {
            return false;
        }
    },


    setValue: function (objInfo) {
        var thisDropDown = this.get_element();
        var jqDropDown = jQuery(thisDropDown);
        var found = false;
        for (var i = 0; i < this._options.length; i++) {
            if (this._options[i].value == objInfo.Value) {
                found = true;
                thisDropDown.value = objInfo.Value;
            }
        }
        if (!found) {
            alert('Item loaded which is not in the dropdown list.');
        }
    },

    getValue: function () {
        var thisDropDown = this.get_element();
        var jqDropDown = jQuery(thisDropDown);
        for (var i = 0; i < this._options.length; i++) {
            if (this._options[i].label == jqDropDown.val()) {
                return this._options[i].value;
            }
        }
        return 0;
    },


    set_isVisible: function (value) {
        this._isVisible = value;
        if (this._initialized) {
            if (value == false) {
                jQuery(this.get_element()).hide();
            } else {
                jQuery(this.get_element()).show();
            }
        }
    },

    get_isVisible: function () {
        return this._isVisible;
    },


    get_isEnabled: function () {
        return this._isEnabled;
    },
    set_isEnabled: function (value) {
        this._isEnabled = value;
        if (this._initialized) {
            if (value == false) {
                jQuery(this.get_element()).addClass("disabled");
            } else {
                jQuery(this.get_element()).removeClass("disabled");
            }
        }
    },


    get_isReadOnly: function () {
        alert('get isreadonly called');
        return this._isReadOnly;
    },
    set_isReadOnly: function (value) {
        alert('set isReadOnly called');
        this._isReadOnly = value;
        if (this._initialized) {
            if (value == true) {
                jQuery(this.get_element()).textbox({}).textbox("readonly");
            }
            else {
                jQuery(this.get_element()).textbox({}).textbox("editable");
            }
        }
    },


    dispose: function () {
        K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextbox.callBaseMethod(this, 'dispose');
    },

    setStyle: function (name, value) {
        UtilitiesHelper.setStyles(name, value, this.get_element());
    },

    getDefaultValue: function () {
        var defaultValue = jQuery(this.get_element()).attr('defaultValue');
        return defaultValue;
    },

    validate: function (objInfo) {
        var input = jQuery(this.get_element());
        UtilitiesHelper.showValidationMessage(input, objInfo);
    }
}

K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextbox.registerClass('K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextbox', SourceCode.Forms.Controls.Web.BehaviorBase);