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
    this._fileObject = null;
    this._status = "NONE";
    this._chunkSize = 102400;
    this._filePath = "";
    this._fileName = '';
    this.contenttype = 'file';

}


K2Field.Smartforms.Controls.SilverlightControl.SilverlightControlBehavior.prototype = {
    initialize: function () {
        K2Field.Smartforms.Controls.SilverlightControl.SilverlightControlBehavior.callBaseMethod(this, 'initialize');
    },

    uploadFile: function () {
        this._fileObject = this.getScriptableSilverlightObject();

        if (this._fileObject != null) {
            this._fileName = this._fileObject.fileName;
            this._status = "BUSY";
            var chunkContents = this._fileObject.byteArrayChunk(this._chunkSize);
            WebForm_DoCallback(this._callbackID, 'oldFileName=' + this._fileName + ';fileContents=' + chunkContents, this._finishUploadFile, this, this._onError, false);
        }
        else {
            alert('no silverlight app found.');
        }
    },



    _onError: function (message, context) {
        popupManager.showError(message);
    },

    _finishUploadFile: function (arg, context) {
        alert('finish file upload');
        context._filePath = arg;

        if (!context._fileObject.completedReading()) {
            var chunkContents = context._fileObject.byteArrayChunk(context._chunkSize);
            WebForm_DoCallback(context._callbackID, 'newFileName=' + context._filePath + ';fileContents=' + chunkContents, context._finishUploadFile, context, context._onError, false);
        }
        else {
            context._status = "COMPLETE";
        }
    },

    getFileValue: function () {
        return { status: this._status, name: this._fileName, path: this._filePath };
    },

    setFileValue: function (fileObj) {
        this._status = fileObj.status;
        this._filePath = fileObj.path;
        this._setUploadedFile(fileObj.name);
    },


    _setUploadedFile: function (fileName) {
        if (fileName != "") {
            this._status = "COMPLETE";
        } else {
            this._status = "NONE";
        }
    },



    getScriptableSilverlightObject: function () {
        var obj = jQuery('#' + this._id + "_SilverlightControl")[0];
        var result = null;
        if (checkExists(obj)) {
            if (checkExists(obj.Content)) {
                if (checkExists(obj.Content.uploadPage)) {
                    result = obj.Content.uploadPage;
                }
            }
        }
        return result;
    },

    setValue: function (objInfo) {
        this.fileContent = objInfo.Value;
        var control = jQuery(this.get_element());
    },

    getValue: function () {
        alert('getvalue');
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

    get_CallbackID: function () {
        return this._callbackID;
    },

    set_CallbackID: function (value) {
        this._callbackID = value;
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

function doSilverlightUpload(objectId) {
    if (objectId) {
        $find(objectId).uploadFile();
    }
}



var MICROSOFTCALLBACKBUG
if (MICROSOFTCALLBACKBUG == undefined || !MICROSOFTCALLBACKBUG) {
    MICROSOFTCALLBACKBUG = true;
    var GlvDelayedNextPageNo;

    function WebForm_CallbackComplete_SyncFixed() {
        // the var statement ensure the variable is not global
        for (var i = 0; i < __pendingCallbacks.length; i++) {
            callbackObject = __pendingCallbacks[i];
            if (callbackObject && callbackObject.xmlRequest &&
			(callbackObject.xmlRequest.readyState == 4)) {
                // SyncFixed: line move below // WebForm_ExecuteCallback(callbackObject);
                if (!__pendingCallbacks[i].async) {
                    __synchronousCallBackIndex = -1;
                }
                __pendingCallbacks[i] = null;
                var callbackFrameID = "__CALLBACKFRAME" + i;
                var xmlRequestFrame = document.getElementById(callbackFrameID);
                if (xmlRequestFrame) {
                    xmlRequestFrame.parentNode.removeChild(xmlRequestFrame);
                }
                // SyncFixed: the following statement has been moved down from above;
                WebForm_ExecuteCallback(callbackObject);
            }
        }
    }

    var OnloadWithoutSyncFixed = window.onload;

    window.onload = function Onload() {
        if (typeof (WebForm_CallbackComplete) == "function") {
            // Set the fixed version
            WebForm_CallbackComplete = WebForm_CallbackComplete_SyncFixed;
            // CallTheOriginal OnLoad
            if (OnloadWithoutSyncFixed != null) OnloadWithoutSyncFixed();
        }
    }
}