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
if (typeof K2Field.Smartforms.Controls.KeypressEvent === "undefined" || K2Field.Smartforms.Controls.KeypressEvent === null) K2Field.Smartforms.Controls.KeypressEvent = {};


K2Field.Smartforms.Controls.KeypressEvent.KeypressEventBehavior = function (element) {
    K2Field.Smartforms.Controls.KeypressEvent.KeypressEventBehavior.initializeBase(this, [element]);
    this._isEnabled = true;
}


K2Field.Smartforms.Controls.KeypressEvent.KeypressEventBehavior.prototype = {
    initialize: function () {
        K2Field.Smartforms.Controls.KeypressEvent.KeypressEventBehavior.callBaseMethod(this, 'initialize');
        $addHandler(this.get_element(), 'change', Function.createDelegate(this, this._onChange));
        var input = jQuery(document);
        input.keypress(jQuery.proxy(this._onChange, this));
    },

    _onChange: function () {
        raiseEvent(this._id, "Control", "KeyPressed");
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

    dispose: function () {
        K2Field.Smartforms.Controls.KeypressEvent.KeypressEventBehavior.callBaseMethod(this, 'dispose');
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

K2Field.Smartforms.Controls.KeypressEvent.KeypressEventBehavior.registerClass('K2Field.Smartforms.Controls.KeypressEvent.KeypressEventBehavior', SourceCode.Forms.Controls.Web.BehaviorBase);