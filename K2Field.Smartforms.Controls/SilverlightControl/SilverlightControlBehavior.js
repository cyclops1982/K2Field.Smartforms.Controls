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
if (typeof K2Field.Smartforms.Controls.SilverlightControl === "undefined" || K2Field.Smartforms.Controls.SilverlightControl === null) K2Field.Smartforms.Controls.SilverlightControl = {};


K2Field.Smartforms.Controls.SilverlightControl.SilverlightControlBehavior = function (element) {
    K2Field.Smartforms.Controls.SilverlightControl.SilverlightControlBehavior.initializeBase(this, [element]);
}


K2Field.Smartforms.Controls.SilverlightControl.SilverlightControlBehavior.prototype = {
    initialize: function () {
        K2Field.Smartforms.Controls.SilverlightControl.SilverlightControlBehavior.callBaseMethod(this, 'initialize');
    },

    setValue: function (objInfo) {
        var control = jQuery(this.get_element());
        var originalValue = control.val();
        var hasChanged = (originalValue != objInfo.Value);
        if (hasChanged) {
            control.val(objInfo.Value);
            //this._onChange();
        }
    },

    getValue: function () {
        var control = jQuery(this.get_element());
        return control.val();
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



    dispose: function () {
        K2Field.Smartforms.Controls.SilverlightControl.SilverlightControlBehavior.callBaseMethod(this, 'dispose');
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

K2Field.Smartforms.Controls.SilverlightControl.SilverlightControlBehavior.registerClass('K2Field.Smartforms.Controls.SilverlightControl.SilverlightControlBehavior', SourceCode.Forms.Controls.Web.BehaviorBase);