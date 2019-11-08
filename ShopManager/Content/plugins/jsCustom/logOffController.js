var LogOff = {
    init: function () {
        LogOff.events();
    },
    events: function () {
        $('#btnLogOff').off('click').on('click', function () {
            $('#frmLogOff').submit();
        });
    }
}
LogOff.init();