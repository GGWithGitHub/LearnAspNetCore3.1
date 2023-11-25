var API =
{
    post: function (url, payload) {
        return $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(payload),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    },
    addresses:
    {
        save: function (address) {
            return $.post('/webservice/user/saveUserAddress', address);
        },
        getAll: function () {
            return $.getJSON('/webservice/user/getUserAddresses');
        },
        get: function (id) {
            return $.getJSON('/webservice/user/getUserAddress', { id: id });
        },
        delete: function (id) {
            return $.getJSON('/webservice/user/deleteUserAddress', { id: id });
        }
    }
};