var config = {
    id: null
}

var meterial = {
    init: function () {
        meterial.events();
    },
    events: function () {
        $('#btnCreateNew').off('click').on('click', function (e) {
            location.href = "/Meterial/CreateNew";
        });

        $('.editUnit').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            location.href = '/Meterial/Update?id=' + id;
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

        $('.viewDetail').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: "/Meterial/ViewDetail",
                data: { id: id },
                type: "post",
                dataType: "json",
                success: function (result) {
                    var model = result.data;

                    $('.lblName').text(model.Name);
                    $('.lblCreateBy').text(model.CreateBy);
                    $('.lblCreateDate').text(model.CreateDate);
                    $('.lblUpdateBy').text(model.UpdateBy);
                    $('.lblUpdateDate').text(model.UpdateDate);
                    $('.lblDescreption').text(model.Descreption);
                    $('.lblStatus').text(model.Status);

                    $('#modalViewDetail').modal();
                }
            });
        });
    }
}
meterial.init();