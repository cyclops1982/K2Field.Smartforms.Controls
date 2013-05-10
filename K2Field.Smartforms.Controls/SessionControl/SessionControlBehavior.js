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
if (typeof K2Field.Smartforms.Controls.SessionControl === "undefined" || K2Field.Smartforms.Controls.SessionControl === null) K2Field.Smartforms.Controls.SessionControl = {};


K2Field.Smartforms.Controls.SessionControl.SessionControlBehavior = function (element) {
    K2Field.Smartforms.Controls.SessionControl.SessionControlBehavior.initializeBase(this, [element]);
    this._sessionvariablevalue = "";
    this._sessionvariablename = "";
}



K2Field.Smartforms.Controls.SessionControl.SessionControlBehavior.prototype = {
    initialize: function () {
        K2Field.Smartforms.Controls.SessionControl.SessionControlBehavior.callBaseMethod(this, 'initialize');
    },

    setValue: function (objInfo) {
        this._sessionvariablevalue = objInfo.Value;
    },

    getValue: function () {
        return this._sessionvariablevalue;
    },

    dispose: function () {
        K2Field.Smartforms.Controls.SessionControl.SessionControlBehavior.callBaseMethod(this, 'dispose');
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

K2Field.Smartforms.Controls.SessionControl.SessionControlBehavior.registerClass('K2Field.Smartforms.Controls.SessionControl.SessionControlBehavior', SourceCode.Forms.Controls.Web.BehaviorBase);