
class DevExtremeCustom {

    Localization(language = "tr") {

        var currentLanguage = sessionStorage.getItem("locale");

        if (currentLanguage !== language) {
            sessionStorage.setItem("locale", language);
            document.location.reload();
        }

        DevExpress.localization.locale(language);
    }

    Default() {

        DevExpress.ui.dxDataGrid.defaultOptions({
            searchPanel: {
                visible: true,
                width: 240,
            },
        });

        DevExpress.config({
            decimalSeparator: ",",
            defaultCurrency: "TL",
            editorStylingMode: "outlined",
            thousandsSeparator: "."
        });
    }


    SendRequest(url, method, data, notify) { // FUNC

        var d = $.Deferred();

        $.ajax(url, {
            method: method || "GET",
            data: data,
        }).done(function (result) {

            var beSolved = result ? (typeof (result.Data) == "object" ? result.Data : result) : "";

            d.resolve(beSolved);

            if (notify)
                devExtremeCustom.Notification(result);

        }).fail(function (xhr) {
            d.reject(xhr.responseJSON ? xhr.responseJSON.Message : xhr.statusText);
            util.showNotification({ "title": "Hata", text: "İşleminiz Sırasında Hata Meydana Geldi.", "type": "error" });
        });

        return d.promise();
    }


    Notification(result) { // FUNC
        if (result && result.ServiceResultType && result.ServiceResultType == 1)
            util.showNotification({ "title": "İşlem Başarılı", text: "İşleminiz Başarı İle Gerçekleştirildi.", "type": "success", icon: "glyphicon glyphicon-envelope pNotifyIconCustom" });
        else
            util.showNotification({ "title": "Hata", text: result.Message || "İşleminiz Sırasında Hata Meydana Geldi.", "type": "error" });
    }

}
var devExtremeCustom = new DevExtremeCustom();

devExtremeCustom.Localization();
devExtremeCustom.Default();


