var wateverCallBack = null;
var waterXmlValue = null;


function F2FieldLiteralInit(propertyXml, fullXml, controlDefinitionXml, controlType, callback) {
    wateverCallBack = callback;
    //store value
    if (!checkExists(propertyXml))
        propertyXml = '<Controls><Control/></Controls>';

    waterXmlValue = parseXML(propertyXml);
    //pull out value to rendered control
    var value = waterXmlValue.selectSingleNode('Controls/Control/Properties/Property[Name="HTML"]/Value')
    if (checkExists(value))
        jQuery("#HTMLControlPropertyConfiguration textarea").val(value.text);
    //show popup
    popupManager.showPopup({
        id: 'anyidasdasdasd',
        buttons: [
            {
                id: 'HtmlPropBtnOk',
                text: "ok", // Resources.WizardButtons.OKButtonText,
                click: F2FieldLiteralReturned
            },
            {
                id: 'HtmlPropBtnCancel',
                text: 'cancel'//Resources.WizardButtons.CancelButtonText
            }
        ],
        closeWith: 'HtmlPropBtnCancel',
        headerText: "HTML",
        draggable: true,
        content: jQuery('#HTMLControlPropertyConfiguration'),
        width: 600,
        height: 500
    });
}

function F2FieldLiteralReturned() {
    //begin save value from control
    var node = waterXmlValue.selectSingleNode('Controls/Control/Properties/Property[Name="HTML"]');

    if (!checkExists(node)) {
        node = waterXmlValue.createElement("Property");
        var name = waterXmlValue.createElement("Name");
        name.appendChild(waterXmlValue.createTextNode("HTML"));
        node.appendChild(name);
        var properties = waterXmlValue.selectSingleNode("Controls/Control/Properties");
        properties.appendChild(node);
    }

    //remove old value
    var valueNode = node.selectSingleNode("Value");
    if (checkExists(valueNode))
        node.removeChild(valueNode);
    //add new value
    valueNode = waterXmlValue.createElement("Value");
    valueNode.appendChild(waterXmlValue.createTextNode(jQuery("#HTMLControlPropertyConfiguration textarea").val()));
    node.appendChild(valueNode);

    //end save value from control
    //callback
    wateverCallBack(1, waterXmlValue.xml);
    popupManager.closeLast();
}