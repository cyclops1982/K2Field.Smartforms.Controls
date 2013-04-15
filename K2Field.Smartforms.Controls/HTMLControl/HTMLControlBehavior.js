/// <reference name="MicrosoftAjaxTimer.debug.js" />
/// <reference name="MicrosoftAjaxWebForms.debug.js" />
/// <reference name="AjaxControlToolkit.ExtenderBase.BaseScripts.js" assembly="AjaxControlToolkit" />

if (typeof K2Field === "undefined" || K2Field === null) K2Field = {};
if (typeof K2Field.Smartforms === "undefined" || K2Field.Smartforms === null) K2Field.Smartforms = {};
if (typeof K2Field.Smartforms.Controls === "undefined" || K2Field.Smartforms.Controls === null) K2Field.Smartforms.Controls = {};
if (typeof K2Field.Smartforms.Controls.HTMLControl === "undefined" || K2Field.Smartforms.Controls.HTMLControl === null) K2Field.Smartforms.Controls.HTMLControl = {};


K2Field.Smartforms.Controls.HTMLControl.HTMLControlBehavior = function (element) {
    K2Field.Smartforms.Controls.HTMLControl.HTMLControlBehavior.initializeBase(this, [element]);
}


K2Field.Smartforms.Controls.HTMLControl.HTMLControlBehavior.prototype = {
    initialize: function () {
        K2Field.Smartforms.Controls.HTMLControl.HTMLControlBehavior.callBaseMethod(this, 'initialize');
    },


    setValue: function (objInfo) {
        alert('setvalue');
        var frameName = "#" + objInfo.CurrentControlId + "_html";
        var frame = jQuery(frameName);
        $(frame).attr('src', objInfo.Value);
    },

    getValue: function () {
        alert('getvalue');
        return 'Myvalue';
        var frameName = "#" + objInfo.CurrentControlId + "_internalframe";
        var frame = jQuery(frameName);
        return $(frame).attr('src');
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
        K2Field.Smartforms.Controls.HTMLControl.HTMLControlBehavior.callBaseMethod(this, 'dispose');
    },

    setStyle: function (name, value) {
        alert('setStyle');
        UtilitiesHelper.setStyles(name, value, this.get_element());
    },

    getDefaultValue: function () {
        alert('getDefaultValue');
        var defaultValue = jQuery(this.get_element()).attr('defaultValue');
        return defaultValue;
    },

    validate: function (objInfo) {
        alert('validate');
        var input = jQuery(this.get_element());
        UtilitiesHelper.showValidationMessage(input, objInfo);
    }
}

K2Field.Smartforms.Controls.HTMLControl.HTMLControlBehavior.registerClass('K2Field.Smartforms.Controls.HTMLControl.HTMLControlBehavior', SourceCode.Forms.Controls.Web.BehaviorBase);