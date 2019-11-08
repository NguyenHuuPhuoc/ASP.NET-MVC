var config = {

}

var Size = {
    init: function () {
        Size.events();
    },
    events: function () {
        $('#btnCreateNew').off('click').on('click', function () {
            location.href = "/Size/CreateNew";
        });

        $('.editUnit').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            location.href = "/Size/Update?id=" + id;
        });

        $('.deleteUnit').off('click').on('click', function (e) {
            config.id = $(this).data('id');
            $('#modalConfirmDelete').show();
        });

        $('#btnOkDelete').off('click').on('click', function (e) {
            $('#modalConfirmDelete').hide();
            var id = config.id;
            config.id = null;
            $('#frm_' + id).submit();
        });
    }
}
Size.init();