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
    this._isEnabled = true;
}


K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextbox.prototype = {
    initialize: function () {
        K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextbox.callBaseMethod(this, 'initialize');
        $.ajaxSetup({
            // Disable caching of AJAX responses
            cache: false
        });

        // Overrides the default autocomplete filter function to search only from the beginning of the string
        $.ui.autocomplete.filter = function (array, term) {
            var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(term), "i");
            return $.grep(array, function (value) {
                return matcher.test(value.label || value.value || value);
            });
        };


        // Caching idea + code from http://developwithstyle.com/articles/2010/05/14/jquery-ui-autocomplete-is-it-any-good/
        var countryCache = {};
        jQuery(this.get_element()).autocomplete({
            source: function (request, response) {
                if (countryCache.term == request.term && countryCache.content) {
                    response(countryCache.content);
                    return;
                }
                if (new RegExp(countryCache.term).test(request.term) && countryCache.content && countryCache.content.length < 13) {
                    response($.ui.autocomplete.filter(countryCache.content, request.term));
                    return;
                }


                $.ajax({
                    url: 'http://k2.denallix.com:8888/SmartObjectServices/rest/Novartis/SmartObjects/Country/GetList?$format=XML',
                    dataType: 'xml',
                    type: 'GET',
                    crossDomain: false,
                    error: function (data, error, status) {
                        alert(error);
                    },
                    success: function (data) {
                        var ret = new Array();
                        var x = $(data).find('Country');
                        for (var i = 0; i < x.length; i++) {
                            var name = $(x[i]).find('Name');
                            ret[i] = name.text();
                        }
                        countryCache.term = request.term;
                        countryCache.content = $.ui.autocomplete.filter(ret, request.term);
                        response(countryCache.content);
                    }
                });

            }
        });
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